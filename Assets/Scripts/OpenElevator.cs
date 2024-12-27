using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OpenElevator : MonoBehaviour
{
    public UnityEvent OnElevatorReached;

    [SerializeField] private GameObject _levelEndCheckPoint;
    [SerializeField] private AudioClip _elevatorClip;

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
        yield return new WaitForSeconds(1.5f);

        SoundFXManager.Instance.PlaySoundFXClip(_elevatorClip, transform, 1f);

        yield return new WaitForSeconds(2.5f);

        _levelEndCheckPoint.SetActive(true);
    }
}
