using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Level1Manager : MonoBehaviour
{
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private PlayerController _player;
    [SerializeField] private PlayerSneeze _playerSneeze;

    private bool _initiateSneeze = false;
    private bool _initiateTimer = false;

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
        "You can use Super Sneeze by pressing S."
    };
    private string[] _sneezeTutorialDialogMobile = new string[3]
    {
        "New skill acquired: Super Sneeze",
        "New skill acquired: Super Sneeze",
        "You can use Super Sneeze by pressing the new button on the right."
    };
    private string[] _computerDialog = new string[5]
    {
        "Computer: Appointment Alert - You have an interview in 30 minutes.",
        "Alex: Wait, what?!? Today is Monday!",
        "Alex: I better hurry if I want to get to my interview on time.",
        "Alex: I need to quickly find my tie, lucky socks and résumé.",
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
    private string[] _missingItemsDialog = new string[4]
    {
        "Does Alex have everything he needs for the interview?",
        "You may continue without these items...",
        "But there may be consequences.",
        "Choose wisely."
    };

    public void DisplayNextDialogLine(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_uIManager.DisplayNextLine())
            {
                // End of dialog set
                _player.SwitchToPlayerActionMap();

                if (_initiateSneeze)
                {
                    _playerSneeze.Sneeze();
                    _initiateSneeze = false;
                    StartSneezeTutorialDialog();
                }
                else if (_initiateTimer)
                {
                    _uIManager.StartTimer();
                    _initiateTimer = false;
                }
            }
        }
    }

    public void StartSneezeTriggerDialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(_sneezeTriggerDialog);
        _playerSneeze.ActivateSneeze();
        _initiateSneeze = true;
    }

    public void StartComputerDialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(_computerDialog);
        _initiateTimer = true;
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

    public void CheckForMissingItems()
    {
        if (_player.HasTie && _player.HasSocks && _player.HasResume)
        {
            return;
        }

        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(_missingItemsDialog);
    }

    public void EndLevel()
    {
        DataTracker dataTracker = DataTracker.GetInstance();
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        // Save game data to DataTracker
        dataTracker.lastSceneIndex = currentLevelIndex;
        dataTracker.SetLevelTime(1, _uIManager.GetLevelTimeTaken());
        dataTracker.SetTotalRemainingTime(_uIManager.StopTimerAndGetTimeRemaining());

        // Go to next scene
        SceneManager.LoadScene(currentLevelIndex + 1);
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

        _initiateTimer = true;
    }
}
