﻿using System.Collections;
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
    [HideInInspector] public Orientation orientation;
    [HideInInspector] public ActorState state;
    [HideInInspector] public Rigidbody rigidBody;

    //temp
    public Transform target;

    public virtual void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        spriteController = spriteContainer.GetComponent<SpriteController>();
        mainCamera = cameraAnchor.GetComponentInChildren<Camera>().transform;
        backLight = GetComponentInChildren<BackLightScript>();
    }
}
