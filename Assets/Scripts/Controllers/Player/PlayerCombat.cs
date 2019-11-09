using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : PlayerComponent
{
    //Editor Dependencies
    public GameObject HitEmitterPrefab;  //Should outsource this to an Object Pool later for performance.

    //Variables
    public float attackForce;
    public float attackWindup;
    public float attackCooldown;
    public float intensifyTime;
    public float intensifyMod;
    public float shakeScreenMod;

    private void Start()
    {
        controller.attackMesh.enabled = false; //Can remove this out when we delete the mesh
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
            //Enemy
            c.GetComponent<Controller>().backLight.IntensifyLight(intensifyTime, intensifyMod);
            c.GetComponent<Controller>().target = this.transform;

            //Player
            controller.playerCamera.ShakeScreen(shakeScreenMod);
            Instantiate(HitEmitterPrefab, c.transform.position, c.transform.rotation);

            //Physics
            c.attachedRigidbody.AddForce(transform.forward * attackForce);
            c.attachedRigidbody.AddForce(transform.up * 100); //Hard code 100 for now
            c.transform.LookAt(transform);
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
