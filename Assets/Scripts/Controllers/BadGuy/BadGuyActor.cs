using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuyActor : Actor2D
{
    //To refactor out at some point
    public GameObject HitEmitterPrefab;  //Should outsource this to an Object Pool later for performance.
    public AttackData attackData;  //Need to eventually make this a unique attack for this enemy.

    //Components
    BadGuyCombat badGuyCombat; 

    public float speed = 3;

    public override void Awake()
    {
        base.Awake();
        badGuyCombat = GetComponent<BadGuyCombat>();
        rigidBody = GetComponent<Rigidbody>();
        target = GameObject.FindWithTag("Player").transform;
        health = 2;
        state = ActorState.IDLE_STATE;
    }

    void Update()
    {
        switch (state)
        {
            case ActorState.IDLE_STATE:
            {
                if (target != null && Vector3.Distance(transform.position, target.position) > 0f)
                        state = ActorState.WALKING_STATE;

                break;
            }
            case ActorState.WALKING_STATE:
            {
                if(target)
                {
                    transform.LookAt(target);
                    rigidBody.MovePosition(transform.localPosition + (transform.forward * speed * Time.deltaTime));
                }
                else
                        state = ActorState.IDLE_STATE;
                break;
            }
            case ActorState.ATTACKING_STATE:
            {
                StartCoroutine(Stunned());
                break;
            }
            case ActorState.STUNNED_STATE:
            {
                StartCoroutine(Stunned());
                break;
            }
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
}
