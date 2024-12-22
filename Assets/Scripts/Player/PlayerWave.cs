using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWave : MonoBehaviour
{
    private PlayerController _player;
    private Whiteboard _nearbyWhiteboard;
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

            if (_nearbyWhiteboard != null )
            {
                _nearbyWhiteboard.CleanWhiteboard();
            }

            // Update action/ability count in DataTracker
            DataTracker.GetInstance().IncrementAbility("Wave");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Whiteboard"))
        {
            _nearbyWhiteboard = collision.gameObject.GetComponent<Whiteboard>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Whiteboard"))
        {
            _nearbyWhiteboard = null;
        }
    }
}
