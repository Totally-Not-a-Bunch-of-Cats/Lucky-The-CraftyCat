using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Camera MainCamera;
    [SerializeField] private float DistancePlayer;
    [SerializeField] private Vector3 Offset = new Vector3(0, 15, -24);
    [SerializeField] private Vector3 TargetLocation;
    [SerializeField] private float SmoothSpeed = .5f;
    private Vector3 velocity = Vector3.zero;


    private void Update() 
    {
        TargetLocation = GameManager.instance.playerManager.PlayerLocation.position + Offset;
        transform.position = Vector3.SmoothDamp(transform.position, TargetLocation, ref velocity, SmoothSpeed);
    }
}
