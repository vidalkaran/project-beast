using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The State Machine is only responsible for keeping a reference to the current state and switching between states.
 */

public class StateMachine : MonoBehaviour
{
    [SerializeField]
    State startingState;
    State currentState;

    protected List<State> stateList = new List<State>();

    public void Start()
    {
        if(startingState != null)
            EnterState(startingState);
    }

    private void Update()
    {
        currentState.UpdateState();
    }

    public void EnterState(State nextState)
    {
        currentState = nextState;
        currentState.EnterState();
    }

    protected void SwitchState(State stateToEnter)
    {
        if (stateToEnter.stateName != currentState.stateName)
        {
            currentState.ExitState();
            stateToEnter.EnterState();
        }
    }
}
