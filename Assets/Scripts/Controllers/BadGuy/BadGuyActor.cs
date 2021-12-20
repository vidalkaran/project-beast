using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuyActor : Actor2D
{
    //To refactor out at some point
    public GameObject HitEmitterPrefab;  //Should outsource this to an Object Pool later for performance.
    public AttackData attackData;  //Need to eventually make this a unique attack for this enemy.

    //Internal AI Data
    public float speed = .75f;

    private GameObject aiWaypoint;
    private Vector3 randomVector;
    private Vector3 lookPos;
    public float distance;

    public override void Awake()
    {
        base.Awake();
        rigidBody = GetComponent<Rigidbody>();
        rigidBody = GetComponent<Rigidbody>();
        // target = GameObject.FindWithTag("Player").transform;
        health = 2;
        state = ActorState.SENTRY_STATE; // Default state could be an input var. 
        aiWaypoint = Instantiate(Resources.Load<GameObject>("Prefabs/AIWaypoint"), Vector3.zero, Quaternion.identity); // Need to investigate possibility of not making this a GameObject...
        aiWaypoint.transform.position = transform.position;
        target = aiWaypoint.transform;
    }

    void Update()
    {
        switch (state)
        {
            case ActorState.IDLE_STATE:
            {
                //nothing yet
                break;
            }
            case ActorState.WANDER_STATE:
            {
                //If we are at the targetPos, switch back to sentry
                distance = Vector3.Distance(transform.position, target.transform.position);
                if (distance < 1f)
                    state = ActorState.SENTRY_STATE;
                break;
            }
            case ActorState.SENTRY_STATE:
            {
                // Randomness Range can be a variable at some point.
                randomVector.x = target.transform.position.x + Random.Range(-.5f, .5f);
                randomVector.y = target.transform.position.y;
                randomVector.z = target.transform.position.z + Random.Range(-.5f, .5f);
                target.transform.position = randomVector;
                state = ActorState.WANDER_STATE;
                break;
            }
            case ActorState.STUNNED_STATE:
            {
                StartCoroutine(EnemyStunned());
                break;
            }
            case ActorState.CHASE_STATE:
            {
                if (target == null)
                    state = ActorState.WANDER_STATE;
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        if (state == ActorState.WANDER_STATE || state == ActorState.CHASE_STATE)
        {
            lookPos.x = target.transform.position.x;
            lookPos.y = transform.position.y;
            lookPos.z = target.transform.position.z;
            transform.LookAt(lookPos);
            rigidBody.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            state = ActorState.ATTACKING_STATE;

            collision.gameObject.GetComponent<PlayerCamera>().ShakeScreen(attackData.shakeScreenMod);
            collision.gameObject.GetComponent<PlayerActor>().ResolveAttack(this.gameObject, attackData);
        }
    }
    public IEnumerator EnemyStunned()
    {
        yield return new WaitForSeconds(.75f);
        state = ActorState.CHASE_STATE;
        StopCoroutine("Stunned");
    }
}
