using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    public float horizontalDashForce = 10.0f;
    public float verticalDashForce = 10.0f;
    public float coolDownTime = 2.0f;

    private Rigidbody2D _rb;
    private PlayerController _player;
    private bool _isActive = false;
    private bool _isFacingRight = true;
    private bool _canDash = true;
    private bool _isDashing = false;

    public void ActivateDash()
    {
        _isActive = true;
    }

    public void DeactivateDash()
    {
        _isActive = false;
    }

    public bool IsDashing()
    {
        return _isDashing;
    }

    public void Dash()
    {
        if (_isActive && _canDash)
        {
            _isDashing = true;

            // Trigger player dash animation
            _player.currentAnimator.SetBool("IsDashing", true);

            // Determine the dash direction
            float horizontalDash = _isFacingRight ? horizontalDashForce : -horizontalDashForce;

            // Create a force vector
            Vector2 dashVector = new Vector2(horizontalDash, verticalDashForce);

            // Apply the force
            _rb.AddForce(dashVector, ForceMode2D.Impulse);

            // Start cooldown
            StartCoroutine(DashCooldown());

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("OfficeChair"))
        {
            _player.currentAnimator.SetBool("IsDashing", false);
            _isDashing = false;
        }
    }

    private IEnumerator DashCooldown()
    {
        _canDash = false;   // Prevent further dashing
        yield return new WaitForSeconds(coolDownTime);
        _canDash = true;    // Re-enable dashing
    }
}
