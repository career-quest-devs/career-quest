using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    private bool _initiateInterviewStart = false;
    private bool _initiateInterviewEnding = false;
    private bool _startChairSpawning = false;
    private bool _hitByChairIntroCompleted = false;
    private int _numberOfWhiteboardsCleaned = 0;

    // Dialog collection
    private string[] _introDialog = new string[3]
    {
        "Hiring Manager: Hi Alex! It's nice to meet you.",
        "Alex: It is very nice to meet you too.",
        "Hiring Manager: Follow me this way and we'll get started."
    };
    private string[] _interviewDialog = new string[5]
    {
        "Hiring Manager: Alex, today I would like you to showcase your creativity and skills.",
        "Hiring Manager: I would like you to clean all the whiteboards in this room using any skills necessary.",
        "Hiring Manager: I may throw in some obstacles along the way just to make things interesting.",
        "Alex: Sounds good. I'm ready.",
        "Hiring Manager: Great! You may begin."
    };
    private string[] _hitByChairIntroDialog = new string[2]
    {
        "When you are hit with an office chair, you will lose the ability to use any special skills for 2 seconds.",
        "To avoid the chairs, try using Power Dash by pressing SPACE while moving left or right."
    };
    private string[] _hitByChairIntroDialogMobile = new string[2]
    {
        "When you are hit with an office chair, you will lose the ability to use any special skills for 2 seconds.",
        "To avoid the chairs, try using Power Dash by pressing the new button on the right while moving left or right."
    };
    private string[] _interviewClosingDialog = new string[2]
    {
        "Hiring Manager: Congratulations, Alex! You have completed the interview.",
        "Hiring Manager: Thank you for coming in today! We will be in touch with you shortly."
    };

    public void DisplayNextDialogLine(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_uIManager.DisplayNextLine())
            {
                if (_initiateInterviewEnding)
                {
                    // Save time taken to complete level
                    DataTracker.GetInstance().SetLevelTime(7, _uIManager.GetLevelTimeTaken());

                    int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

                    SceneManager.LoadScene(nextSceneIndex);
                }

                // End of dialog set
                _player.SwitchToPlayerActionMap();

                if (_initiateManagerMove)
                {
                    _hiringManager.MoveToPosition2();
                    _initiateManagerMove = false;
                }
                else if (_initiateInterviewStart)
                {
                    // Activate special actions that are available in this level
                    _playerSneeze.ActivateSneeze();
                    _playerWave.ActivateWave();
                    _playerDash.ActivateDash();

                    if (_uIManager.IsRunningOnMobile())
                    {
                        // Set visibility for mobile action buttons
                        _uIManager.SetSneezeButtonVisibility(true);
                        _uIManager.SetWaveButtonVisibility(true);
                        _uIManager.SetDashButtonVisibility(true);
                    }

                    _chairSpawner.StartSpawning();
                    _initiateInterviewStart = false;
                }
                else if (_startChairSpawning)
                {
                    _chairSpawner.StartSpawning();
                    _startChairSpawning = false;
                }
            }
        }
    }

    public void StartInterviewDialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(_interviewDialog);
        _initiateInterviewStart = true;
    }

    public void StartHitByChairIntroDialog()
    {
        if (!_hitByChairIntroCompleted)
        {
            _chairSpawner.StopSpawning();
            _player.SwitchToUIActionMap();

            if (_uIManager.IsRunningOnMobile())
            {
                _uIManager.StartDialog(_hitByChairIntroDialogMobile);
            }
            else
            {
                _uIManager.StartDialog(_hitByChairIntroDialog);
            }
            
            _startChairSpawning = true;
            _hitByChairIntroCompleted = true;
        }
    }

    public void UpdateWhiteboardTask()
    {
        _numberOfWhiteboardsCleaned++;

        if (_numberOfWhiteboardsCleaned == 3)
        {
            StartInterviewClosingSequence();
        }
    }

    private void Start()
    {
        if (_uIManager.IsRunningOnMobile())
        {
            // Set visibility for mobile action buttons
            _uIManager.SetSneezeButtonVisibility(false);
            _uIManager.SetWaveButtonVisibility(false);
            _uIManager.SetDashButtonVisibility(false);
            _uIManager.SetPickUpButtonVisibility(false);
            _uIManager.SetOpenButtonVisibility(false);
        }

        StartIntroDialog();
    }

    private void StartIntroDialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(_introDialog);
        _initiateManagerMove = true;
    }

    private void StartInterviewClosingSequence()
    {
        _chairSpawner.StopSpawning();
        _player.SwitchToUIActionMap();
        _hiringManager.MoveCloseToPlayer(_player.transform);
        _initiateInterviewEnding = true;

        _uIManager.StartDialog(_interviewClosingDialog);
    }
}
