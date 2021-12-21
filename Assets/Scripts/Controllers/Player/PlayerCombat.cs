using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : PlayerComponent
{
    public AttackData basicAttack; // Note, eventually have this be a list and tie it to combo system or to player input somehow
    Collider hitboxCollider; //Note, need to store this somewhere more useful. Maybe assign it back to the scriptable object?

    private void Start()
    {
        hitboxCollider = basicAttack.SetupCollider(transform);
    }

    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    public void Parry()
    {
        StartCoroutine(ParryCoroutine());
    }


    void HandleHit()
    {
        //Get everything our hitbox collided with.
        var collided = Physics.OverlapBox(hitboxCollider.bounds.center,
                                hitboxCollider.bounds.extents,
                                hitboxCollider.transform.rotation,
                                LayerMask.GetMask("Hitbox"));

        //For each collision, handle it
        foreach (Collider collision in collided)
        {
            if (collision.transform.tag == "Enemy")
            {
                actor.playerCamera.ShakeScreen(basicAttack.shakeScreenMod);
                collision.GetComponent<BadGuyActor>().ResolveAttack(this.gameObject, basicAttack);
            }

        }
    }

    //The attack coroutine.
    IEnumerator AttackCoroutine()
    {
        actor.state = ActorState.ATTACKING_STATE;
        yield return new WaitForSeconds(basicAttack.attackWindup);
        HandleHit();
        yield return new WaitForSeconds(basicAttack.attackCooldown);
        actor.state = ActorState.IDLE_STATE;
    }

    //The parry coroutine.
    IEnumerator ParryCoroutine()
    {
        actor.state = ActorState.PARRY_STATE;
        yield return new WaitForSeconds(2f);
        actor.state = ActorState.IDLE_STATE;
    }
}
