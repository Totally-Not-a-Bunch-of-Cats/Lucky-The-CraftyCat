using UnityEngine;

public class SlideState : EnemyState
{
    float movementSpeed = 0.005f;

    bool peakReached = false;

    Vector3 playerLocation;

    Vector3 currentPosition;

    Vector3 inititalLocation;

    Vector3 midpoint;

    Transform transform;

    protected override void OnEnter()
    {
        playerLocation = GameManager.instance.playerManager.PlayerLocation.position;
        playerLocation.y = 0;
        transform = sc.gameObject.transform;
        inititalLocation = transform.position;
        inititalLocation.y = 0;
        peakReached = false;
    }

    protected override void OnExit()
    {
        transform.position = new Vector3(transform.position.x,0,transform.position.z);
    }


    protected override void OnUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerLocation, movementSpeed);
        currentPosition = new Vector3(transform.position.x, 0, transform.position.z);



        if (!peakReached)
        {
            float percentTraveled = Vector3.Distance(currentPosition, inititalLocation) / Vector3.Distance(inititalLocation, playerLocation);
            transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(0, 1, percentTraveled * 2f), transform.position.z);
            if (percentTraveled >= .5f)
            {
                peakReached = true;
                midpoint = currentPosition;
            }
        }
        else
        {
            float percentTraveled = Vector3.Distance(currentPosition, midpoint) / Vector3.Distance(midpoint, playerLocation);
            transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(1, 0, percentTraveled), transform.position.z);
        }


        if (Vector3.Distance(transform.position, playerLocation) <= 0.1f)
        {
            sc.pauseState.cooldownTime = .1f;
            sc.ChangeState(sc.pauseState);
        }
    }
}
