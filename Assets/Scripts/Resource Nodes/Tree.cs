using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// This manages the tree resource node logic
/// </summary>
public class Tree : ResourceNode
{
    // Reference to the acorn scriptableobject
    [SerializeField] public Item Acorn;
    // Reference to the wood scriptableobject
    [SerializeField] public Item Wood;
    // Variable holding if the player is in the trigger box of the tree
    private bool playerInTrigger = false;

    /// <summary>
    /// Constructor for the tree class
    /// </summary>
    public Tree()
    {
        numberOfUses = 5;
        loot = new Item[2];
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    public void Start()
    {
        loot = new Item[2];
        loot[0] = Acorn;
        loot[1] = Wood;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    public void Update()
    {
        // If the player interacts with the tree
        if(playerInTrigger)
        {
            if(Input.GetButtonDown("Interact") && numberOfUses > 0)
            {
                DecreaseNodeUses();
                GenerateLoot();
            }
        }
    }

    /// <summary>
    /// Generate and spits out the loot for the player to pick up
    /// </summary>
    private void GenerateLoot()
    {
        // Generates a random number
        float randomNumber = UnityEngine.Random.Range(1, 101);
        // odds out of hundred, more likely to hit wood
        if(randomNumber < 70)
        {
            // X position of the resource bag that takes into account not spawning inside the resource node object
            float x = UnityEngine.Random.Range(-3, 3);
            float z = UnityEngine.Random.Range(-3, 0);
            if (x < 0.5 && x > 0 && z < -0.7)
            {
                x = 0.6f;
            }
            else if(x > -0.5 && x <= 0 && z < -0.7)
            {
                x = -0.6f;
            }
            float y = 1.22f;
            Vector3 randomPosition = new Vector3(x + transform.position.x, y, z + transform.position.z);
            // Instantiates the bag inside the scene
            Instantiate(loot[1].itemObject, randomPosition, Quaternion.identity);
        }
        else
        {
            // X position of the resource bag that takes into account not spawning inside the resource node object
            float x = UnityEngine.Random.Range(-3, 3);
            float z = UnityEngine.Random.Range(-3, 0);
            if (x < 0.5 && x > 0 && z < -0.7)
            {
                x = 0.6f;
            }
            else if(x > -0.5 && x <= 0 && z < -0.7)
            {
                x = -0.6f;
            }
            float y = 1.22f;
            Vector3 randomPosition = new Vector3(x + transform.position.x, y, z + transform.position.z);
            // Instantiates the bag inside the scene
            Instantiate(loot[0].itemObject, randomPosition, Quaternion.identity);
        }
    }

    /// <summary>
    /// When the player enters the trigger collider it flips the playerInTrigger bool on
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other) 
    {
        if(other == GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>())
        {
            playerInTrigger = true;
        }
    }

    /// <summary>
    /// When the player enters the trigger collider it flips the playerInTrigger bool on
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other) 
    {
        if(other == GameObject.FindGameObjectWithTag("Player").GetComponent<CapsuleCollider>())
        {
            playerInTrigger = false;
        }
    }
}
