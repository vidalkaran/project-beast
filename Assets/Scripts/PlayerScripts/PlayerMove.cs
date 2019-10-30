using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //Dependencies
    public Rigidbody rigidBody;
    public Transform cameraAnchor;
    public PlayerInput playerInput;

    //Variables
    public float speed = 1f;
    private Vector3 moveVector;

    //Movement is handled in fixed update for collision
    private void FixedUpdate()
    {
        rigidBody.MovePosition(transform.position + (moveVector * speed * Time.deltaTime));
    }

    //Updates the moveVector that fixedUpdate checks.
    public void UpdateSpeed(float moveVert, float moveHorz)
    {
        //Use the camera anchor so player always move forward relative to camera.
        moveVector = (cameraAnchor.right * moveVert) +(cameraAnchor.forward * moveHorz);

        //Make sure the character always faces the last direction of movement and update state
        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            playerInput.playerState = PlayerInput.PlayerState.walking;
        }
        else if(playerInput.playerState != PlayerInput.PlayerState.attacking)
            playerInput.playerState = PlayerInput.PlayerState.idle;
    }

}
