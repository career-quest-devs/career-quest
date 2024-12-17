using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HiddenItem : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteracted;

    public void Interact()
    {
        this.gameObject.SetActive(false);
        OnInteracted?.Invoke();
    }
}
