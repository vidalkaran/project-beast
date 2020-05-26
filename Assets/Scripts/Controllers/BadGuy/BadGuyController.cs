using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuyController : Controller
{
    //To refactor out at some point
    public GameObject HitEmitterPrefab;  //Should outsource this to an Object Pool later for performance.

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

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            state = ActorState.ATTACKING_STATE;

            Instantiate(HitEmitterPrefab, c.transform.position, c.transform.rotation);
            c.transform.LookAt(transform);
            c.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 100); //Hard code 100 for now
            c.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 100f);
            c.gameObject.GetComponent<Controller>().backLight.IntensifyLight(.5f, 5f);
            c.gameObject.GetComponent<PlayerController>().TakeDamage(1);
        }
    }
}
