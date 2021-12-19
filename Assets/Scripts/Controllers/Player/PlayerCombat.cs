﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : PlayerComponent
{
    public AttackData basicAttack; // Note, eventually have this be a list and tie it to combo system or to player input somehow
    Collider collider; //Note, need to store this somewhere more useful. Maybe assign it back to the scriptable object?

    private void Start()
    {
        //Set up mesh based on scriptable object
        GameObject hitboxGameObject = Instantiate(basicAttack.hitboxPrefab, new Vector3(0, 0, .5f), Quaternion.identity);
        hitboxGameObject.transform.SetParent(this.gameObject.transform, false);
        hitboxGameObject.GetComponent<MeshRenderer>().enabled = false; //Disabling debug renderer
        collider = hitboxGameObject.GetComponent<BoxCollider>();
    }

    public void Attack()
    {
        StartCoroutine(AttackCoroutine());
    }

    void HandleHit()
    {
        //Get everything our hitbox collided with.
        var collided = Physics.OverlapBox(collider.bounds.center,
                                collider.bounds.extents,
                                collider.transform.rotation,
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
}
