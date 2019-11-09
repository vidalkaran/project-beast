using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Base Controller class
 * all unique actor controllers, like input and AI controllers, should inhereit this.
 */

public class Controller : MonoBehaviour
{
    //Manual dependencies
    public Transform cameraAnchor;
    public Transform cameraSmoother;
    public Transform spriteContainer;

    //Autofill Dependencies
    [HideInInspector] public SpriteController spriteController;
    [HideInInspector] public Transform mainCamera;
    [HideInInspector] public BackLightScript backLight;

    //Vars
    public Orientation orientation;
    public ActorState state;
    public Transform target;
    public Rigidbody myBody;

    public virtual void Awake()
    {
        spriteController = spriteContainer.GetComponent<SpriteController>();
        mainCamera = cameraAnchor.GetComponentInChildren<Camera>().transform;
        backLight = GetComponentInChildren<BackLightScript>();
    }

    public virtual void TriggerActorEvent(ActorEvent animationEvent)
    {
        //To be overriden. 
    }
}
