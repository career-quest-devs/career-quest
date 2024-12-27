using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWave : MonoBehaviour
{
    public float cleanDelay = 0.5f;
    public float waveDelay = 0.5f;
    public float coolDownTime = 1.0f;

    [SerializeField] private AudioClip _waveClip;

    private PlayerController _player;
    private GameObject _nearbyNeighbour;
    private GameObject _nearbyElevator;
    private GameObject _nearbyWhiteboard;
    private bool _isActive = false;
    private bool _canWave = true;
    private bool _isWaving = false;

    public void ActivateWave()
    {
        _isActive = true;
    }

    public void DeactivateWave()
    {
        _isActive = false;
    }

    public bool IsWaving()
    {
        return _isWaving;
    }

    public void Wave()
    {
        if (_isActive && _canWave)
        {
            _isWaving = true;

            // Trigger player wave animation
            SoundFXManager.Instance.PlaySoundFXClip(_waveClip, transform, 1f);
            _player.currentAnimator.SetTrigger("Wave");

            if (_nearbyNeighbour != null)
            {
                WaveToNeighbour();
            }

            if (_nearbyElevator != null)
            {
                FixElevator();
            }

            if (_nearbyWhiteboard != null)
            {
                StartCoroutine(CleanWhiteboard());
            }

            StartCoroutine(WaveCooldown());

            // Update action/ability count in DataTracker
            DataTracker.GetInstance().IncrementAbility("Wave");
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
        // Detect if neighbour is within range
        if (collision.CompareTag("Neighbour") || collision.CompareTag("BlockedNeighbour"))
        {
            _nearbyNeighbour = collision.gameObject;
        }

        if (collision.CompareTag("Elevator"))
        {
            _nearbyElevator = collision.gameObject;
        }

        // Detect if whiteboard is within range
        if (collision.CompareTag("Whiteboard"))
        {
            _nearbyWhiteboard = collision.gameObject;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Reset if the player leaves the interaction range
        if (collision.CompareTag("Neighbour") || collision.CompareTag("BlockedNeighbour"))
        {
            _nearbyNeighbour = null;
        }

        if (collision.CompareTag("Elevator"))
        {
            _nearbyElevator = null;
        }

        if (collision.CompareTag("Whiteboard"))
        {
            _nearbyWhiteboard = null;
        }
    }

    private void WaveToNeighbour()
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
            Debug.LogException(e);
        }
    }

    private void FixElevator()
    {
        try
        {
            OpenElevator openElevator = _nearbyElevator.GetComponent<OpenElevator>();
            if (openElevator != null)
            {
                openElevator.Open();
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private IEnumerator CleanWhiteboard()
    {
        yield return new WaitForSeconds(cleanDelay);

        try
        {
            Whiteboard whiteboard = _nearbyWhiteboard.GetComponent<Whiteboard>();
            if (whiteboard != null)
            {
                whiteboard.CleanWhiteboard();
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private IEnumerator WaveCooldown()
    {
        _canWave = false;
        yield return new WaitForSeconds(coolDownTime);
        _canWave = true;
        _isWaving = false;
    }
}
