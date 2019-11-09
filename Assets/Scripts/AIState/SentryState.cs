using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryState : BaseState
{
    public SentryState(GameObject owner)
    {
        this.owner = owner;
    }

    public override AIState tickState()
    {
        throw new System.NotImplementedException();
    }
}


