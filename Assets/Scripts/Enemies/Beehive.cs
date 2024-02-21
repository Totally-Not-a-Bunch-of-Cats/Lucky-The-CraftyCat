using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the logic of the Beehive
/// Handles the logic of the bees attached to it by making them angry if the player enters it trigger zone
/// </summary>
public class Beehive : MonoBehaviour
{
    // List of Bees that hover around the beehive
    [SerializeField] private GameObject[] bees;
    // Size of Zone of  Control of the Beehive
    [SerializeField] private float zoneOfControl = 16.0f;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Sets the size of the collision box based on the zoneOfControl variable
        gameObject.GetComponent<BoxCollider>().size = new Vector3(zoneOfControl, 4, zoneOfControl);
    }
    
    /// <summary>
    /// When the Player Enters its zone of control make the bees angry
    /// </summary>
    /// <param name="other"> This is the object colliding looking for the player's collision box </param>
    private void OnTriggerEnter(Collider other) {
        // For each bee that is attached to the beehive
        for(int i = 0; i <= bees.Length - 1; i++)
        {
            // If the player is colliding with the box make the bees aggro
            if(other == GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>())
            {
                bees[i].GetComponent<Bee>().SetisAngry(true);
            }
        }
    }

    /// <summary>
    /// When the Player Exits its zone of control makes the bees docile
    /// </summary>
    /// <param name="other"> This is the object collding looking for the player's collision box</param>
    private void OnTriggerExit(Collider other) {
        // For each bee that is attached to the beehive
        for(int i = 0; i <= bees.Length - 1; i++)
        {
            // If the player has left the collision box of the beehive make the bees docile
            if(other == GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>())
            {
                bees[i].GetComponent<Bee>().SetisAngry(false);
                bees[i].GetComponent<Bee>().returnToStart = true;
            }
        }
    }
}
