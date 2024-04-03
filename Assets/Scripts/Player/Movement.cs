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
    [SerializeField] PlayerManager PlayerStats;
    [SerializeField] Rigidbody PlayerBody;
    [SerializeField] GameObject GroundObject;
    [SerializeField] LayerMask Ground;

    // Checking for if the player is moving diagonal
    private bool vertical = false;
    private bool horizontal = false;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Sets the size of the collision box based on the zoneOfControl variable
        PlayerStats = GameObject.FindGameObjectWithTag("GameController").GetComponent<PlayerManager>();
        PlayerBody = transform.GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // When the user presses the w key or up arrow
        if (Input.GetButton("Forward"))
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
            CheckGround();
            Jump();
        }

        // When the user presses either shift
        if(Input.GetButton("Run"))
        {
            
        }
        else
        {
            PlayerStats.movementSpeed = 0.01f;
        }

        // If the Player is moving at a horizontal
        if(horizontal && vertical)
        {
            PlayerStats.movementSpeed = 0.005f;
        }
        else
        {
            PlayerStats.movementSpeed = 0.01f;
        }
        //tossed the player into the ground
        if(!CheckGround())
        {
            PlayerBody.AddForce(Vector3.down * PlayerStats.GravityPower, ForceMode.Acceleration);
        }
    }

    /// <summary>
    /// Moves the player forward/up
    /// </summary>
    void Forward()
    {
        //Vector3 forwardTransform = new Vector3(0, 0, playerStats.movementSpeed);
        //gameObject.transform.position += forwardTransform;
        PlayerBody.AddForce(transform.forward * 1000, ForceMode.Force);
    }

    /// <summary>
    /// Moves the player backward/down
    /// </summary>
    void Backward()
    {
        //Vector3 backwardTransform = new Vector3(0, 0, -playerStats.movementSpeed);
        //gameObject.transform.position += backwardTransform;
        PlayerBody.AddForce(-transform.forward * 1000, ForceMode.Force);
    }

    /// <summary>
    /// Moves the player to the left
    /// </summary>
    void Left()
    {
        //Vector3 leftTransform = new Vector3(-playerStats.movementSpeed, 0, 0);
        //gameObject.transform.position += leftTransform;
        PlayerBody.AddForce(new Vector3(-1, 0, 0) * 1000, ForceMode.Force);
    }

    /// <summary>
    /// Moves the player to the right
    /// </summary>
    void Right()
    {
        //Vector3 rightTransform = new Vector3(playerStats.movementSpeed, 0, 0);
        //gameObject.transform.position += rightTransform;
        PlayerBody.AddForce(new Vector3(1, 0, 0) * 1000, ForceMode.Force);
    }

    /// <summary>
    /// Makes the player jump either a long jump or a high jump
    /// </summary>
    void Jump()
    {
        //standing jump
        if(PlayerBody.velocity.magnitude < 5)
        {
            print("high Jump");
            PlayerBody.AddForce(Vector3.up * 40, ForceMode.VelocityChange); //VelocityChange makes the jump up feel real powerful since it ignores mass
        }
        else
        {
            print("long Jump");
            //ads forces up and in the direction the player is moving
            Vector3 dir = PlayerBody.velocity.normalized; //direction vector
            dir *= 2; //makes the lerch in the direction of travel more pronounced
            dir.y = 1; //makes the player actualy jump up
            PlayerBody.AddForce(dir * 2000, ForceMode.Impulse); //impulse force type becuase its a burst of moevemnt
        }
    }
    bool CheckGround()
    {
        if (Physics.CheckSphere(GroundObject.transform.position, .5f, Ground))
        {
            return true;
        }
        return false;
    }
}
