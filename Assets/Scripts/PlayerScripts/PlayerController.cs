using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Acting as a statecontroller AND input controller atm, will need to abstract this into two classes at some point.

[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerCamera))]
[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    //Dependencies //Note, should eventually abstract this out to generic move classes at some point...
    [HideInInspector] public PlayerMove playerMove;
    [HideInInspector] public PlayerCamera playerCamera;
    [HideInInspector] public PlayerCombat playerCombat;
    [HideInInspector] public Rigidbody rigidBody;

    //Manually set these dependencies in the editor for now...
    public SpriteController spriteController;
    public Collider attackCollider;
    public MeshRenderer attackMesh;
    public Transform spriteContainer;

    //Camera dependencies
    public Transform cameraAnchor;
    public Transform camera;
    public Transform cameraSmoother;

    //State enum
    public ActorState playerState;
    public Orientation orientation;

    private void Awake()
    {
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
        if (playerState != ActorState.ATTACKING_STATE)
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
            Debug.Log("TEST");
            playerState = ActorState.ATTACKING_STATE;
        }

    }

    //Handle animation events from a nested spriteController
    public void TriggerAnimationEvent(AnimationEvent animationEvent)
    {
        if (animationEvent == AnimationEvent.ATTACK_EVENT)
            playerCombat.Attack();
        else if (animationEvent == AnimationEvent.END_ATTACK_EVENT)
            playerState = ActorState.IDLE_STATE;
    }
}
