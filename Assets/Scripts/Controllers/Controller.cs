using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Base Controller class
 * all unique actor controllers, like input and AI controllers, should inhereit this.
 */

public class Controller : MonoBehaviour
{
    //Camera dependencies
    public Transform cameraAnchor;
    public Transform camera;
    public Transform cameraSmoother;

    //Vars
    public Orientation orientation;
    public ActorState state;

    public virtual void TriggerAnimationEvent(AnimationEvent animationEvent)
    {
        //To be overriden. 
    }
}
