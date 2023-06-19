using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Launch();
    }
    
    // Update is called once per frame
    void Update()
    {

    }
   
    // Launch is called before the first frame update
    private void Launch()
    {
        float x = Random.Range(0, 2) == ? -1 : 1;
        float y = Random.Range(0, 2) == ? -1 : 1;
        // Generates a random integer
        // If the random value is equal to 0, the ternary operator ?
        // Assigns the value -1 to the variable.
        // Otherwise it assigns 1 to variable.

        rb.velocity = new Vector2(speed * x, speed * y); // velocity property of rigid body component in game.
        // creates a new Vector2 object
        // x and y are variables that determine the direction of the velocity
        // values of 1 or - 1
    }
 
}
