using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedNeighbour : MonoBehaviour
{
    private Animator _neighbourAnimator;
    private SpriteRenderer img;

    // Start is called before the first frame update
    void Start()
    {
        _neighbourAnimator = GetComponent<Animator>();
        img = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChatAndLeave()
    {
        // Trigger the declutter animation
        _neighbourAnimator.SetTrigger("Declutter");

        //Reveal hidden item if exist
        //if (hiddenItem != null)
        //{
            StartCoroutine(HideNeighbour());
        //}

        // Disable interaction after decluttering
        GetComponent<Collider2D>().enabled = false;

    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }

private IEnumerator HideNeighbour()
    {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
}
