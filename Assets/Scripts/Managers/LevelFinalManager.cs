using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelFinalManager : MonoBehaviour
{
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private PlayerController _player;
    [SerializeField] private PlayerSneeze _playerSneeze;
    [SerializeField] private PlayerWave _playerWave;
    [SerializeField] private PlayerDash _playerDash;
    [SerializeField] private HiringManager _hiringManager;
    [SerializeField] private OfficeChairSpawner _chairSpawner;

    private bool _initiateManagerMove = false;
    private bool _initiateChairSpawner = false;

    // Dialog collection
    private string[] _introDialog = new string[3]
    {
        "Hiring Manager: Hi Alex! It's nice to meet you.",
        "Alex: It is very nice to meet you too.",
        "Hiring Manager: Follow me this way and we'll get started."
    };
    private string[] _interviewDialog = new string[8]
    {
        "Hiring Manager: Alex, today I'd like you put your skills into practice.",
        "Hiring Manager: I'd like you to use any skills necessary to help me complete some tasks I've setup for you.",
        "Hiring Manager: Firstly, I'd like you to clean all the whiteboards in this room.",
        "Hiring Manager: Secondly, I need help finding an important document I seemed to have misplaced.",
        "Hiring Manager: Please find it, and bring it to me.",
        "Hiring Manager: I may throw in some obstacles along the way just to make things interesting.",
        "Alex: Sounds good. I'm ready.",
        "Hiring Manager: Great! Ready, set, go!"
    };

    public void DisplayNextDialogLine(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_uIManager.DisplayNextLine())
            {
                // End of dialog set
                _player.SwitchToPlayerActionMap();

                if (_initiateManagerMove)
                {
                    _hiringManager.MoveToPosition2();
                    _initiateManagerMove = false;
                }
                else if (_initiateChairSpawner)
                {
                    _chairSpawner.StartSpawning();
                    _initiateChairSpawner = false;
                }
            }
        }
    }

    public void StartInterviewDialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(_interviewDialog);
        _initiateChairSpawner = true;
    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            // Set visibility for mobile action buttons
            _uIManager.SetSneezeButtonVisibility(true);
            _uIManager.SetWaveButtonVisibility(true);
            _uIManager.SetDashButtonVisibility(true);
            _uIManager.SetPickUpButtonVisibility(false);
            _uIManager.SetOpenButtonVisibility(false);
        }

        // Activate special actions that are available in this level
        _playerSneeze.ActivateSneeze();
        _playerWave.ActivateWave();
        _playerDash.ActivateDash();

        StartIntroDialog();
    }

    private void StartIntroDialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(_introDialog);
        _initiateManagerMove = true;
    }
}
