using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f;

    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    private PlayerController _player;

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _rb.velocity = new Vector2(_moveInput.x * _moveSpeed, _rb.velocity.y);

        if (_moveInput.x > 0)
        {
            _player.currentAnimator.SetBool("IsWalking", true);
            _player.currentAnimator.SetBool("IsGoingRight", true);
            _player.EnableDashAction(); // Enable dash action when moving
            _player.DisableSneezeAction(); // Disable sneeze action when moving
        }
        else if (_moveInput.x < 0)
        {
            _player.currentAnimator.SetBool("IsWalking", true);
            _player.currentAnimator.SetBool("IsGoingRight", false);
            _player.EnableDashAction(); // Enable dash action when moving
            _player.DisableSneezeAction(); // Disable sneeze action when moving
        }
        else
        {
            _player.currentAnimator.SetBool("IsWalking", false);
            _player.EnableSneezeAction(); // Enable sneeze action when not moving
            _player.DisableDashAction(); // Disable dash action when not moving
        }
    }
}
