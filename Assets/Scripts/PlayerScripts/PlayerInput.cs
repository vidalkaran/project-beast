using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Acting as a statecontroller AND input controller atm, will need to abstract this into two classes at some point.
public class PlayerInput : MonoBehaviour
{
    //Dependencies
    public PlayerMove playerMove;
    public PlayerCamera playerCamera;
    public PlayerCombat playerCombat;
    public Animator animator;

    //State enum
    public enum PlayerState {idle, walking, attacking}; 

    public PlayerState playerState;

    void Update()
    {
        updateState(); //Update player state for the animator and also other logics
        getInput(); //Input is handled by frame.
    }

    void getInput()
    {
        //Movement
        playerMove.UpdateSpeed(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Combat
        if(Input.GetKeyDown(KeyCode.Space))
            playerCombat.Attack();

        //Camera
        if (Input.GetKey(KeyCode.Q))
            playerCamera.Rotate(Vector3.up);
        if (Input.GetKey(KeyCode.E))
            playerCamera.Rotate(Vector3.down);
    }

    void updateState()
    {
        if(playerState == PlayerState.idle)
            animator.SetInteger("state", 0);
        else if (playerState == PlayerState.walking)
            animator.SetInteger("state", 1);
        if (playerState == PlayerState.attacking)
            animator.SetInteger("state", 2);
    }
}
