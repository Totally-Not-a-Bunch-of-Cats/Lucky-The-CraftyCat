using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// This is the parent class to resource nodes that will common functionality and variable
/// </summary>
public class ResourceNode : MonoBehaviour
{
    // Number of times the resource node can be harvested
    [SerializeField] public int numberOfUses = 5;
    // List of the items that the resource node can be harvested for
    [SerializeField] public Item[] loot;

    // Reduces the current amount of uses the node has left
    public void DecreaseNodeUses()
    {
        numberOfUses --;
    }
}
