using UnityEngine;

public class SlimeWanderState : EnemyState
{

    private int detectionRange = 5;

    private int wanderDistance = 3;

    protected float movementSpeed = 0.003f;

    private Transform playerLocation;

    private Vector3 destination;

    private Transform myLocation;


    protected override void OnEnter()
    {
        destination = Random.insideUnitSphere.normalized * wanderDistance;
        destination.y = 0;
        playerLocation = GameManager.instance.playerManager.PlayerLocation;
        myLocation = sc.gameObject.transform;
        destination += myLocation.position;
    }

    protected override void OnUpdate()
    {
        if (Vector3.Distance(playerLocation.position, myLocation.position) <= detectionRange)
        {
            sc.ChangeState(sc.warningState);
        }
        else if (myLocation.position == destination)
        {
            sc.pauseState.cooldownTime = .3f;
            sc.ChangeState(sc.pauseState);
        }
        else
        {
            myLocation.position = Vector3.MoveTowards(myLocation.position, destination, movementSpeed);
        }
    }
}