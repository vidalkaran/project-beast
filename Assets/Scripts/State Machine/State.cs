using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : ScriptableObject
{
    public ExecutionState executionState { get; protected set; }
    public StateEnum stateName;

    public virtual bool EnterState()
    {
        executionState = ExecutionState.ACTIVE;
        return true;
    }

    public virtual void OnEnable()
    {
        executionState = ExecutionState.NONE;
    }

    public virtual bool ExitState()
    {
        executionState = ExecutionState.COMPLETED;
        return true;
    }

    public abstract void UpdateState();

    public enum ExecutionState
    {
        NONE,
        ACTIVE,
        COMPLETED,
        TERMINATED
    }
}
