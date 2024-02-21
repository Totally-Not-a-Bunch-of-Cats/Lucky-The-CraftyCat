using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class of all enemies that will hold universal information
/// </summary>
public class Enemy : MonoBehaviour
{   
    // Movement Speed of the Slime
    [SerializeField] protected float movementSpeed = 0.1f;
    // Health of the Slime
    [SerializeField] protected float health = 100f;
    // Damage the slime does
    [SerializeField] protected float damage = 25f;
    
    /// <summary>
    /// Moves the enemy towards the player
    /// </summary>
    public void MoveTowardsPlayer(GameObject player, float yOffset)
    {
        gameObject.transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, yOffset, player.transform.position.z), movementSpeed);
    }
    
}
