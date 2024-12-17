using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSneeze : MonoBehaviour
{
    [HideInInspector]
    public bool isEnabled = false;

    private GameObject _nearbyClothes; // Tracks the clothes pile in range

    public void Sneeze()
    {
        if (isEnabled)
        {
            // Trigger player sneeze animation
            //_player.currentAnimator.SetBool("IsSneezing", true);

            // Trigger declutter animation on clothes
            if (_nearbyClothes != null)
            {
                ClothesPile clothesPile = _nearbyClothes.GetComponent<ClothesPile>();
                if (clothesPile != null)
                {
                    clothesPile.Declutter();
                }
            }
        }
    }

    public void OnSneeze(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Sneeze();
        }
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
}
