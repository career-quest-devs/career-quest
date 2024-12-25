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
    private PlayerInput _playerInput;
    private PlayerDash _playerDash;

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _player = GetComponent<PlayerController>();
        _playerDash = GetComponent<PlayerDash>();
    }

    // Update is called once per frame
    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (!_playerDash.IsDashing())
        {
            _rb.velocity = new Vector2(_moveInput.x * _moveSpeed, _rb.velocity.y);

            if (_moveInput.x > 0)
            {
                _player.currentAnimator.SetBool("IsWalking", true);
                _player.currentAnimator.SetBool("IsGoingRight", true);
                EnableDashActionWhenMoving();
                DisableSneezeActionWhenMoving();
                DisableWaveActionWhenMoving();
            }
            else if (_moveInput.x < 0)
            {
                _player.currentAnimator.SetBool("IsWalking", true);
                _player.currentAnimator.SetBool("IsGoingRight", false);
                EnableDashActionWhenMoving();
                DisableSneezeActionWhenMoving();
                DisableWaveActionWhenMoving();
            }
            else
            {
                _player.currentAnimator.SetBool("IsWalking", false);
                EnableSneezeActionWhenStopped();
                EnableWaveActionWhenStopped();
                DisableDashActionWhenStopped();
            }
        }
    }

    private void EnableSneezeActionWhenStopped()
    {
        if (_playerInput.currentActionMap.name == "Player")
        {
            _playerInput.actions["Sneeze"].Enable();
        }
    }

    private void DisableSneezeActionWhenMoving()
    {
        if (_playerInput.currentActionMap.name == "Player")
        {
            _playerInput.actions["Sneeze"].Disable();
        }
    }

    private void EnableWaveActionWhenStopped()
    {
        if (_playerInput.currentActionMap.name == "Player")
        {
            _playerInput.actions["Wave"].Enable();
        }
    }

    private void DisableWaveActionWhenMoving()
    {
        if (_playerInput.currentActionMap.name == "Player")
        {
            _playerInput.actions["Wave"].Disable();
        }
    }

    private void EnableDashActionWhenMoving()
    {
        if (_playerInput.currentActionMap.name == "Player")
        {
            _playerInput.actions["Dash"].Enable();
        }
    }

    private void DisableDashActionWhenStopped()
    {
        if (_playerInput.currentActionMap.name == "Player")
        {
            _playerInput.actions["Dash"].Disable();
        }
    }
}
