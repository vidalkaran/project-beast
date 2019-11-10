using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public override void EnterState()
    {
        base.EnterState();
        stateName = PlayerStateEnum.PLAYER_IDLE;
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            controller.SwitchState(PlayerStateEnum.PLAYER_WALKING);

        if (Input.GetKey(KeyCode.Z))
            controller.SwitchState(PlayerStateEnum.PLAYER_ATTACKING);
    }
}
