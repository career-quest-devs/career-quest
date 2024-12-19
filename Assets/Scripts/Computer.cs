using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Computer : MonoBehaviour
{
    public UnityEvent OnComputerLoggedIn;

    private BoxCollider2D _collider;
    private Animator _laptopAnimator;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _laptopAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Play computer animation
            _laptopAnimator.SetTrigger("LaptopStart");

            OnComputerLoggedIn?.Invoke();
            _collider.enabled = false;
        }
    }
}
