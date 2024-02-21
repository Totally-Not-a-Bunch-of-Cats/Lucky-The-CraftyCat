using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the behavior of the slime enemy
/// Chases the player across the map
/// </summary>
public class Slime : Enemy
{
    // The player model
    [SerializeField] private GameObject player;
    
    /// <summary>
    /// Initializing Slime
    /// </summary>
    public Slime()
    {
        // Movement Speed of the Slime
        movementSpeed = 0.005f;
        // Health of the Slime
        health = 100f;
        // Damage the slime does
        damage = 25f;
    }

    /// <summary>
    /// Called On the Intialization of Script
    /// </summary>
    private void Start()
    {
        // Finds the player object in the scene when script is initialized
        player = GameObject.FindGameObjectWithTag("Player");
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Moves towards the players using the moveTowardsPlayer function in the parent script
        MoveTowardsPlayer(player, 0);
    }
}
