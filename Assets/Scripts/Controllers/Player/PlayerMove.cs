using UnityEngine;
using System.Collections;

public class PlayerMove : PlayerComponent
{
    //Move Variables
    public float walkSpeed = 2f;
    public Vector3 moveVector;

    //Dash Variables
    public float dashLength = .25f;
    public float dashCooldown = .5f;
    public float dashSpeed = 5f;

    //Dependencies
    ParticleSystem dashEffect;

    private void Start()
    {
        dashEffect = Instantiate(Resources.Load<GameObject>("Prefabs/SFX/DashEffect"), new Vector3(0, 0, .5f), Quaternion.identity).GetComponent<ParticleSystem>();
        dashEffect.Stop();
    }

    //Movement is handled in fixed update for collision
    private void FixedUpdate() 
    {
        if (actor.state == ActorState.DODGE_STATE)
            actor.rigidBody.MovePosition(transform.position + (moveVector * dashSpeed * Time.deltaTime));
        else
            actor.rigidBody.MovePosition(transform.position + (moveVector * walkSpeed * Time.deltaTime));
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

    // Design notes on this...
    // Right now the speed is static and the dodge is fast and snappy. 
    // Should experiment with possibly using a curve to handle the speed and have it start slow, speed up, and slow down again like a dark souls dodge roll.
    // Also don't like how we handle iFrames in here. We should probably move it back into the actor somehow.
    IEnumerator DashCoroutine()
    {
        actor.state = ActorState.DODGE_STATE;
        dashEffect.transform.position = transform.position;
        dashEffect.transform.rotation = transform.rotation;
        dashEffect.Play();
        moveVector = transform.forward;
        Physics.IgnoreLayerCollision(3, 8, true); // For iFrames
        yield return new WaitForSeconds(dashLength);
        moveVector = Vector3.zero;
        Physics.IgnoreLayerCollision(3, 8, false); // For iFrames
        yield return new WaitForSeconds(dashCooldown);
        dashEffect.Stop();
        actor.state = ActorState.IDLE_STATE;
    }
}
