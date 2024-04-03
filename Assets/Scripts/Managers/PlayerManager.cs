using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the logics of the Player
/// </summary>
public class PlayerManager : MonoBehaviour {
    // The speed at which the player moves
    [SerializeField] public float movementSpeed = 0.01f;
    // The height that the player can jump
    [SerializeField] public float jumpHeight = 2f;
    [SerializeField] public float HighJumpPower = 2f;
    [SerializeField] public float LongJumpPower = 2f;
    [SerializeField] public float GravityPower = 5f;
    // Reference to the MovementScript
    [SerializeField] Movement MovementScript;
    //players transform
    public Transform PlayerLocation;
    //number of hit a player can take before being sent home
    [SerializeField] int Health = 3;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Sets the size of the collision box based on the zoneOfControl variable
        PlayerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        MovementScript = PlayerLocation.GetComponent<Movement>();
    }
    /// <summary>
    /// returns player HP
    /// </summary>
    /// <returns></returns>
    public int GetPlayerHP()
    {
        return Health;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage"></param>
    public void DamagePlayer(int damage)
    {
        Health -= damage;
        Debug.Log("damage delt " + Health);
    }
}