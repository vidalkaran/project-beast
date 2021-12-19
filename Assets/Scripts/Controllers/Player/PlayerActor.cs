using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Acting as a statecontroller AND input controller atm, will need to abstract this into two classes at some point... or maybe not?
public class PlayerActor : Actor2D
{
    [HideInInspector] public PlayerMove playerMove; 
    [HideInInspector] public PlayerCamera playerCamera;
    [HideInInspector] public PlayerCombat playerCombat;

    public override void Awake()
    {
        base.Awake();

        //Reference required dependencies
        playerMove = GetComponent<PlayerMove>();
        playerCamera = GetComponent<PlayerCamera>();
        playerCombat = GetComponent<PlayerCombat>();
        health = 99;
    }

    void Update()
    {
        switch(state)
        {
            case ActorState.IDLE_STATE:
            {
                HandleInput();
                break;
            }
            case ActorState.WALKING_STATE:
            {
                HandleInput();
                break;
            }
            case ActorState.STUNNED_STATE:
            {
                StartCoroutine("Stunned"); //Returns to idle state when done.
                break;
            }
            case ActorState.ATTACKING_STATE:
            {
                //Nothing Yet. Can be used for combos later. 
                break;
            }
            case ActorState.DODGE_STATE:
            {
                //Nothing Yet
                break;
            }          
            //Todo
            //Add a dodge and parry state.
        }
    }



    void HandleInput() // Eventually, when the above states are abstracted out to classes, we can have each one have their own HandleInput() function.
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

            //Attack
            if (Input.GetKeyDown(KeyCode.LeftControl))
                playerCombat.Attack();

            //Dodge
            if (Input.GetKeyDown(KeyCode.LeftShift))
                playerMove.Dash();
        }
    } 
}
