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

    //Dependencies
    [HideInInspector] public SpriteController spriteController;
    [HideInInspector] public Transform mainCamera;
    [HideInInspector] public BackLightScript backLight;
    [HideInInspector] public Transform cameraAnchor;
    [HideInInspector] public Transform cameraSmoother;
    [HideInInspector] public Transform spriteContainer;

    //Vars
    [HideInInspector] public Orientation orientation;
    [HideInInspector] public ActorState state;
    [HideInInspector] public Rigidbody rigidBody;

    //temp
    public Transform target;

    public virtual void Awake()
    {
        cameraAnchor = GameObject.FindGameObjectWithTag("CameraAnchor").transform;
        cameraSmoother = GameObject.FindGameObjectWithTag("CameraSmoother").transform;
        rigidBody = GetComponent<Rigidbody>();
        spriteContainer = GetComponentInChildren<Animator>().transform;
        spriteController = spriteContainer.GetComponent<SpriteController>();
        mainCamera = cameraAnchor.GetComponentInChildren<Camera>().transform;
        backLight = GetComponentInChildren<BackLightScript>();
    }

    public void Dead()
    {
        Destroy(gameObject);
    }
}
