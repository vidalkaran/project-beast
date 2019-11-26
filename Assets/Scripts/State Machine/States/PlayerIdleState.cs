using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName ="FSM/States/Idle", order =1)]
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
