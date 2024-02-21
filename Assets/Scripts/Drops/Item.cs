using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Holds the information of the items to then be used in inventory and equipment
/// Has images for inventory and while on the ground
/// Name and Description
/// As well as an option to be stacked and category it fits into
/// </summary>
[CreateAssetMenu]
public class Item : ScriptableObject
{
    // The inventory image
    [Tooltip("The image that will be displayed when the item is in inventory")]
    public Sprite inventoryImage;

    // The ground image
    [Tooltip("This is the object that the will be on the ground to display the item")]
    public GameObject itemObject;

    // The Name of the item
    [Tooltip("Name of the item")]
    public String nameOfItem;

    // The category of items
    [Tooltip("The Category of the item")]
    public Category categoryOfItem;

    // The Name of the item
    [Tooltip("Description of the item")]
    public String descriptionOfItem;

    // The amount the item can be stacked
    [Range(1, 30)]
    [Tooltip("Max amount item can be stacked")]
    public int stackSize = 1;
}

// An enum to decide the category options of the items in the game
public enum Category
{
    Equipment,
    Trap,
    Tool,
    Material,
    Valuable
}
