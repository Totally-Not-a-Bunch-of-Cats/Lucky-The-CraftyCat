using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Controls the movements of the player model
/// </summary>
public class Movement : MonoBehaviour
{
    // Reference to PlayerManager
    [SerializeField] PlayerManager playerStats;

    // Checking for if the player is moving diagonal
    private bool vertical = false;
    private bool horizontal = false;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Sets the size of the collision box based on the zoneOfControl variable
        playerStats = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerManager>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // When the user presses the w key or up arrow
        if(Input.GetButton("Forward"))
        {
            Forward();
            vertical = true;
        }
        else if(Input.GetButtonUp("Forward"))
        {
            vertical = false;
        }

        // When the user presses the s key or down arrow
        if(Input.GetButton("Backward"))
        {
            Backward();
            vertical = true;
        }
        else if(Input.GetButtonUp("Backward"))
        {
            vertical = false;
        }

        // When the user presses the a key or left arrow
        if(Input.GetButton("Left"))
        {
            Left();
            horizontal = true;
        }
        else if(Input.GetButtonUp("Left"))
        {
            horizontal = false;
        }

        // When the user presses the d key or right arrow
        if(Input.GetButton("Right"))
        {
            Right();
            horizontal = true;
        }
        else if(Input.GetButtonUp("Right"))
        {
            horizontal = false;
        }

        // When the user presses the space
        if(Input.GetButtonDown("Jump") && gameObject.transform.position.y == 1)
        {
            Jump();
        }

        // When the user presses either shift
        if(Input.GetButton("Run"))
        {
            Run();
        }
        else
        {
            playerStats.movementSpeed = 0.01f;
        }

        // If the Player is moving at a horizontal
        if(horizontal && vertical)
        {
            playerStats.movementSpeed = 0.005f;
        }
        else
        {
            playerStats.movementSpeed = 0.01f;
        }
    }

    /// <summary>
    /// Moves the player forward/up
    /// </summary>
    void Forward()
    {
        Vector3 forwardTransform = new Vector3(0, 0, playerStats.movementSpeed);
        gameObject.transform.position += forwardTransform;
    }

    /// <summary>
    /// Moves the player backward/down
    /// </summary>
    void Backward()
    {
        Vector3 backwardTransform = new Vector3(0, 0, -playerStats.movementSpeed);
        gameObject.transform.position += backwardTransform;
    }

    /// <summary>
    /// Moves the player to the left
    /// </summary>
    void Left()
    {
        Vector3 leftTransform = new Vector3(-playerStats.movementSpeed, 0, 0);
        gameObject.transform.position += leftTransform;
    }

    /// <summary>
    /// Moves the player to the right
    /// </summary>
    void Right()
    {
        Vector3 rightTransform = new Vector3(playerStats.movementSpeed, 0, 0);
        gameObject.transform.position += rightTransform;
    }

    /// <summary>
    /// Makes the player jump
    /// </summary>
    void Jump()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * playerStats.jumpHeight, ForceMode.Impulse);
    }

    /// <summary>
    /// Makes the player move at double speed
    /// </summary>
    void Run()
    {
        playerStats.movementSpeed = 0.02f;
    }
}
