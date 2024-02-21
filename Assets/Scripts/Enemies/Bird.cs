using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Handles the control of the bird enemy
/// Sweeps across the screen in a line towards the player and past towards the edge of the screen
/// </summary>
public class Bird : Enemy
{
    // The player model
    [SerializeField] private GameObject player;
    
    // Width and Length of Room
    [SerializeField] private float width = 16f;
    [SerializeField] private float length = 16.0f;

    // A boolean for whether the bird is currently attacking
    [SerializeField] bool isAttacking = false;
    // A boolean for whether the attack is a vertical or horizontal line
    private bool attackHorizontal = false;
    // If the bird is ready to attack the player
    private bool readyToAttack = false;
    // If the bird is on the left side of the screen
    private bool isLeftSide = false;
    // If the bird is on the bottom side of the screen
    private bool isBottomSide = false;
    // If the bird is moving into position
    private bool isMoving = false;
    // The target position on the map for the bird movement
    private Vector3 targetPosition;
    
    /// <summary>
    /// Initializing Bird
    /// </summary>
    public Bird()
    {
        // Movement Speed of the Bird
        movementSpeed = 0.01f;
        // Health of the Bird
        health = 100f;
        // Damage the Bird does
        damage = 25f;
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Finds the player object in the scene when script is initialized
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // If the bird has not reached its position for attack
        if(!isMoving)
        {
            // If the bird is doing a vertical attack
            if(!attackHorizontal)
            {
                // If the bird is at the bottom of the map after an attack it will go towards the bottom of the map
                if(!isBottomSide)
                {
                    targetPosition = new Vector3(player.transform.position.x, 0, length);
                    isMoving = true;
                }
                // If the bird is at the top of the map after an attack it will go towards the top of the map
                else
                {
                    targetPosition = new Vector3(player.transform.position.x, 0, -length);
                    isMoving = true;
                }
                
            }
            // Else the bird is doing a horizontal attack
            else
            {
                // If the bird is on the right side of the map after an attack it will go towards the right side of the map
                if(!isLeftSide)
                {
                    targetPosition = new Vector3(width, 0, player.transform.position.z);
                    isMoving = true;
                }
                // Else the bird is on the left side of the map after an attack it will go towards the left side of the map
                else
                {
                    targetPosition = new Vector3(-width, 0, player.transform.position.z);
                    isMoving = true;
                }
            }
        }
        // Else if the bird has selected it line up spot and is ready to attack when it gets there
        else if(isMoving && readyToAttack)
        {
            // Moves towards the line up spot
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, movementSpeed/2);
            // If the bird has reached approximately where it need to attack from
            if((gameObject.transform.position.x >= targetPosition.x - 0.2 && gameObject.transform.position.x <= targetPosition.x + 0.2 && attackHorizontal) || (gameObject.transform.position.z >= targetPosition.z - 0.2 && gameObject.transform.position.z <= targetPosition.z + 0.2 && !attackHorizontal))
            {
                isAttacking = true;
                readyToAttack = false;
            }
        }

        // If the bird is in attack mode
        if(isAttacking)
        {          
            // If the bird is doing a vertical attack
            if(!attackHorizontal)
            {
                // If the bird is at the bottom of the map after an attack it will go towards the bottom of the map
                if(!isBottomSide)
                {
                    targetPosition = new Vector3(gameObject.transform.position.x, 0, -length);
                    isAttacking = false;
                }
                // If the bird is at the top of the map after an attack it will go towards the top of the map
                else
                {
                    targetPosition = new Vector3(gameObject.transform.position.x, 0, length);
                    isAttacking = false;
                }
                
            }
            // Else the bird is doing a horizontal attack
            else
            {
                // If the bird is on the right side of the map after an attack it will go towards the right side of the map
                if(!isLeftSide)
                {
                    targetPosition = new Vector3(-width, 0, gameObject.transform.position.z);
                    isAttacking = false;
                }
                // Else the bird is on the left side of the map after an attack it will go towards the left side of the map
                else
                {
                    targetPosition = new Vector3(width, 0, gameObject.transform.position.z);
                    isAttacking = false;
                }
            }
            
        }
        // Else if the bird has selected it target spot
        else
        {
            // Moves towards the target spot of the attack
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, movementSpeed);
            // If the bird has reached its target spot on the map after the attack
            if(gameObject.transform.position == targetPosition)
            {
                // Resetting the sequence as well as switching the angle the bird will attack from
                isAttacking = false;
                isMoving = false;
                attackHorizontal = !attackHorizontal;
                readyToAttack = true;

                // If the bird ended up on the left side of the map after a vertical attack
                if(gameObject.transform.position.x < 0)
                {
                    isLeftSide = true;
                }
                // Else the bird ended up on the right side of the map after a vertical attack
                else
                {
                    isLeftSide = false;
                }

                // If the bird ended up on the bottom side of the map after a horizontal attack
                if(gameObject.transform.position.z < 0)
                {
                    isBottomSide = true;
                }
                // Else the bird ended up on the top side of the map after a horizontal attack
                else
                {
                    isBottomSide = false;
                }
            }
        }
    }
}
