using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState
{
    protected StateController sc;

    public void OnStateEnter(StateController stateController)
    {
        // Code placed here will always run
        Debug.Log("Entering state " + this);
        sc = stateController;
        OnEnter();
    }

    protected virtual void OnEnter()
    {
        // Code placed here can be overridden
    }

    public void OnStateUpdate()
    {
        // Code placed here will always run
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
        // Code placed here can be overridden
    }

    public void OnStateHurt()
    {
        // Code placed here will always run
        OnHurt();
    }

    protected virtual void OnHurt()
    {
        // Code placed here can be overridden
    }

    public void OnStateExit()
    {
        // Code placed here will always run
        Debug.Log("Exiting state " + this);
        OnExit();
    }

    protected virtual void OnExit()
    {
        // Code placed here can be overridden
    }
}