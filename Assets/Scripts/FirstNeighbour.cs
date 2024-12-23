using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstNeighbour : MonoBehaviour
{
    public UnityEvent LearningSayingHi;

    private bool hadtutorial;

    //[SerializeField] private ParticleSystem _dustParticles;

    private BoxCollider2D _ConversationCollider;
    private CapsuleCollider2D _BlockCollider;

    private void Start()
    {
        hadtutorial = false;
        _ConversationCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!hadtutorial)
            {
                LearningSayingHi?.Invoke();
                hadtutorial = true;
            }
            else {
                //_ConversationCollider.enabled = false;
            }
            
            
        }
    }
}
