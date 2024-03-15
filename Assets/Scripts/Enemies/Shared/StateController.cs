using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{

    State currentState;
    
    public PauseState pauseState = new();
    public SlideState slideState = new();
    public SlimeWanderState slimeWanderState = new();



    // Start is called before the first frame update
    void Start()
    {
        ChangeState(slimeWanderState);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentState != null){
            currentState.OnStateUpdate();
        }
    }

    public void ChangeState(State newState)
    {
        currentState?.OnStateExit();
        currentState = newState;
        currentState.OnStateEnter(this);
    }
}
