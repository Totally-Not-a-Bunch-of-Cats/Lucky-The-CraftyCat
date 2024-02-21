using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the control of the bee enemies
/// Hovers around the beehive until the player reaches the zone of control of the hive in which it then chases the player until they leave the zone
/// </summary>
public class Bee : Enemy
{
    // The player model
    [SerializeField] private GameObject player;
    // The Hive model
    [SerializeField] private GameObject parentHive;

    // XYZ spread that the bee will hover around the beehive
    public float xSpread = 0.003f;
    public float yOffset;
    public float zSpread = 0.003f;

    // the point that the bees will hover around
    public Transform centerPoint;
    // Which way the bees will orbit
    public bool rotateClockwise;
    // Speed of Rotation
    public float rotSpeed = 1f;

    // Timer for rotating
    float timer = 0;

    // Is the Bee aggressive
    public bool isAngry = false;

    // Return the Bees to the hive when no longer angry
    public bool returnToStart = false;
    // Gets the bee current status
    public bool GetisAngry()
    {
        return isAngry;
    }
    // Sets the bee current status
    public void SetisAngry(bool Condition)
    {
        isAngry = Condition;
    }

    /// <summary>
    /// Initializing Bee when loaded
    /// </summary>
    public Bee()
    {
        // Movement Speed of the Bee
        movementSpeed = 0.01f;
        // Health of the Bee
        health = 100f;
        // Damage the Bee does
        damage = 25f;
    }

    /// <summary>
    /// Called on the intialization of the script
    /// </summary>
    private void Start()
    {
        // Finds the player object in the scene when intialized
        player = GameObject.FindGameObjectWithTag("Player");
        // Finds the parent hive of the bee and gets the centerpoint for the bee to hover around
        parentHive = gameObject.transform.parent.transform.parent.gameObject;
        centerPoint = parentHive.transform;
    }


    /// <summary>
    /// Update is called once per a frame
    /// </summary>
    void Update()
    {
        // Timer that handles the rotation of the bee
        timer += Time.deltaTime * rotSpeed;
        // If the bee has been aggro
        if(isAngry)
        {
            // Bee moves towards the player model using the moveTowardsPlayer function in parent script
            gameObject.transform.eulerAngles = new Vector3(90, 180, 0);
            MoveTowardsPlayer(player, 1.5f);
        }
        else
        {
            // If the bee is no longer aggro and isn't back to the beehive it will move to it before engaging its orbit
            if(returnToStart == true)
            {
                gameObject.transform.eulerAngles = new Vector3(90, 0, 0);
                gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(parentHive.transform.position.x, parentHive.transform.position.y + 2, parentHive.transform.position.z), movementSpeed);
            }
            else
            {
                Rotate();
                transform.LookAt(centerPoint);
            }
        }
        // If the bee has made it back to the hive its back to start
        if(gameObject.transform.position == new Vector3(parentHive.transform.position.x, parentHive.transform.position.y + 2, parentHive.transform.position.z))
        {
            returnToStart = false;
        }
    }

    /// <summary>
    /// Orbits the bee around the hive
    /// </summary>
    void Rotate() 
    {
        // If the bee orbits clockwise
        if(rotateClockwise)
        {
            float x = -Mathf.Cos(timer) * xSpread;
            float z = Mathf.Sin(timer) * zSpread;
            Vector3 pos = new Vector3(x, yOffset, z);
            transform.position = pos + centerPoint.position;
        }
        // If the bee orbits counterclockwise
        else
        {
            float x = Mathf.Cos(timer) * xSpread;
            float z = Mathf.Sin(timer) * zSpread;
            Vector3 pos = new Vector3(x, yOffset, z);
            transform.position = pos + centerPoint.position;
        }
    }
}
