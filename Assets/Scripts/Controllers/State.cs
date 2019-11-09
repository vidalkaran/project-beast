﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    public Controller controller;
    public StateEnum stateName;

    public virtual void EnterState()
    {

    }

    public virtual void ExitState()
    {

    }

    public virtual void UpdateState()
    {

    }
}
