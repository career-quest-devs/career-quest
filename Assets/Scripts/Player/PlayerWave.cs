using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWave : MonoBehaviour
{
    [SerializeField] private float _cleanDelay = 0.5f;

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

            StartCoroutine(CleanWhiteboard());

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

    private IEnumerator CleanWhiteboard()
    {
        yield return new WaitForSeconds(_cleanDelay);

        if (_nearbyWhiteboard != null)
        {
            _nearbyWhiteboard.CleanWhiteboard();
        }
    }
}
