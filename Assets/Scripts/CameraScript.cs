using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Dependencies
    public Transform target; //The gameobject to follow
    public Transform cameraSmoother; //an empty game object used only to smoothen camera movement.
    public Camera cameraComponent; //The camera component. This should be on a separate gameobject so that it can be offset. 

    //Variables
    public Vector3 offset; 
    public float speed = 0.125f; //Rotation speed.

    //Internal vars
    private Vector3 desiredPosition; //For smooth camera movement
    private Quaternion desiredRotation; //For smooth camera movement

    private void Start()
    {
        cameraComponent.transform.position = transform.position + offset;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Update position of camera anchor
        desiredPosition = target.position;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, speed);

        //update rotation of camera anchor
        desiredRotation = cameraSmoother.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, speed);

        //Update position of camera attachment.
        cameraComponent.transform.LookAt(transform);
    }
}
