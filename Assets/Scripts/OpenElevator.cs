using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenElevator : MonoBehaviour
{
    public UnityEvent OnElevatorReached;

    [SerializeField] private GameObject _levelEndCheckPoint;
    [SerializeField] private AudioClip audioClip;

    private Animator _elevatorAnimator;
    
    public void Open()
    {
        
        _elevatorAnimator.SetTrigger("Open");
        StartCoroutine(addExitTrigger());
    }

    private void Start()
    {
        _elevatorAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnElevatorReached?.Invoke();
        }
    }

    private IEnumerator addExitTrigger()
    {
        yield return new WaitForSeconds(4.0f);
        SoundFXManager.Instance.PlaySoundFXClip(audioClip, transform, 1f);
        _levelEndCheckPoint.SetActive(true);
    }
}
