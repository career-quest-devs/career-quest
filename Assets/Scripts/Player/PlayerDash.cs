using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    private PlayerController _player;
    private bool _isActive;

    public void ActivateDash()
    {
        _isActive = true;
    }

    public void Dash()
    {
        if (_isActive)
        {
            // Trigger player wave animation
            _player.currentAnimator.SetTrigger("Dash");
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Dash();
        }
    }

    private void Start()
    {
        _player = GetComponent<PlayerController>();
        _isActive = false;
    }
}
