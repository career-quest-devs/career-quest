using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinalManager : MonoBehaviour
{
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private PlayerSneeze _playerSneeze;
    [SerializeField] private PlayerWave _playerWave;
    [SerializeField] private PlayerDash _playerDash;

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
    }
}
