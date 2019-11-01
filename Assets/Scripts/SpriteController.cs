using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This sprite controller goes on the nested sprite of any object in game. 
 *Assumptions:
 *  - The parent object has a controller that has a reference to the camera. 
 *  - This nested controller manages the animator
 *  - The animator is on the nested sprite object. 
 */

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class SpriteController : MonoBehaviour
{
    //Dependencies
    PlayerController playerController;
    Animator animator;

    //Variables
    public Orientation spriteOrientation;
       
    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    void LateUpdate()
    {
        transform.LookAt(playerController.camera);
        SetOrientation(Mathf.Round((playerController.cameraAnchor.eulerAngles.y - playerController.transform.eulerAngles.y) / 45) * 45);
        UpdateAnimator();
    }

    //Gets the orientation of the player relevant to the camera via Euler angles. May not be the most performant solution.
    void SetOrientation(float facing)
    {
        if (facing == 0 || facing == -360)
            spriteOrientation = Orientation.UP_ORIENTATION;
        else if (facing == -45 || facing == 315)
            spriteOrientation = Orientation.UPRIGHT_ORIENTATION;
        else if (facing == -90 || facing == 270)
            spriteOrientation = Orientation.RIGHT_ORIENTATION;
        else if (facing == -135 || facing == 225)
            spriteOrientation = Orientation.DOWNRIGHT_ORIENTATION;
        else if (facing == -180 || facing == 180)
            spriteOrientation = Orientation.DOWN_ORIENTATION;
        else if (facing == 135 || facing == -225)
            spriteOrientation = Orientation.DOWNLEFT_ORIENTATION;
        else if (facing == 90 || facing == -270)
            spriteOrientation = Orientation.LEFT_ORIENTATION;
        else if (facing == 45 || facing == -315)
            spriteOrientation = Orientation.UPLEFT_ORIENTATION;

        playerController.orientation = spriteOrientation;
    }

    void UpdateAnimator()
    {
        //Updating state
        if (playerController.playerState == ActorState.IDLE_STATE)
            animator.SetInteger("state", 0);
        else if (playerController.playerState == ActorState.WALKING_STATE)
            animator.SetInteger("state", 1);
        if (playerController.playerState == ActorState.ATTACKING_STATE)
            animator.SetInteger("state", 2);

        //Updating orientation
        if (spriteOrientation == Orientation.UP_ORIENTATION)
            animator.SetInteger("orientation", 1);
        else if (spriteOrientation == Orientation.UPRIGHT_ORIENTATION)
            animator.SetInteger("orientation", 2);
        else if (spriteOrientation == Orientation.RIGHT_ORIENTATION)
            animator.SetInteger("orientation", 3);
        else if (spriteOrientation == Orientation.DOWNRIGHT_ORIENTATION)
            animator.SetInteger("orientation", 4);
        else if (spriteOrientation == Orientation.DOWN_ORIENTATION)
            animator.SetInteger("orientation", 5);
        else if (spriteOrientation == Orientation.DOWNLEFT_ORIENTATION)
            animator.SetInteger("orientation", 6);
        else if (spriteOrientation == Orientation.LEFT_ORIENTATION)
            animator.SetInteger("orientation", 7);
        else if (spriteOrientation == Orientation.UPLEFT_ORIENTATION)
            animator.SetInteger("orientation", 8);
    }
    
    void TriggerAnimationEvent(string animationEvent)
    {   
        playerController.TriggerAnimationEvent((AnimationEvent)System.Enum.Parse(typeof(AnimationEvent), animationEvent));
    }

    /*For flipping the sprite based on facing. Disabled for now.
    void updateFacing()
    {
        if (orientation == Orientation.upright || orientation == Orientation.downright || orientation == Orientation.right)
            renderer.flipX = false;
        else if (orientation == Orientation.upleft || orientation == Orientation.downleft || orientation == Orientation.left)
            renderer.flipX = true;
    }*/
}
