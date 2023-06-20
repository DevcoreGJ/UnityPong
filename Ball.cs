using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        Launch();
    }
    
    // Update is called once per frame
    void Update()
    {

    }

    // This method resets the object's position and velocity, preparing for a new adventure!

    public void Reset()
    {
        rb.velocity = Vector2.zero; // Applying the brakes, bringing the object to a graceful stop.
        transform.position = startPosition; // Teleporting the object to its place of origin. Time to start over!
        Launch(); // Activating the launch function to unleash the power of my balls. Hold on tight!
    }


    // Launch is called before the first frame update
    private void Launch()
    {
        // Flip a coin to determine the direction
        float x = Random.Range(0, 2) == 0 ? -1 : 1; // Heads or tails? Let's go left or right!
        float y = Random.Range(0, 2) == 0 ? -1 : 1; // Up or down? Let's leave it to chance!

        // Set the velocity of the rigid body for epic motion
        rb.velocity = new Vector2(speed * x, speed * y); // Time to unleash the velocity!
    }
 
}
