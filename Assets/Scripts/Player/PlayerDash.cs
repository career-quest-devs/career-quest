using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    public float horizontalDashForce = 10.0f;
    public float verticalDashForce = 8.0f;
    public float coolDownTime = 2.0f;
    public float doubleDashTimeLimit = 0.5f;

    private Rigidbody2D _rb;
    private PlayerController _player;
    private bool _isActive = false;
    private bool _isFacingRight = true;
    private bool _canDash = true;
    private int _dashCount = 0;
    private float _lastDashTime = 0.0f;

    public void ActivateDash()
    {
        _isActive = true;
    }

    public void Dash()
    {
        if (_isActive && _canDash)
        {
            // Check if the time between dashes exceeds the limit
            if (Time.time - _lastDashTime > doubleDashTimeLimit)
            {
                _dashCount = 0; // Reset dash count if too much time passed
            }

            // Increment dash count
            _dashCount++;
            _lastDashTime = Time.time;

            // Trigger player dash animation
            _player.currentAnimator.SetTrigger("Dash");

            // Determine the dash direction
            float horizontalDash = _isFacingRight ? horizontalDashForce : -horizontalDashForce;

            // Create a force vector
            Vector2 dashVector = new Vector2(horizontalDash, verticalDashForce);

            // Apply the force
            _rb.AddForce(dashVector, ForceMode2D.Impulse);

            // If the second dash is performed, start the cooldown
            if (_dashCount >= 2)
            {
                StartCoroutine(DashCooldown());
            }

            // Update action/ability count in DataTracker
            DataTracker.GetInstance().IncrementAbility("Dash");
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
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Update direction based on player movement or input
        if (_rb.velocity.x > 0.1f)
        {
            _isFacingRight = true;
        }
        else if (_rb.velocity.x < -0.1f)
        {
            _isFacingRight = false;
        }
    }

    private IEnumerator DashCooldown()
    {
        _canDash = false;   // Prevent further dashing
        _dashCount = 0;     // Reset dash count
        yield return new WaitForSeconds(coolDownTime);
        _canDash = true;    // Re-enable dashing
    }
}
