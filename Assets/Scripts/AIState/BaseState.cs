using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public GameObject owner;
    AIState currentState;

    public abstract AIState tickState();
}


