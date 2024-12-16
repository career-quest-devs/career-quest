using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Level1Manager : MonoBehaviour
{
    public string[] introDialog;
    public string[] sneezeTriggerDialog;
    public string[] sneezeTutorialDialogWebGL;
    public string[] sneezeTutorialDialogMobile;
    public string[] computerDialog;

    [SerializeField] private UIManager _uIManager;
    [SerializeField] private PlayerController _player;
    [SerializeField] private PlayerSneeze _playerSneeze;
    [SerializeField] private Computer _computer;

    private bool initiateSneeze = false;
    private bool initiateComputerTrigger = false;

    public void DisplayNextDialogLine(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_uIManager.DisplayNextLine())
            {
                // End of dialog set
                _player.SwitchToPlayerActionMap();

                if (initiateSneeze)
                {
                    _playerSneeze.Sneeze();
                    initiateSneeze = false;
                    StartSneezeTutorialDialog();
                }
                else if (initiateComputerTrigger)
                {
                    _computer.EnableCollider();
                    initiateComputerTrigger = false;
                }
            }
        }
    }

    public void StartSneezeTriggerDialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialogue(sneezeTriggerDialog);
        _playerSneeze.isEnabled = true;
        initiateSneeze = true;
    }

    public void StartComputerDialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialogue(computerDialog);
    }

    private void Start()
    {
        //Set sneeze button visibility to false
        _uIManager.SetSneezeButtonVisibility(false);

        StartIntroDialog();
    }

    private void StartIntroDialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialogue(introDialog);
    }

    private void StartSneezeTutorialDialog()
    {
        _player.SwitchToUIActionMap();

        if (Application.platform == RuntimePlatform.Android)
        {
            _uIManager.SetSneezeButtonVisibility(true);
            _uIManager.StartDialogue(sneezeTutorialDialogMobile);
        }
        else
        {
            _uIManager.StartDialogue(sneezeTutorialDialogWebGL);
        }

        initiateComputerTrigger = true;
    }
}
