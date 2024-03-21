using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringMiniGame : MonoBehaviour
{
    public GameObject UI;
    public GameObject Green;
    public GameObject Handle;
    public Rock Rock;
    [SerializeField] int BreakRange = 5;
    //max and min green position
    [SerializeField] float GreenMinRange = -70;
    [SerializeField] float GreenMaxRange = 70;
    //handle ping pong distance
    [SerializeField] float HandleMinRange = -5;
    [SerializeField] float HandleMaxRange = 145;
    [SerializeField] float GreenRange;
    //grace range of handle checking green.
    [SerializeField] int GreenBufferRanger = 5;
    //Direction of bar
    [SerializeField] bool Reverse = false;



    private void Update()
    {
        //activates the UI action bar
        if (Input.GetButton("Interact") && UI.activeInHierarchy == false)
        {
            ActivateUI();
        }
        //turns off the ui when the player is to far away or the rock is out of uses
        if((GameManager.instance.playerManager.PlayerLocation.position - transform.position).magnitude > BreakRange || Rock.numberOfUses <= 0)
        {
            DeactivateUI();
        }
        if (Input.GetButtonDown("Interact") && UI.activeInHierarchy == true)
        {
            Debug.Log("checking");
            CheckGreen();
        }
        //the below if statments determine the direction of the handle
        if (Handle.transform.localPosition.x > HandleMaxRange)
        {
            Debug.Log("Reversing");
            Reverse = true;
        }
        if (Handle.transform.localPosition.x < HandleMinRange)
        {
            Debug.Log("noReverse");
            Reverse = false;
        }
    }
    /// <summary>
    /// Moves the handles position depending on which way it shold be going
    /// </summary>
    private void FixedUpdate()
    {
        if (Reverse)
        {
            Handle.transform.localPosition += new Vector3(-2, 0, 0);
        }
        else
        {
            Handle.transform.localPosition += new Vector3(2, 0, 0);
        }
    }
    /// <summary>
    /// Turns the action bar UI on
    /// </summary>
    void ActivateUI()
    {
        RollGreenPos();
        UI.SetActive(true);
    }
    /// <summary>
    /// Turns the action bar UI off
    /// </summary>
    void DeactivateUI()
    {
        UI.SetActive(false);
    }
    /// <summary>
    /// Randoms the location of the green area
    /// </summary>
    void RollGreenPos()
    {
        GreenRange = Random.Range(GreenMinRange, GreenMaxRange);
        Green.transform.localPosition = new Vector3(GreenRange, 0, 0);
    }
    /// <summary>
    /// checks to see if the bar was in the green 
    /// </summary>
    void CheckGreen()
    {
        if (Handle.transform.position.x - GreenBufferRanger <= Green.transform.position.x && Handle.transform.position.x + GreenBufferRanger >= Green.transform.position.x)
        {
            Debug.Log("We harvesting");
            Rock.Harvest();
            RollGreenPos();
        }
    }
}
