using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSneeze : MonoBehaviour
{
    public float declutterDelay = 0.5f;
    public float coolDownTime = 1.0f;

    [SerializeField] private AudioClip _sneezeClip;

    private PlayerController _player;
    private GameObject _nearbyClothes; // Tracks the clothes pile in range
    private GameObject _nearbyNeighbour;
    private bool _isActive = false;
    private bool _canSneeze = true;
    private bool _isSneezing = false;

    public void ActivateSneeze()
    {
        _isActive = true;
    }

    public void DeactivateSneeze()
    {
        _isActive = false;
    }

    public bool IsSneezing()
    {
        return _isSneezing;
    }

    public void Sneeze()
    {
        if (_isActive && _canSneeze)
        {
            _isSneezing = true;

            // Trigger player sneeze animation
            SoundFXManager.Instance.PlaySoundFXClip(_sneezeClip, transform, 1f);
            _player.currentAnimator.SetTrigger("Sneeze");

            // Trigger declutter animation on clothes if nearby
            if (_nearbyClothes != null)
            {
                StartCoroutine(DeclutterClothes());
            }

            // Trigger blow away neighbour animation if nearby
            if (_nearbyNeighbour != null)
            {
                StartCoroutine(BlowAwayNeighbour());
            }

            StartCoroutine(SneezeCooldown());

            // Update action/ability count in DataTracker
            DataTracker.GetInstance().IncrementAbility("Sneeze");
        }
    }

    public void OnSneeze(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Sneeze();
        }
    }

    private void Start()
    {
        _player = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detect if a clothes pile is within range
        if (collision.CompareTag("ClothesPile"))
        {
            _nearbyClothes = collision.gameObject;
        }

        if (collision.CompareTag("BlockedNeighbour"))
        {
            _nearbyNeighbour = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Reset if the player leaves the interaction range
        if (collision.CompareTag("ClothesPile"))
        {
            _nearbyClothes = null;
        }

        if (collision.CompareTag("BlockedNeighbour"))
        {
            _nearbyNeighbour = null;
        }
    }

    private IEnumerator DeclutterClothes()
    {
        yield return new WaitForSeconds(declutterDelay);

        try
        {
            ClothesPile clothesPile = _nearbyClothes.GetComponent<ClothesPile>();
            if (clothesPile != null)
            {
                clothesPile.Declutter();
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private IEnumerator BlowAwayNeighbour()
    {
        yield return new WaitForSeconds(declutterDelay);// use the same delay?

        try
        {
            BlockedNeighbour nearbyNeighbour = _nearbyNeighbour.GetComponent<BlockedNeighbour>();
            if (nearbyNeighbour != null)
            {
                nearbyNeighbour.BlowedAway();
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    private IEnumerator SneezeCooldown()
    {
        _canSneeze = false;
        yield return new WaitForSeconds(coolDownTime);
        _canSneeze = true;
        _isSneezing = false;
    }
}
