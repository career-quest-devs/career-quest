using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteracted;

    [SerializeField] private GameObject _doorClose;
    [SerializeField] private GameObject _doorOpen;

    private BoxCollider2D _collider;

    public void Interact()
    {
        _doorClose.SetActive(false);
        _doorOpen.SetActive(true);
        _collider.enabled = false;
        OnInteracted?.Invoke();
    }

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
    }
}
