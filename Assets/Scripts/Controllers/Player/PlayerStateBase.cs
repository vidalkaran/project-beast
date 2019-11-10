using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerStateBase : MonoBehaviour
{
    protected PlayerController controller;
    public PlayerStateEnum stateName = PlayerStateEnum.PLAYER_IDLE;

    public void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    public virtual void EnterState()
    {
        this.enabled = true;
    }

    public virtual void ExitState()
    {
        this.enabled = false;
    }
}
