using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuyController : Controller
{
    //To refactor out at some point
    BadGuyState newState = BadGuyState.IDLE;
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
    }

    void Update()
    {
        switch (newState)
        {
            case BadGuyState.IDLE:
            {
                if (target != null && Vector3.Distance(transform.position, target.position) > 1f)
                    newState = BadGuyState.CHASE;

                break;
            }
            case BadGuyState.CHASE:
            {
                if(target)
                {
                    transform.LookAt(target);
                    rigidBody.MovePosition(transform.localPosition + (transform.forward * speed * Time.deltaTime));
                }
                else
                   newState = BadGuyState.IDLE;


                //if (Vector3.Distance(transform.position, target.position) < .5f)
                //{
                //    newState = BadGuyState.IDLE;
                //}

                break;
            }
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "Player")
        {
            Instantiate(HitEmitterPrefab, c.transform.position, c.transform.rotation);
            c.gameObject.GetComponent<PlayerController>().Dead();
        }
    }

    public enum BadGuyState
    {
        IDLE,
        CHASE,
        ATTACK,
    }
}
