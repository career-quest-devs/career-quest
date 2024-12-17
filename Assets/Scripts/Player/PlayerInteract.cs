using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public UnityEvent OnPickUpInRange;
    public UnityEvent OnDoorInRange;
    public Action OnInteracted;

    private IInteractable _interactableObject;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && _interactableObject != null)
        {
            _interactableObject.Interact();
            OnInteracted();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detect if an interactable item is within range
        if (collision.CompareTag("HiddenItem"))
        {
            _interactableObject = collision.gameObject.GetComponent<IInteractable>();
            OnPickUpInRange?.Invoke();
        }
        else if (collision.CompareTag("Door"))
        {
            _interactableObject = collision.gameObject.GetComponent<IInteractable>();
            OnDoorInRange?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Reset if the player leaves the interaction range
        if (collision.CompareTag("HiddenItem") || collision.CompareTag("Door"))
        {
            _interactableObject = null;
            OnInteracted();
        }
    }
}
