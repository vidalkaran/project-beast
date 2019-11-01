using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerCombat : MonoBehaviour
{
    //Editor Dependencies
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
            Debug.Log("Hitting: " + c.name);
            c.attachedRigidbody.AddForce(transform.forward * attackForce);
            c.attachedRigidbody.AddForce(transform.up * attackForce/2);
        }
    }
}
