using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Your basic Game Manager
/// </summary>
public class GameManager : MonoBehaviour
{
    // Singleton Setup
    public static GameManager instance;

    [SerializeField] private PlayerManager playerManager;

    /// <summary>
    /// On Awake create an instance of gamemanger or destroy if there is already a gamemanger in scene
    /// </summary>
    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }
}
