using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InformationDisplayManager : MonoBehaviour
{
    [SerializeField] List<TextWarning> Warnings;
    [SerializeField] List<TextTip> Tips;
    public GameObject DisplayText;
    [SerializeField] int DecayTime = 2;

    /// <summary>
    /// Goes and gets the display object    //Expensive should be done on load
    /// </summary>
    public void FetchTxtObject()
    {
        DisplayText = GameObject.FindGameObjectWithTag("DisplayTxt");
    }

    /// <summary>
    /// Dispalys the tips
    /// </summary>
    /// <param name="num"></param>
    public void DisplayTip(int num)
    {
        DisplayText.transform.GetChild(0).gameObject.SetActive(true);
        DisplayText.GetComponentInChildren<TMP_Text>().text = Tips[num].DisplayText;
        StartCoroutine(Decay());
    }

    /// <summary>
    /// Displays the warnings
    /// </summary>
    /// <param name="num"></param>
    public void DisplayWarning(int num)
    {
        DisplayText.transform.GetChild(0).gameObject.SetActive(true);
        DisplayText.GetComponentInChildren<TMP_Text>().text = Warnings[num].DisplayText;
        StartCoroutine(Decay());
    }

    /// <summary>
    /// Time till the help text disapears or what ever we want to happen
    /// </summary>
    /// <returns></returns>
    IEnumerator Decay()
    {
        yield return new WaitForSeconds(DecayTime);
        DisplayText.transform.GetChild(0).gameObject.SetActive(false);
    }
}
