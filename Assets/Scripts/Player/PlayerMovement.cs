using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5.0f;
    [SerializeField] private Animator Default;

    private Vector2 _moveInput;
    private Rigidbody2D _rb;

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
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
            Default.SetBool("isWalking", true);
            Default.SetBool("isGoingRight", true);
        }
        else if (_moveInput.x < 0)
        {
            Default.SetBool("isWalking", true);
            Default.SetBool("isGoingRight", false);
        }
        else
        {
            Default.SetBool("isWalking", false);
        }

    }
}
