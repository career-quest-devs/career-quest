using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesPile : MonoBehaviour
{
    public GameObject hiddenItem;

    private Animator _clothesAnimator;

    public void Declutter()
    {
        // Trigger the declutter animation
        _clothesAnimator.SetTrigger("Declutter");

        //Reveal hidden item if exist
        if (hiddenItem != null)
        {
            StartCoroutine(RevealHiddenItem());
        }

        // Disable interaction after decluttering
        GetComponent<Collider2D>().enabled = false;
    }

    private void Start()
    {
        _clothesAnimator = GetComponent<Animator>();
    }

    private IEnumerator RevealHiddenItem()
    {
        yield return new WaitForSeconds(0.5f);
        hiddenItem.SetActive(true);
    }
}
