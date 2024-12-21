using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWave : MonoBehaviour
{
    private PlayerController _player;
    private bool _isActive = false;

    public void ActivateWave()
    {
        _isActive = true;
    }

    public void Wave()
    {
        if (_isActive)
        {
            // Trigger player wave animation
            _player.currentAnimator.SetTrigger("Wave");
        }
    }

    public void OnWave(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Wave();
        }
    }

    private void Start()
    {
        _player = GetComponent<PlayerController>();
    }
}
