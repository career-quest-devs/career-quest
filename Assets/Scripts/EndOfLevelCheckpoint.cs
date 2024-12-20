using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndOfLevelCheckpoint : MonoBehaviour
{
    public UnityEvent OnLevelEnded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Zone entered");
            OnLevelEnded?.Invoke();
        }
    }
}
