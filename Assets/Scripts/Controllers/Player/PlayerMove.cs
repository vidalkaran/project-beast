using UnityEngine;
using System.Collections;

public class PlayerMove : PlayerComponent
{
    //Variables
    public float speed = 1f;
    private Vector3 moveVector;

    //Movement is handled in fixed update for collision
    private void FixedUpdate() 
    {
        // Eventually, this state dependency should be handled in a player move state object
        if (actor.state != ActorState.ATTACKING_STATE
            && actor.state != ActorState.STUNNED_STATE) //Only allow movement when standing idle or mid walking.
        {
            actor.rigidBody.MovePosition(transform.position + (moveVector * speed * Time.deltaTime));
        }
    }

    //Updates the moveVector that fixedUpdate checks.
    public void UpdateSpeed(float moveVert, float moveHorz)
    {
        //Use the camera anchor so player always move forward relative to camera.
        moveVector = (actor.cameraAnchor.right * moveVert) + (actor.cameraAnchor.forward * moveHorz);

        //Make sure the character always faces the last direction of movement and update state
        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            actor.state = ActorState.WALKING_STATE;
        }
        else
            actor.state = ActorState.IDLE_STATE;
    }

    //Dash!
    public void Dash()
    {
        StartCoroutine(DashCoroutine());
    }

    IEnumerator DashCoroutine()
    {
        Debug.Log("Test");
        actor.state = ActorState.DODGE_STATE;
        yield return new WaitForSeconds(3);
        actor.state = ActorState.IDLE_STATE;
    }
}
