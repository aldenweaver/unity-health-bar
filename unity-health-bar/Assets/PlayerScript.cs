// Based off inScope Studio's Health Bar Tutorial
// https://www.youtube.com/watch?v=NgftVg3idB4

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    public float speed;
 
    // Use this for initialization
    void Start()
    {   

    }

    // Update is called once per frame
    void Update()
    {   
        HandleMovement();
    }


    // Handles the player's movement
    private void HandleMovement()
    {   
        // Used for making the movement framerate independent
        float translation = speed * Time.deltaTime;

        // Used to move player based on axes the function fetches
        // Axes are multiplied by translation so that we move an amount in the game per second instead of per frame
        // Makes frame rate independent
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * translation, 0, Input.GetAxis("Vertical") * translation));
    }
}
