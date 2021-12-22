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

    // private GameObject aiWaypoint;
    public Vector3 targetDestination;
    private Vector3 randomVector;
    private Vector3 lookPos;
    public float distance;

    public override void Awake()
    {
        base.Awake();
        rigidBody = GetComponent<Rigidbody>();
        health = 2;
        state = ActorState.SENTRY_STATE; // Default state could be an input var. 
        targetDestination = transform.position;
    }

    void Update()
    {
        switch (state)
        {
            case ActorState.IDLE_STATE:
            {
                break;
            }
            case ActorState.WANDER_STATE:
            {
                distance = Vector3.Distance(transform.position, targetDestination);
                if (distance < .3f)
                    state = ActorState.SENTRY_STATE;
                break;
            }
            case ActorState.SENTRY_STATE:
            {
                StartCoroutine(SentryDelay());
                state = ActorState.IDLE_STATE;
                break;
            }
            case ActorState.STUNNED_STATE:
            {
                StopCoroutine(EnemyStunned());
                StartCoroutine(EnemyStunned());
                break;
            }
            case ActorState.CHASE_STATE:
            {
                if (target == null)
                    state = ActorState.WANDER_STATE;
                else
                    targetDestination = target.position;
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        if (state == ActorState.WANDER_STATE || state == ActorState.CHASE_STATE)
        {
            lookPos.x = targetDestination.x;
            lookPos.y = targetDestination.y;
            lookPos.z = targetDestination.z;
            transform.LookAt(lookPos);
            rigidBody.MovePosition(transform.position + (speed * Time.deltaTime * transform.forward));
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
    }

    public IEnumerator SentryDelay()
    {
        yield return new WaitForSeconds(Random.Range(.05f,3));
        randomVector.x = targetDestination.x + Random.Range(-1f, 1f);
        randomVector.y = transform.position.y;
        randomVector.z = targetDestination.z + Random.Range(-1f, 1f);
        targetDestination = randomVector;
        state = ActorState.WANDER_STATE;
    }

}
