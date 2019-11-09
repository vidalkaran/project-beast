using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    protected List<State> stateList = new List<State>();
    protected State currentState;

    private void Update()
    {
        currentState.UpdateState();
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
