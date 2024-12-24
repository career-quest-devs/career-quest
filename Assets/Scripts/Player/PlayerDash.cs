using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDash : MonoBehaviour
{
    public float horizontalDashForce = 10.0f;
    public float verticalDashForce = 10.0f;
    public float coolDownTime = 2.0f;
    public float doubleDashTimeLimit = 0.5f;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundCheckDistance = 0.15f;

    private Rigidbody2D _rb;
    private PlayerController _player;
    private bool _isActive = false;
    private bool _isFacingRight = true;
    private bool _canDash = true;
    private bool _isGrounded = true;

    public void ActivateDash()
    {
        _isActive = true;
    }

    public void DeactivateDash()
    {
        _isActive = false;
    }

    public void Dash()
    {
        if (_isActive && _canDash)
        {
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

        //GroundCheck();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("OfficeChair"))
        {
            _player.currentAnimator.SetBool("IsDashing", false);
        }
    }

    private IEnumerator DashCooldown()
    {
        _canDash = false;   // Prevent further dashing
        yield return new WaitForSeconds(coolDownTime);
        _canDash = true;    // Re-enable dashing
    }

    private void GroundCheck()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckDistance, _groundMask);
    }
}
