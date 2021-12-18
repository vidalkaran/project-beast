using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : PlayerComponent
{
    //Design thoughts
    //To extend this to multiple attacks and combos, should create an "AttackObject" that holds the attack force, windup, cooldown, ect, and belongs to the hitbox/mesh gameobject, 
    //and have this script reference it and act as a middleman between the attack object and the playercontroller.

    public AttackData basicAttack; // Note, eventually have this be a list and tie it to combo system or to player input somehow
    GameObject hitboxGameObject; //Note, need to store this somewhere useful. Maybe assign it back to the scriptable object?

    private void Start()
    {
        //Set up mesh
        hitboxGameObject = Instantiate(basicAttack.hitboxPrefab, new Vector3(0, 0, .5f), Quaternion.identity);
        hitboxGameObject.transform.SetParent(this.gameObject.transform, false);
        hitboxGameObject.GetComponent<MeshRenderer>().enabled = false; //Disabling debug renderer
    }

    public void Attack()
    {
        controller.state = ActorState.ATTACKING_STATE;
        StartCoroutine(AttackCoroutine());
    }

    void HandleHit()
    {
        var collider = hitboxGameObject.GetComponent<BoxCollider>();

        var collided = Physics.OverlapBox(collider.bounds.center,
                                collider.bounds.extents,
                                collider.transform.rotation,
                                LayerMask.GetMask("Hitbox"));

        foreach (Collider c in collided)
        {
            //Player
            controller.playerCamera.ShakeScreen(basicAttack.shakeScreenMod);
            Instantiate(basicAttack.HitEmitterPrefab, c.transform.position, c.transform.rotation);

            //Physics
            c.attachedRigidbody.AddForce(transform.forward * basicAttack.attackForce);
            c.attachedRigidbody.AddForce(transform.up * 100); //Hard code 100 for now
            c.transform.LookAt(transform);

            //Enemy
            c.GetComponent<Controller>().backLight.IntensifyLight(basicAttack.intensifyTime, basicAttack.intensifyMod);
            c.GetComponent<Controller>().TakeDamage(1);
        }
    }

    //The attack coroutine.
    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(basicAttack.attackWindup);
        HandleHit();
        yield return new WaitForSeconds(basicAttack.attackCooldown);
        controller.state = ActorState.IDLE_STATE;
    }
}
