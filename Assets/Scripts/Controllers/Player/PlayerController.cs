using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Note, this is a playerController designed to handle player control states, such as walking. 
// Right now, it pulls double duty managing the input for camera and for sprites... we can abstract this all out to a manager at some point.
public class PlayerController : Controller
{
    //Manually set these dependencies in the editor for now...
    public Collider attackCollider;
    public MeshRenderer attackMesh;

    public PlayerStateBase currentState;

    //Dependencies //Note, should eventually abstract this out to generic move classes at some point...
    [HideInInspector] public PlayerIdleState playerIdle;
    [HideInInspector] public PlayerMoveState playerMove;
    [HideInInspector] public PlayerCameraController playerCamera;
    [HideInInspector] public PlayerAttackComponent playerCombat;

    Dictionary<PlayerStateEnum, PlayerStateBase> stateList = new Dictionary<PlayerStateEnum, PlayerStateBase>();

    public override void Awake()
    {
        base.Awake();

        //Reference required dependencies
        playerMove = GetComponent<PlayerMoveState>();
        playerCamera = GetComponent<PlayerCameraController>();
        //playerCombat = GetComponent<PlayerAttackComponent>();

        playerIdle = GetComponent<PlayerIdleState>();
        stateList.Add(PlayerStateEnum.PLAYER_IDLE, playerIdle);
        currentState = playerIdle;

        playerMove = GetComponent<PlayerMoveState>();
        stateList.Add(PlayerStateEnum.PLAYER_WALKING, playerMove);
        playerMove.ExitState();

        playerCombat = GetComponent<PlayerAttackComponent>();
        stateList.Add(PlayerStateEnum.PLAYER_ATTACKING, playerCombat);
        playerCombat.ExitState();
    }

    public void SwitchState(PlayerStateEnum stateToEnter)
    {
        if (stateToEnter != currentState.stateName)
        {
            currentState.ExitState();
            stateList[stateToEnter].EnterState();
        }
    }
}

