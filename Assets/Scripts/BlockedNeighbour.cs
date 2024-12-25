using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlockedNeighbour : MonoBehaviour
{
    public UnityEvent OnConversationIntiated;

    [SerializeField] string[] _smallTalk;
    [SerializeField] bool chatAndLeave;

    private Animator _neighbourAnimator;
    private SpriteRenderer _sr;

    // Start is called before the first frame update
    void Start()
    {
        _neighbourAnimator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Chat()
    {
        _neighbourAnimator.SetTrigger("ChatAndLeave");

        OnConversationIntiated?.Invoke();
        

        if (chatAndLeave) {
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<CapsuleCollider2D>().enabled = false;
        }
    }

    public void BlowedAway() {
        _neighbourAnimator.SetTrigger("BlowAway");
        StartCoroutine(FadeNeighbour(true));
    }


    IEnumerator FadeNeighbour(bool fadeAway)
    {
        yield return new WaitForSeconds(0.75f);
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                _sr.color = new Color(1, 1, 1, i);

                yield return null;
                this.gameObject.SetActive(false);
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                _sr.color = new Color(1, 1, 1, i);

                yield return null;
                this.gameObject.SetActive(true);
            }
        }
    }

}
