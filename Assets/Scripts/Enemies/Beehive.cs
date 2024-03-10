//Aaron Tweden 3/9/24
using System.Collections;
using TMPro;
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
    //time till the bees get angry
    [SerializeField] private int BeeAngerDelay = 5;
    [SerializeField] private int DetectionRange = 15;
    //bool to check if the bees are angry
    [SerializeField] private bool BeesAngry = false;
    [SerializeField] private bool BeesCharging = false;
    //indicator for angry
    [SerializeField] private GameObject ExclamationMark;


    /// <summary>
    /// handles the bees getting angry and getting not angry.
    /// </summary>
    private void Update()
    {
        Debug.Log(BeesCharging);
        if ((GameManager.instance.playerManager.PlayerLocation.position - transform.position).magnitude < DetectionRange && !BeesCharging)
        {
            BeesCharging = true;
            Debug.Log("Getting Angry");
            StartCoroutine(GettingAngry());
        }
        if(BeesCharging && (GameManager.instance.playerManager.PlayerLocation.position - transform.position).magnitude > DetectionRange)
        {
            Debug.Log("No Longer Getting angry");
            ExclamationMark.SetActive(false);
            BeesCharging = false;
            StopAllCoroutines();
        }
        if(BeesAngry && (GameManager.instance.playerManager.PlayerLocation.position - transform.position).magnitude > DetectionRange)
        {
            Debug.Log("Bees Passified");
            DeactivateBees();
        }
    }

    /// <summary>
    /// Makes the bees aggressive and indicates to the player they are mad
    /// </summary>
    private void ActivateBees()
    {
        Debug.Log("active");
        ExclamationMark.GetComponent<TextMeshPro>().color = new Color(255, 0, 0);
        BeesAngry = true;
        for (int i = 0; i <= bees.Length - 1; i++)
        {
            bees[i].GetComponent<Bee>().SetisAngry(true);
        }
    }
    /// <summary>
    /// makes the bees passive
    /// </summary>
    private void DeactivateBees()
    {
        ExclamationMark.GetComponent<TextMeshPro>().color = new Color(255, 255, 255);
        ExclamationMark.SetActive(false);
        BeesAngry = false;
        for (int i = 0; i <= bees.Length - 1; i++)
        {
            bees[i].GetComponent<Bee>().SetisAngry(false);
            bees[i].GetComponent<Bee>().returnToStart = true;
        }
    }

    /// <summary>
    /// time until the bees get angry
    /// </summary>
    /// <returns></returns>
    IEnumerator GettingAngry()
    {
        ExclamationMark.SetActive(true);
        yield return new WaitForSeconds(BeeAngerDelay);
        ActivateBees();
    }
}
