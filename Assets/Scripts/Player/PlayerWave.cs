using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWave : MonoBehaviour
{
    [SerializeField] private float _sayingHiDelay = 0.5f;

    private PlayerController _player;
    private GameObject _nearbyNeighbour;
    private bool _isActive = false;

    public void ActivateWave()
    {
        _isActive = true;
    }

    public void Wave()
    {
        if (_isActive)
        {
            // Trigger player wave animation
            _player.currentAnimator.SetTrigger("Wave");

            // Trigger declutter animation on clothes
            if (_nearbyNeighbour != null)
            {
                StartCoroutine(SayHiToNeighbour());
            }
        }
    }

    public void OnWave(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Wave();
        }
    }

    private void Start()
    {
        _player = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detect if a clothes pile is within range
        if (collision.CompareTag("BlockedNeighbour"))
        {
            _nearbyNeighbour = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Reset if the player leaves the interaction range
        if (collision.CompareTag("BlockedNeighbour"))
        {
            _nearbyNeighbour = null;
        }
    }

    private IEnumerator SayHiToNeighbour()
    {
        yield return new WaitForSeconds(_sayingHiDelay);

        if (_nearbyNeighbour != null)
        {
            try
            {
                BlockedNeighbour blockedNeighbour = _nearbyNeighbour.GetComponent<BlockedNeighbour>();
                if (blockedNeighbour != null)
                {
                    blockedNeighbour.Chat();
                }
            }
            catch (Exception e)
            {
            }

            try {
                OpenElevator openElevator = _nearbyNeighbour.GetComponent<OpenElevator>();
                if (openElevator != null)
                {
                    openElevator.Open();
                }
            } catch (Exception e){ 
            }
            
        }
    }
}
