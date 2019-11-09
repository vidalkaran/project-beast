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
    public float intensifyTime;
    public float intensifyMod;
    public float shakeScreenMod;

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
            //Enemy
            c.GetComponent<Controller>().TriggerActorEvent(ActorEvent.HIT_EVENT); //For enemy to take damage... this isn't really good for now because it does not pass values...
            c.GetComponent<Controller>().backLight.IntensifyLight(intensifyTime, intensifyMod);
            c.GetComponent<Controller>().target = this.transform;

            //Player
            playerController.playerCamera.ShakeScreen(shakeScreenMod);
            Instantiate(HitEmitterPrefab, c.transform.position, c.transform.rotation); 

            //Physics
            c.attachedRigidbody.AddForce(transform.forward * attackForce);
            c.attachedRigidbody.AddForce(transform.up * 100); //Hard code 100 for now
            c.transform.LookAt(transform);
        }
    }
}
