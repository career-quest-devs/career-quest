using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FirstNeighbour : MonoBehaviour
{
    public UnityEvent OnWaveTutorialStarted;

    private bool waveTutorialCompleted = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!waveTutorialCompleted)
            {
                OnWaveTutorialStarted?.Invoke();
                waveTutorialCompleted = true;
            }
        }
    }
}
