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
    [HideInInspector] public Transform cameraAnchor;
    [HideInInspector] public Transform cameraSmoother;

    //Autofill Dependencies
    [HideInInspector] public SpriteController spriteController;
    [HideInInspector] public Transform mainCamera;
    [HideInInspector] public BackLightScript backLight;

    //Vars
    [HideInInspector] public Orientation orientation;
    [HideInInspector] public ActorState state;
    [HideInInspector] public Rigidbody rigidBody;

    //temp
    public Transform target;


    //Would be nice to delegate this to a game controller at some point...
    public virtual void Awake()
    {
        cameraAnchor = GameObject.FindGameObjectWithTag("CameraAnchor").transform;
        cameraSmoother = GameObject.FindGameObjectWithTag("CameraSmoother").transform;

        rigidBody = GetComponent<Rigidbody>();
        mainCamera = cameraAnchor.GetComponentInChildren<Camera>().transform;
        backLight = GetComponentInChildren<BackLightScript>();
    }
}
