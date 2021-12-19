using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Base Controller class
 * all unique actor controllers, like input and AI controllers, should inhereit this.
 */

public class Actor2D : MonoBehaviour
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

    public float health = 0f;

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

    public void TakeDamage(float damage)
    {
        StopCoroutine("Stunned");

        health -= 1;
        state = ActorState.STUNNED_STATE;

        if (health <= 0)
        {
            Debug.Log(gameObject.name + " has died");
            Destroy(gameObject);
        }
    }

    public IEnumerator Stunned()
    {
        yield return new WaitForSeconds(.75f);
        state = ActorState.IDLE_STATE;
        StopCoroutine("Stunned");
    }
}
