using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Computer : MonoBehaviour
{
    public UnityEvent OnComputerLoggedIn;

    private Collider2D _collider;

    public void EnableCollider()
    {
        _collider.enabled = true;
    }

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnComputerLoggedIn?.Invoke();
            _collider.enabled = false;
        }
    }
}
