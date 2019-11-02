using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMove : MonoBehaviour
{
    //Dependencies
    PlayerController playerController;

    //Variables
    public float speed = 1f;
    private Vector3 moveVector;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    //Movement is handled in fixed update for collision
    private void FixedUpdate()
    {
        if (playerController.state != ActorState.ATTACKING_STATE) //halt movement when attacking.
            playerController.rigidBody.MovePosition(transform.position + (moveVector * speed * Time.deltaTime));
    }

    //Updates the moveVector that fixedUpdate checks.
    public void UpdateSpeed(float moveVert, float moveHorz)
    {
        //Use the camera anchor so player always move forward relative to camera.
        moveVector = (playerController.cameraAnchor.right * moveVert) +(playerController.cameraAnchor.forward * moveHorz);

        //Make sure the character always faces the last direction of movement and update state
        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            playerController.state = ActorState.WALKING_STATE;
        }
        else if(playerController.state != ActorState.ATTACKING_STATE)
            playerController.state = ActorState.IDLE_STATE;
    }

}
