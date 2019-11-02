using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Acting as a statecontroller AND input controller atm, will need to abstract this into two classes at some point.

[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerCamera))]
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : Controller
{
    //Dependencies //Note, should eventually abstract this out to generic move classes at some point...
    [HideInInspector] public PlayerMove playerMove;
    [HideInInspector] public PlayerCamera playerCamera;
    [HideInInspector] public PlayerCombat playerCombat;
    [HideInInspector] public Rigidbody rigidBody;

    //Manually set these dependencies in the editor for now...
    public Collider attackCollider;
    public MeshRenderer attackMesh;

    public override void Awake()
    {
        base.Awake();

        //Reference required dependencies
        playerMove = GetComponent<PlayerMove>();
        playerCamera = GetComponent<PlayerCamera>();
        playerCombat = GetComponent<PlayerCombat>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput(); //Check for input
    }

    void GetInput()
    {
        if (state != ActorState.ATTACKING_STATE)
        {
            //Movement
            playerMove.UpdateSpeed(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            //Camera
            if (Input.GetKey(KeyCode.Q))
                playerCamera.Rotate(Vector3.up);
            if (Input.GetKey(KeyCode.E))
                playerCamera.Rotate(Vector3.down);
        }

        //Combat
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            state = ActorState.ATTACKING_STATE;
        }

    }

    //Handle animation events from a nested spriteController
    public override void TriggerActorEvent(ActorEvent actorEvent)
    {
        if (actorEvent == ActorEvent.ATTACK_EVENT)
            playerCombat.Attack();
        else if (actorEvent == ActorEvent.END_ATTACK_EVENT)
            state = ActorState.IDLE_STATE;
        else if (actorEvent == ActorEvent.HIT_EVENT)
            playerCamera.ShakeScreen();
    }
}
