using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody rb;

    public static Player singleton;

    private float boundForce = 7f;

    private Vector3 startPosition;

    private void Awake()
    {
        if (singleton != null && singleton != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            singleton = this;
        }

        startPosition = rb.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.velocity = new Vector3(rb.velocity.x, boundForce, rb.velocity.z);

        if (collision.gameObject.CompareTag("LastPlatform") && !GameManager.levelCompleted)
        {
            GameManager.levelCompleted = true;
        }

        if (collision.gameObject.CompareTag("UnsafePart") && !GameManager.levelCompleted)
        {
            GameManager.isGameOver = true;
        }
    }

    public void RestartPosition()
    {
        rb.MovePosition(startPosition);
    }
}
