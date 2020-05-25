using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerComponent
{
    //Variables
    public float speed = 1f;
    private Vector3 moveVector;

    //Movement is handled in fixed update for collision
    private void FixedUpdate()
    {
        if (controller.state != ActorState.ATTACKING_STATE) //halt movement when attacking.
            controller.rigidBody.MovePosition(transform.position + (moveVector * speed * Time.deltaTime));
    }

    //Updates the moveVector that fixedUpdate checks.
    public void UpdateSpeed(float moveVert, float moveHorz)
    {
        //Use the camera anchor so player always move forward relative to camera.
        moveVector = (controller.cameraAnchor.right * moveVert) +(controller.cameraAnchor.forward * moveHorz);

        //Make sure the character always faces the last direction of movement and update state
        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            controller.state = ActorState.WALKING_STATE;
        }
        else if(controller.state != ActorState.ATTACKING_STATE)
            controller.state = ActorState.IDLE_STATE;
    }
}
