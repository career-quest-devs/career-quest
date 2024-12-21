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
    private string[] _waveTutorialDialogMobile = new string[2]
    {
        "New skill acquired: Wave to say hi",
        "New skill acquired: Wave to say hi"
    };
    private string[] _waveTutorialDialog = new string[2]
    {
        "New skill acquired: Wave to say hi",
        "You can use Wave to say hi by pressing spacebar."
    };

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            //Set visibility for mobile action buttons to false

            //_uIManager.SetSneezeButtonVisibility(false);
            _uIManager.SetWaveButtonVisibility(false);
            _uIManager.SetDashButtonVisibility(false);
            _uIManager.SetPickUpButtonVisibility(false);
            _uIManager.SetOpenButtonVisibility(false);
        }

        //StartIntroDialog();
    }
    private void StartIntroDialog()
    {
        _player.SwitchToUIActionMap();
        _uIManager.StartDialog(_introDialog);
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

}
