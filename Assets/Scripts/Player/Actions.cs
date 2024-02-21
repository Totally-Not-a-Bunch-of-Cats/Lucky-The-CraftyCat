using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the actions the player can do
/// </summary>
public class Actions : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // When the user presses e
        if(Input.GetButton("Interact"))
        {
            //Interact();
        }

        // When the user presse q
        if(Input.GetButton("Use"))
        {

        }
    }
}
