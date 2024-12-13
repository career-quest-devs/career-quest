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
        _clothesAnimator.SetBool("IsDecluttering", true);

        //Reveal hidden item if exist
        if (hiddenItem != null)
        {
            StartCoroutine(RevealHiddenItem());
        }

        // Optionally, disable interaction after decluttering
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
