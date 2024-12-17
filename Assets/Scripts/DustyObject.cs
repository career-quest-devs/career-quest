using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DustyObject : MonoBehaviour
{
    public UnityEvent OnDustDisturbed;

    [SerializeField] private ParticleSystem _dustParticles;

    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_dustParticles.isPlaying)
        {
            _dustParticles.Play();
            _collider.enabled = false;
            OnDustDisturbed?.Invoke();
        }
    }
}
