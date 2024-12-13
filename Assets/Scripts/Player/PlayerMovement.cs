using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f;

    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    private Animator _playerAnimator;

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
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
            _playerAnimator.SetBool("isWalking", true);
            _playerAnimator.SetBool("isGoingRight", true);
        }
        else if (_moveInput.x < 0)
        {
            _playerAnimator.SetBool("isWalking", true);
            _playerAnimator.SetBool("isGoingRight", false);
        }
        else
        {
            _playerAnimator.SetBool("isWalking", false);
        }

    }
}
