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
    // The health of the player
    [SerializeField] public float playerHealth = 100f;
    // Reference to the MovementScript
    [SerializeField] Movement movementScript;
    //
    [SerializeField] public Transform PlayerLocation;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    private void Awake(){
        // Sets the size of the collision box based on the zoneOfControl variable
        movementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        PlayerLocation = GameObject.FindGameObjectWithTag("Player").transform;
    }
}