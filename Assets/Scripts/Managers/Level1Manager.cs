using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Level1Manager : MonoBehaviour
{
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private PlayerController _player;
    [SerializeField] private PlayerSneeze _playerSneeze;
    [SerializeField] private Computer _computer;

    private bool initiateSneeze = false;
    private bool initiateComputerTrigger = false;

    // Dialog collection
    private string[] introDialog = new string[4]
    {
        "Alex: What a beautiful Sunday morning.",
        "Alex: I'm looking forward to a lazy, relaxing day before my big interview tomorrow.",
        "Alex: Wait, I just heard a reminder alert on my laptop.",
        "Alex: I better go check it out."
    };
    private string[] sneezeTriggerDialog = new string[1]
    {
        "Alex: This shelf is really dusty. Ah... ah... ah... choo!"
    };
    private string[] sneezeTutorialDialog = new string[2]
    {
        "New skill acquired: Super Sneeze",
        "You can use Super Sneeze by pressing spacebar."
    };
    private string[] sneezeTutorialDialogMobile = new string[1]
    {
        "New skill acquired: Super Sneeze"
    };
    private string[] computerDialog = new string[5]
    {
        "Computer: Appointment Alert - Interview at 10am today.",
        "Alex: What?!? Today is Monday!",
        "Alex: I better hurry if I want to get to my interview on time.",
        "Alex: I need to quickly find my tie, lucky socks and résumé.",
        "Alex: They have to be around here somewhere."
    };
    private string[] pickUpDialog = new string[1]
    {
        "Press E to pick up item."
    };

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
                    initiateComputerTrigger = false;
                }
            }
        }
    }

    public void StartSneezeTriggerDialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(sneezeTriggerDialog);
        _playerSneeze.isEnabled = true;
        initiateSneeze = true;
    }

    public void StartComputerDialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(computerDialog);
    }

    public void EnablePickUpInteraction()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            _uIManager.SetPickUpButtonVisibility(true);
        }
        else
        {
            _uIManager.StartDialog(pickUpDialog);
        }
    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            //Set visibility for mobile action buttons to false
            _uIManager.SetSneezeButtonVisibility(false);
            _uIManager.SetPickUpButtonVisibility(false);
        }

        StartIntroDialog();
    }

    private void StartIntroDialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(introDialog);
    }

    private void StartSneezeTutorialDialog()
    {
        _player.SwitchToUIActionMap();

        if (Application.platform == RuntimePlatform.Android)
        {
            _uIManager.SetSneezeButtonVisibility(true);
            _uIManager.StartDialog(sneezeTutorialDialogMobile);
        }
        else
        {
            _uIManager.StartDialog(sneezeTutorialDialog);
        }

        initiateComputerTrigger = true;
    }
}
