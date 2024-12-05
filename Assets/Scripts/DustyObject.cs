using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustyObject : MonoBehaviour
{
    [SerializeField] private ParticleSystem _dustParticles;

    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_dustParticles.isPlaying)
        {
            _dustParticles.Play();
            _boxCollider.enabled = false;
        }
    }
}
