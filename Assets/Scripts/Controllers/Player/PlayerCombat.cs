using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : PlayerComponent
{
    //Design thoughts
    //To extend this to multiple attacks and combos, should create an "AttackObject" that holds the attack force, windup, cooldown, ect, and belongs to the hitbox/mesh gameobject, 
    //and have this script reference it and act as a middleman between the attack object and the playercontroller.

    //Editor Dependencies
    public GameObject HitEmitterPrefab;  //Should outsource this to an Object Pool later for performance.
    public MeshRenderer attackMesh; //The hitbox of this attack.

    //Variables
    public float attackForce;
    public float attackWindup;
    public float attackCooldown;
    public float intensifyTime;
    public float intensifyMod;
    public float shakeScreenMod;

    private void Start()
    {
        attackMesh.enabled = false; //Can remove this out when we delete the mesh
    }

    // Start is called before the first frame update
    public void Attack()
    {
        controller.state = ActorState.ATTACKING_STATE;
        StartCoroutine(AttackCoroutine());
    }

    void HandleHit()
    {
        var collided = Physics.OverlapBox(controller.attackCollider.bounds.center,
                                controller.attackCollider.bounds.extents,
                                controller.attackCollider.transform.rotation,
                                LayerMask.GetMask("Hitbox"));

        foreach (Collider c in collided)
        {
            //Player
            controller.playerCamera.ShakeScreen(shakeScreenMod);
            Instantiate(HitEmitterPrefab, c.transform.position, c.transform.rotation);

            //Physics
            c.attachedRigidbody.AddForce(transform.forward * attackForce);
            c.attachedRigidbody.AddForce(transform.up * 100); //Hard code 100 for now
            c.transform.LookAt(transform);

            //Enemy
            c.GetComponent<Controller>().backLight.IntensifyLight(intensifyTime, intensifyMod);
            c.GetComponent<Controller>().TakeDamage(1);
        }
    }

    //The attack coroutine.
    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(attackWindup);
        HandleHit();
        yield return new WaitForSeconds(attackCooldown);
        controller.state = ActorState.IDLE_STATE;
    }
}
