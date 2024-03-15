using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlideState : State {
    [SerializeField] protected float movementSpeed = 0.005f;

    Vector3 playerLocation;

    Transform transform;

    protected override void OnEnter(){
        playerLocation = GameManager.instance.playerManager.PlayerLocation.position;
        transform = sc.gameObject.transform;
        playerLocation.y = transform.position.y;
    }

    protected override void OnUpdate(){
        transform.position = Vector3.MoveTowards(transform.position,playerLocation,movementSpeed);

        if(Vector3.Distance(transform.position,playerLocation) <= 0.1f){
            sc.ChangeState(sc.pauseState);
        }
    }
}
