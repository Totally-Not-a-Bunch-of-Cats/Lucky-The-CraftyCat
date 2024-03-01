using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Text", menuName = "ScriptableObjects/Text", order = 2)]
[System.Serializable]
public class Text : ScriptableObject
{
    //holds the display text
    public string DisplayText;
}
