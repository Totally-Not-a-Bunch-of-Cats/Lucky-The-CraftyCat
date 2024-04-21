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
    [SerializeField] float LocalSpeed;
    [SerializeField] Vector3 DirectionOfMovement;
    [SerializeField] bool DirectionOfMovementCheck = true;
    [SerializeField] bool SpeedingUP = false;
    [SerializeField] bool CheckDirectionBool = false;
    [SerializeField] GameObject RunIdicator;
    [SerializeField] bool DashDelayBool = false;
    [SerializeField] bool SwipeDelayBool = false;
    [SerializeField] Animator Animator;

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
        LocalSpeed = PlayerStats.Speed;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (DirectionOfMovementCheck)
        {
            print("Called once");
            DirectionOfMovementCheck = false;
            StartCoroutine(PastDirection());
        }
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
        if(Input.GetButtonDown("Jump") && CheckGround()) 
        {
            Jump();
        }
        //dash
        if(Input.GetButtonDown("Dash") && !DashDelayBool)
        {
            DashDelayBool = true;
            Dash();
        }
        if (Input.GetButtonDown("invincible") && !SwipeDelayBool)
        {
            SwipeDelayBool = true;
            TailSwipe();
        }

        // If the Player is moving at a horizontal
        if (horizontal && vertical)
        {
            LocalSpeed = PlayerStats.Speed/2;
        }
        else
        {
            LocalSpeed = PlayerStats.Speed;
        }
        //tossed the player into the ground
        if(!CheckGround())
        {
            PlayerBody.AddForce(Vector3.down * PlayerStats.GravityPower, ForceMode.Acceleration);
        }
        //speed increase code kinda janky fix with state machiene
        if (PlayerBody.velocity.normalized.x <= DirectionOfMovement.x + PlayerStats.NormalVectorSpeedBoostBuffer || 
            PlayerBody.velocity.normalized.x >= -DirectionOfMovement.x + PlayerStats.NormalVectorSpeedBoostBuffer 
            && PlayerBody.velocity.normalized.x != 0 && !CheckDirectionBool)
        {
            CheckDirectionBool = true;
            SpeedingUP = true;
            StartCoroutine(CheckDirection());
        }
        else
        {
            SpeedingUP = false;
            RunIdicator.SetActive(false);
        }
    }

    /// <summary>
    /// Moves the player forward/up
    /// </summary>
    void Forward()
    {
        PlayerBody.AddForce(transform.forward * LocalSpeed, ForceMode.Force);
    }

    /// <summary>
    /// Moves the player backward/down
    /// </summary>
    void Backward()
    {
        PlayerBody.AddForce(-transform.forward * LocalSpeed, ForceMode.Force);
    }

    /// <summary>
    /// Moves the player to the left
    /// </summary>
    void Left()
    {
        PlayerBody.AddForce(new Vector3(-1, 0, 0) * LocalSpeed, ForceMode.Force);
    }

    /// <summary>
    /// Moves the player to the right
    /// </summary>
    void Right()
    {
        PlayerBody.AddForce(new Vector3(1, 0, 0) * LocalSpeed, ForceMode.Force);
    }

    /// <summary>
    /// Makes the player jump high jump.
    /// </summary>
    void Jump()
    {
        print("Jump");
        PlayerBody.AddForce(Vector3.up * PlayerStats.HighJumpPower, ForceMode.VelocityChange);
    }
    /// <summary>
    /// dashes the player in the direction of travel
    /// </summary>
    void Dash()
    {
        print("Dash");
        PlayerBody.AddForce(PlayerBody.velocity.normalized * PlayerStats.DashDistance, ForceMode.VelocityChange);
        StartCoroutine(DashDelay());
    }

    void TailSwipe()
    {
        print("Invic");
        //makes player invunerable and plays an animation
        SwipeDelayBool = false;
        PlayerStats.Invincible = true;
        StartCoroutine(InvincibleDelay());
        StartCoroutine(InvincibleDuration());
    }
    /// <summary>
    /// Checks to see if the player is touching the ground
    /// </summary>
    /// <returns></returns>
    bool CheckGround()
    {
        if (Physics.CheckSphere(GroundObject.transform.position, .5f, Ground))
        {
            return true;
        }
        return false;
    }

    IEnumerator PastDirection()
    {
        if(PlayerBody.velocity.normalized.x != 0)
        {
            DirectionOfMovement = PlayerBody.velocity.normalized;
            yield return new WaitForSeconds(1f);
        }
        DirectionOfMovementCheck = true;
    }

    IEnumerator CheckDirection()
    {
        yield return new WaitForSeconds(5f);
        if(SpeedingUP)
        {
            RunIdicator.SetActive(true);
            LocalSpeed *= PlayerStats.SpeedMultiplier;
            print("we are speeding");
        }
        CheckDirectionBool = false;
    }
    IEnumerator DashDelay()
    {
        yield return new WaitForSeconds(PlayerStats.DashDelay);
        DashDelayBool = false;
    }
    IEnumerator InvincibleDelay()
    {
        yield return new WaitForSeconds(PlayerStats.InvincibleDelay);
        DashDelayBool = false;
    }
    IEnumerator InvincibleDuration()
    {
        yield return new WaitForSeconds(PlayerStats.InvincibleDuration);
        PlayerStats.Invincible = false;
    }
}
