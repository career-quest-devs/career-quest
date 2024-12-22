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

    private bool _initiateManagerMove = false;

    // Dialog collection
    private string[] _introDialog = new string[2]
    {
        "Hiring Manager: Hi Alex! It's nice to meet you.",
        "Blah, blah, blah"
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
                }
            }
        }
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
