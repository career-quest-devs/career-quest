using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstNeighbour : MonoBehaviour
{
    public UnityEvent SayingHi;

    //[SerializeField] private ParticleSystem _dustParticles;

    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _collider.enabled = false;
            SayingHi?.Invoke();
        }
    }
}
