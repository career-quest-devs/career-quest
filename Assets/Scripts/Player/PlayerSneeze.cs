using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSneeze : MonoBehaviour
{
    [SerializeField] private float _declutterDelay = 0.5f;

    private PlayerController _player;
    private GameObject _nearbyClothes; // Tracks the clothes pile in range
    private bool _isActive = false;

    public void ActivateSneeze()
    {
        _isActive = true;
    }

    public void Sneeze()
    {
        if (_isActive)
        {
            // Trigger player sneeze animation
            _player.currentAnimator.SetTrigger("Sneeze");

            // Trigger declutter animation on clothes
            if (_nearbyClothes != null)
            {
                StartCoroutine(DeclutterClothes());
            }

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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Reset if the player leaves the interaction range
        if (collision.CompareTag("ClothesPile"))
        {
            _nearbyClothes = null;
        }
    }

    private IEnumerator DeclutterClothes()
    {
        yield return new WaitForSeconds(_declutterDelay);

        ClothesPile clothesPile = _nearbyClothes.GetComponent<ClothesPile>();
        if (clothesPile != null)
        {
            clothesPile.Declutter();
        }
    }
}
