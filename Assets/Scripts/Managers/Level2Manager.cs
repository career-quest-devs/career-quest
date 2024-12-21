using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Manager : MonoBehaviour
{
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private PlayerController _player;
    [SerializeField] private PlayerSneeze _playerSneeze;
    [SerializeField] private PlayerWave _playerWave;


    // Dialog collection
    private string[] _introDialog = new string[2]
    {
        "Alex: I got to be hurry!",
        "Alex: I remember the evalator is on the right."
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

        StartIntroDialog();
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
}
