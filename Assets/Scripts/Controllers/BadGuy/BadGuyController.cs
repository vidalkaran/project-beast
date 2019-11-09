using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuyController : Controller
{
    //To refactor out at some point
    BadGuyState newState = BadGuyState.IDLE;

    //Components
    BadGuyCombat badGuyCombat;

    public override void Awake()
    {
        base.Awake();
        badGuyCombat = GetComponent<BadGuyCombat>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        switch (newState)
        {
            case BadGuyState.IDLE:
            {
                if (target != null)
                    newState = BadGuyState.CHASE;

                break;
            }
            case BadGuyState.CHASE:
            {
                transform.LookAt(target);
                rigidBody.MovePosition(transform.localPosition+ (transform.forward * 1.5f * Time.deltaTime));

                if (Vector3.Distance(transform.position, target.position) < .5f)
                {
                    target = null;
                    newState = BadGuyState.IDLE;
                }

                break;
            }
            case BadGuyState.ATTACK:
            {
                break;
            }
        }
    }

    enum BadGuyState
    {
        IDLE,
        CHASE,
        ATTACK
    }
}
