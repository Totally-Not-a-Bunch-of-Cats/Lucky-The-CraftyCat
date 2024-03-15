using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class PauseState : State {
    
    public float cooldownTime =  1f;
    private float startTime;

    protected override void OnEnter(){
        startTime = Time.time;
    }

    protected override void OnUpdate(){
       if(Time.time > startTime + cooldownTime){
        sc.ChangeState(sc.slimeWanderState);
       }
    }
}