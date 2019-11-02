using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerCombat : MonoBehaviour
{
    //Editor Dependencies
    public GameObject HitEmitterPrefab;  //Should outsource this to an Object Pool later for performance.

    //Auto load dependencies
    PlayerController playerController;

    //Variables
    public float attackForce;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        playerController.attackMesh.enabled = false;
    }

    // Start is called before the first frame update
    public void Attack()
    {
        var collided = Physics.OverlapBox(playerController.attackCollider.bounds.center,
                                        playerController.attackCollider.bounds.extents,
                                        playerController.attackCollider.transform.rotation,
                                        LayerMask.GetMask("Hitbox"));

        foreach (Collider c in collided)
        {
            c.GetComponent<Controller>().TriggerActorEvent(ActorEvent.HIT_EVENT); //For enemy to take damage.
            playerController.TriggerActorEvent(ActorEvent.HIT_EVENT); //shake screen
            Instantiate(HitEmitterPrefab, c.transform.position, c.transform.rotation);

            c.attachedRigidbody.AddForce(transform.forward * attackForce);
            c.attachedRigidbody.AddForce(transform.up * attackForce/2);
            c.transform.LookAt(transform);
        }
    }
}
