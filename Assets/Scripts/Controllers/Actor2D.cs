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
    [HideInInspector] public Rigidbody rigidBody;

    //Variables
    public ActorState state;
    public float health = 0f;
    public Transform target; // This should be moved up to the enemy class at some point... need to consider making resolve attack more virtual somehow.

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

    //This should be virtual and implemented by the higher order class, ie the PlayerActor or EnemyActor...
    public void ResolveAttack(GameObject enemyActor, AttackData enemyAttack)
    {
        target = enemyActor.transform;

        if (state == ActorState.PARRY_STATE)
        {
            //This is... horrible... but temporary
            enemyActor.GetComponent<BadGuyActor>().ResolveAttack(gameObject, Resources.Load<AttackData>("TempParryData"));
        }
        else
        {
            StopCoroutine("Stunned");

            Instantiate(enemyAttack.HitEmitterPrefab, transform.position, transform.rotation);

            //Physics
            rigidBody.AddForce(enemyActor.transform.forward * enemyAttack.attackForce);
            rigidBody.AddForce(enemyActor.transform.up * 100); //Hard code 100 for now
            transform.LookAt(enemyActor.transform);

            //Enemy
            backLight.IntensifyLight(enemyAttack.intensifyTime, enemyAttack.intensifyMod);
            health -= enemyAttack.attackDamage;
            state = ActorState.STUNNED_STATE;

            if (health <= 0)
            {
                Debug.Log(gameObject.name + " has died"); //Send this to a notification center at some point. 
                Destroy(gameObject); //Player should override this to perform some gameOver logic
            }
        }
    }


    public IEnumerator Stunned()
    {
        yield return new WaitForSeconds(.75f);
        state = ActorState.IDLE_STATE;
        StopCoroutine("Stunned");
    }
}
