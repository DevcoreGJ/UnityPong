using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;

    public delegate void MovementDelegate(float movementValue); // Delegate type for movement events
    public static event MovementDelegate OnMovementEvent; // Event triggered when movement input is detected

    private string verticalInputAxis; // Input axis for vertical movement

    private void Awake()
    {
        // Set the vertical input axis based on the player index
        verticalInputAxis = isPlayer1 ? "Vertical" : "Vertical2";
    }

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float movement = Input.GetAxisRaw(verticalInputAxis);

        rb.velocity = new Vector2(rb.velocity.x, movement * speed);

        // Trigger the OnMovementEvent with the movement value
        OnMovementEvent?.Invoke(movement);
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
/* In the refactored code, the key improvement lies in caching the input axis string based on the player index (isPlayer1). 
By doing this, we avoid repeatedly checking the isPlayer1 condition during each Update() call, resulting in better performance.

Other than that, minor adjustments have been made to the code for clarity and consistency, such as moving the assignment of 
startPosition to the Start() method and using the null-conditional operator (?.) when triggering the OnMovementEvent.

These optimizations should enhance the efficiency of the code without altering its intended functionality. */