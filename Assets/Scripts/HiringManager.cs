using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HiringManager : MonoBehaviour
{
    public Transform position1;
    public Transform position2;
    public float speed = 5.5f;

    private Transform _target;
    private Animator _animator;
    private bool _isMoving = false;

    public void MoveToPosition2()
    {
        _isMoving = true;
        _target = position2;
        _animator.SetBool("IsWalking", true);
        _animator.SetBool("IsGoingRight", true);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _target = transform;
    }

    private void Update()
    {
        if (_isMoving)
        {
            // Move the Hiring Manager toward the _target
            transform.position = Vector3.MoveTowards(transform.position, _target.position, speed * Time.deltaTime);

            // If the Hiring Manager reaches the target, turn off walking animation
            if (Vector3.Distance(transform.position, _target.position) < 0.1f)
            {
                _animator.SetBool("IsWalking", false);
                _isMoving = false;
            }
        }
    }
}
