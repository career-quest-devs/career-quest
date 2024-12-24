using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableOfficeChair : MonoBehaviour
{
    public float pushForce = 20.0f; // Force to push the chair

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PushChair();
    }

    public void PushChair()
    {
        // Push the chair horizontally
        rb.AddForce(Vector2.left * pushForce, ForceMode2D.Impulse);
    }

    // Destroy the chair after it leaves the screen
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
