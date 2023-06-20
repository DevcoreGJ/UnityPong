using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLPaddle : MonoBehaviour
{
    public Transform ballTransform;
    public float speed;
    public Rigidbody2D rb;
    public float movementDelay = 0.2f; // Delay before the computer paddle starts moving towards the ball

    private float movementTimer; // Timer for movement delay

    private float movement; // Stores the movement value received from the Paddle script

    public Vector3 startPosition;

    private void Update()
    {
        if (movementTimer > 0)
        {
            movementTimer -= Time.deltaTime;
        }
        else
        {
            // Move the computer paddle towards the ball's y-position
            float targetY = ballTransform.position.y;
            float currentY = transform.position.y;
            float newY = Mathf.MoveTowards(currentY, targetY, speed * Time.deltaTime);

            // Apply the new position to the computer paddle
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }

        rb.velocity = new Vector2(rb.velocity.x, movement * speed);

        // Perform machine learning algorithm updates here
        // Update Q-values, train neural network, etc.
    }

    private void Start()
    {
        startPosition = transform.position;
        movementTimer = movementDelay;

        // Subscribe to the OnMovementEvent of the Paddle script
        Paddle.OnMovementEvent += SetMovement;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnMovementEvent when the CPUPaddle object is destroyed
        Paddle.OnMovementEvent -= SetMovement;
    }

    public void SetMovement(float movementValue)
        {
            movement = movementValue;
        }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
