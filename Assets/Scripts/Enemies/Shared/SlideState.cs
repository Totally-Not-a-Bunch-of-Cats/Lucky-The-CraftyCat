using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlideState : EnemyState
{
    float movementSpeed = 0.005f;

    Vector3 playerLocation;

    Transform transform;

    protected override void OnEnter()
    {
        playerLocation = GameManager.instance.playerManager.PlayerLocation.position;
        playerLocation.y = 0;
        transform = sc.gameObject.transform;
    }

    protected override void OnUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerLocation, movementSpeed);

        if (Vector3.Distance(transform.position, playerLocation) <= 0.1f)
        {
            sc.ChangeState(sc.pauseState);
        }
    }
}
