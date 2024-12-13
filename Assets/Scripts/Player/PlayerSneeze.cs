using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSneeze : MonoBehaviour
{
    private Animator _playerAnimator; // Assign the player's Animator in the Inspector
    private GameObject _nearbyClothes; // Tracks the clothes pile in range

    public void OnSneeze()
    {
        // Trigger player sneeze animation
        //_playerAnimator.SetBool("IsSneezing", true);

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

    private void Start()
    {
        _playerAnimator = GetComponent<Animator>();
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
