using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUPaddle : MonoBehaviour
{
    public Transform ballTransform;
    public float moveSpeed = 5f;
    public float movementDelay = 0.2f; // Delay before the computer paddle starts moving towards the ball

    private float movementTimer; // Timer for movement delay

    private float movement; // Stores the movement value received from the Paddle script

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
            float newY = Mathf.MoveTowards(currentY, targetY, moveSpeed * Time.deltaTime);

            // Apply the new position to the computer paddle
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    private void Start()
    {
        movementTimer = movementDelay;

        // Subscribe to the OnMovementEvent of the Paddle script
        Paddle.OnMovementEvent += SetMovement;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnMovementEvent when the CPUPaddle object is destroyed
        Paddle.OnMovementEvent -= SetMovement;
    }

    private void SetMovement(float movementValue)
    {
        movement = movementValue;
    }

    // Use the movement value in the AI logic to control the CPUPaddle
}
