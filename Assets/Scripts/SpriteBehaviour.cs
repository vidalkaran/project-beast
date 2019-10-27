using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBehaviour : MonoBehaviour
{
    //Dependencies
    public Transform camera;
    public Transform cameraAnchor;
    public Transform player;
    public Animator animator;
    public SpriteRenderer renderer;

    //Orientation enum
    public enum Orientation { up, upright, right, downright, down, downleft, left, upleft, none };

    //variables
    public Orientation orientation;

    void LateUpdate()
    {
        transform.LookAt(camera);
        getOrientation(Mathf.Round((cameraAnchor.eulerAngles.y - player.eulerAngles.y) / 45) * 45);
        updateAnimator();
        //updateFacing(); temporary disabling because of the ys sprite
    }

    //Gets the orientation of the player relevant to the camera via Euler angles. May not be the most performant solution.
    void getOrientation(float facing)
    {
        if (facing == 0 || facing == -360)
            orientation = Orientation.up;
        else if (facing == -45 || facing == 315)
            orientation = Orientation.upright;
        else if (facing == -90 || facing == 270)
            orientation = Orientation.right;
        else if (facing == -135 || facing == 225)
            orientation = Orientation.downright;
        else if (facing == -180 || facing == 180)
            orientation = Orientation.down;
        else if (facing == 135 || facing == -225)
            orientation = Orientation.downleft;
        else if (facing == 90 || facing == -270)
            orientation = Orientation.left;
        else if (facing == 45 || facing == -315)
            orientation = Orientation.upleft;
    }

    //For updating the animator variables
    void updateAnimator()
    {
        if (orientation == Orientation.up)
            animator.SetInteger("orientation", 1);
        else if (orientation == Orientation.upright)
            animator.SetInteger("orientation", 2);
        else if (orientation == Orientation.right)
            animator.SetInteger("orientation", 3);
        else if (orientation == Orientation.downright)
            animator.SetInteger("orientation", 4);
        else if (orientation == Orientation.down)
            animator.SetInteger("orientation", 5);
        else if (orientation == Orientation.downleft)
            animator.SetInteger("orientation", 6);
        else if (orientation == Orientation.left)
            animator.SetInteger("orientation", 7);
        else if (orientation == Orientation.upleft)
            animator.SetInteger("orientation", 8);
    }

    //For flipping the sprite based on facing. 
    void updateFacing()
    {
        if (orientation == Orientation.upright || orientation == Orientation.downright || orientation == Orientation.right)
            renderer.flipX = false;
        else if (orientation == Orientation.upleft || orientation == Orientation.downleft || orientation == Orientation.left)
            renderer.flipX = true;
    }
}
