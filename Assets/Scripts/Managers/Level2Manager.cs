using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private PlayerController _player;
    [SerializeField] private PlayerSneeze _playerSneeze;
    [SerializeField] private PlayerWave _playerWave;

    private bool _activateWave = false;

    // Dialog collection
    private string[] _introDialog = new string[1]
    {
        "Alex: I have to get to the elevator down the hall as quickly as I can.",
    };
    private string[] _waveTutorialDialog = new string[5]
    {
        "Alex: This is my neighbour Ben.",
        "Alex: I should say a quick hello to him.",
        "Alex: Otherwise, I'll never hear the end of it.",
        "New skill acquired: Dynamic Wave",
        "You can use Dynamic Wave by pressing key W."
    };
    private string[] _waveTutorialDialogMobile = new string[5]
    {
        "Alex: This is my neighbour Ben.",
        "Alex: I should say a quick hello to him.",
        "Alex: Otherwise, I'll never hear the end of it.",
        "New skill acquired: Dynamic Wave",
        "You can use Dynamic Wave by pressing the new button on the right."
    };
    private string[] _neighbour1Dialog = new string[5]
    {
        "Alex: Good morning, Ben!",
        "Ben: Oh, good morning, Alex!",
        "Ben: Where are you off to today?",
        "Alex: I'm actually running late for my job interview.",
        "Ben: Well then, you better be on your way. Best of luck!"
    };
    private string[] _neighbour2Dialog = new string[6]
    {
        "Alex: Good morning, Carol!",
        "Alex: Working out in the hallway again?",
        "Carol: You know it. I'm hoping more people like you are going to join me.",
        "Carol: Come on, Alex. What do you say?",
        "Alex: I'll think about it. I really have to run right now as I'm late for my interview.",
        "Carol: Good luck. Until next time."
    };
    private string[] _neighbour3Dialog = new string[6]
    {
        "Alex: Good morning, Daniel!",
        "Daniel: ...",
        "Alex: GOOD MORNING, DANIEL!",
        "Daniel: ...",
        "Alex: He doesn't seem to hear me... or he is ignoring me.",
        "Alex: Hmmm..."
    };
    private string[] _elevatorDialog = new string[2] {
        "Alex: Oh crap! The elevator is not working again.",
        "Alex: I wonder if there is something I can do to fix it."
    };

    public void DisplayNextDialogLine(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_uIManager.DisplayNextLine())
            {
                // End of dialog set
                _player.SwitchToPlayerActionMap();

                if (_activateWave)
                {
                    _playerWave.ActivateWave();
                    _activateWave = false;
                }
            }
        }
    }

    public void StartNeighbour1Dialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(_neighbour1Dialog);
    }

    public void StartNeighbour2Dialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(_neighbour2Dialog);
    }

    public void StartNeighbour3Dialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(_neighbour3Dialog);
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

        _activateWave = true;
    }

    public void EndLevel()
    {
        DataTracker dataTracker = DataTracker.GetInstance();

        // Save game data to DataTracker
        dataTracker.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        dataTracker.SetTotalRemainingTime(_uIManager.StopTimerAndGetTimeRemaining());

        // Go to next scene
        SceneManager.LoadScene("MiniLevels");
    }

    private void Start()
    {
        // Activate skills learned in previous level
        _playerSneeze.ActivateSneeze();

        if (Application.platform == RuntimePlatform.Android)
        {
            // Set visibility for mobile action buttons
            _uIManager.SetSneezeButtonVisibility(true);
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
}
