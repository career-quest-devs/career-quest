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

    private bool initiateSneeze = false;
    private bool initiateComputerTrigger = false;

    // Dialog collection
    private string[] _introDialog = new string[4]
    {
        "Alex: What a beautiful Sunday morning.",
        "Alex: I'm looking forward to a lazy, relaxing day before my big interview tomorrow.",
        "Alex: Wait, I just heard a reminder alert on my laptop.",
        "Alex: I better go check it out."
    };
    private string[] _sneezeTriggerDialog = new string[1]
    {
        "Alex: This shelf is really dusty. Ah... ah... ah... choo!"
    };
    private string[] _sneezeTutorialDialog = new string[2]
    {
        "New skill acquired: Super Sneeze",
        "You can use Super Sneeze by pressing spacebar."
    };
    private string[] _sneezeTutorialDialogMobile = new string[2]
    {
        "New skill acquired: Super Sneeze",
        "New skill acquired: Super Sneeze"
    };
    private string[] _computerDialog = new string[5]
    {
        "Computer: Appointment Alert - Interview at 10am today.",
        "Alex: What?!? Today is Monday!",
        "Alex: I better hurry if I want to get to my interview on time.",
        "Alex: I need to quickly find my tie, lucky socks and r�sum�.",
        "Alex: They have to be around here somewhere."
    };
    private string[] _pickUpDialog = new string[1]
    {
        "Press E to pick up item."
    };
    private string[] _openDoorDialog = new string[1]
    {
        "Press E to open the door."
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
        _uIManager.StartDialog(_sneezeTriggerDialog);
        _playerSneeze.ActivateSneeze();
        initiateSneeze = true;
    }

    public void StartComputerDialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(_computerDialog);
    }

    public void EnablePickUpInteraction()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            _uIManager.SetPickUpButtonVisibility(true);
        }
        else
        {
            _uIManager.StartDialog(_pickUpDialog);
        }
    }

    public void EnableOpenDoorInteraction()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            _uIManager.SetOpenButtonVisibility(true);
        }
        else
        {
            _uIManager.StartDialog(_openDoorDialog);
        }
    }

    private void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            //Set visibility for mobile action buttons to false
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
    }

    private void StartSneezeTutorialDialog()
    {
        _player.SwitchToUIActionMap();

        if (Application.platform == RuntimePlatform.Android)
        {
            _uIManager.SetSneezeButtonVisibility(true);
            _uIManager.StartDialog(_sneezeTutorialDialogMobile);
        }
        else
        {
            _uIManager.StartDialog(_sneezeTutorialDialog);
        }

        initiateComputerTrigger = true;
    }
}
