using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.XR;

public class SlimeWanderState : State {
    
    private float detectionRange = 5f;

    private float wanderDistance = 3f;

    protected float movementSpeed = 0.1f;

    private Transform playerLocation;

    private Vector3 destination;

    private Transform myLocation;

    protected override void OnEnter(){
        destination = Random.insideUnitCircle.normalized * wanderDistance;
        playerLocation = GameManager.instance.playerManager.PlayerLocation;
        myLocation = sc.gameObject.transform;
    }

    protected override void OnUpdate(){
       if(Vector3.Distance(playerLocation.position,myLocation.position) <= detectionRange){
        sc.ChangeState(sc.slideState);
       }else if(myLocation.position == destination){
        sc.ChangeState(this);
       }else{
        myLocation.position = Vector3.MoveTowards(myLocation.position,playerLocation.position,movementSpeed);
       }
    }
}