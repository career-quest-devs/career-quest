using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Level2Manager : MonoBehaviour
{
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private PlayerController _player;
    [SerializeField] private PlayerSneeze _playerSneeze;
    [SerializeField] private PlayerWave _playerWave;

    private bool initiateWave = false;

    // Dialog collection
    private string[] _introDialog = new string[2]
    {
        "Alex: I got to be hurry!",
        "Alex: I remember the evalator is on the right."
    };
    private string[] _elevatorDialog = new string[3] { 
        "Oh crap!",
        "The elevator is not working again...",
        "I will be late, can I do something to fix it?"
    };
    private string[] _waveTutorialDialogMobile = new string[4]
    {
        "Oh! This is my neighbour Ben, he is a classical old guy.",
        "I'd better say hi to him.",
        "New skill acquired: Wave to say hi",
        "New skill acquired: Wave to say hi"
    };
    private string[] _waveTutorialDialog = new string[4]
    {
        "Oh! This is my neighbour Ben, he is a classical old guy.",
        "I'd better say hi to him.",
        "New skill acquired: Wave to say hi",
        "You can use Wave to say hi by pressing key W."
    };
 
    // Start is called before the first frame update
    void Start()
    {
        _playerSneeze.ActivateSneeze();

        if (Application.platform == RuntimePlatform.Android)
        {
            //Set visibility for mobile action buttons to false

            //_uIManager.SetSneezeButtonVisibility(false);
            _uIManager.SetWaveButtonVisibility(false);
            _uIManager.SetDashButtonVisibility(false);
            _uIManager.SetPickUpButtonVisibility(false);
            _uIManager.SetOpenButtonVisibility(false);
        }

        // Start timer based on time remaining from previous level
        _uIManager.SetTimeRemaining(DataTracker.GetInstance().GetTotalRemainingTime());
        _uIManager.StartTimer();

        StartIntroDialog();
    }
    private void StartIntroDialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(_introDialog);
    }

    public void StartElevatorDialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(_elevatorDialog);
    }

    public void StartDialog(string[] theTalk)
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(theTalk);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisplayNextDialogLine(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_uIManager.DisplayNextLine())
            {
                // End of dialog set
                _player.SwitchToPlayerActionMap();

                if (initiateWave)
                {
                    initiateWave = false;
                    //StartWaveTutorialDialog();
                }
            }
        }
    }

    public void StartWaveTutorialDialog()
    {
        _player.SwitchToUIActionMap();

        if (Application.platform == RuntimePlatform.Android)
        {
            _uIManager.SetWaveButtonVisibility(true);
            _uIManager.StartDialog(_waveTutorialDialogMobile);
        }
        else
        {
            _uIManager.StartDialog(_waveTutorialDialog);
        }

        initiateWave = true;
    }

    public void EndLevel()
    {
        DataTracker.GetInstance().SetTotalRemainingTime(_uIManager.StopTimerAndGetTimeRemaining());
    }

}
