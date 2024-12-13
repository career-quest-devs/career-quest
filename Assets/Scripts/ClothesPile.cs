using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothesPile : MonoBehaviour
{
    private Animator _clothesAnimator;

    public void Declutter()
    {
        // Trigger the declutter animation
        _clothesAnimator.SetBool("IsDecluttered", true);

        // Optionally, disable interaction after decluttering
        GetComponent<Collider2D>().enabled = false;
    }

    private void Start()
    {
        _clothesAnimator = GetComponent<Animator>();
    }
}
