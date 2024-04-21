using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the logics of the Player
/// </summary>
public class PlayerManager : MonoBehaviour {
    // The speed at which the player moves
    public float Speed = 100f;
    public float HighJumpPower = 10f;
    public float GravityPower = 5f;
    public float SpeedBonus = 10f;
    public float SpeedMultiplier = 1.15f;
    public float NormalVectorSpeedBoostBuffer = .05f;
    public float DashDistance = 40f;
    public float DashDelay = 1f;
    public float InvincibleDuration = 1f;
    public float InvincibleDelay = 5f;
    public bool Invincible = false;

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
        if(Invincible)
        {
            Debug.Log("player immune");
        }
        else
        {
            Health -= damage;
            Debug.Log("damage delt " + Health);
        }
    }
}