using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuyController : Controller
{
    BadGuyCombat badGuyCombat;

    public override void Awake()
    {
        base.Awake();
        badGuyCombat = GetComponent<BadGuyCombat>();
    }

    public override void TriggerActorEvent(ActorEvent actorEvent)
    {
        if (actorEvent == ActorEvent.HIT_EVENT)
        {
            badGuyCombat.GetHit();
        }
    }
}
