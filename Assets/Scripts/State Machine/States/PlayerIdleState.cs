using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State
{
    public override bool EnterState()
    {
        base.EnterState();
        return true;
    }

    public override void UpdateState()
    {
        Debug.Log("idle");
        throw new System.NotImplementedException();
    }
}
