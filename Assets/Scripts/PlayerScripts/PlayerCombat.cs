using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //Editor Dependencies
    public GameObject attackObject;
    public float attackForce;
    public PlayerInput playerInput;
    public Animator animator;
    public Collider attackCollider;
    public MeshRenderer mesh;

    //Variables
    private bool attackCooldown = false;

    private void Start()
    {
        mesh.enabled = false;
    }

    // Start is called before the first frame update
    public void Attack()
    {
        //Only attacks if we are not cooling down from a previous attack.
        if (!attackCooldown)
        {
            var collided = Physics.OverlapBox(attackCollider.bounds.center,
                                          attackCollider.bounds.extents,
                                          attackCollider.transform.rotation,
                                          LayerMask.GetMask("Hitbox"));

            attackCooldown = true;
            playerInput.playerState = PlayerInput.PlayerState.attacking;
            StartCoroutine("playAnim");

            foreach (Collider c in collided)
            {
                Debug.Log("Hitting: " + c.name);
                c.attachedRigidbody.AddForce(transform.forward * attackForce);
                c.attachedRigidbody.AddForce(transform.up * attackForce/2);
            }
        }
    }

    //Temporary to mock a hypothetical attack animation. 
    IEnumerator playAnim()
    {
        for(int i = 0; i<2; i++)
            yield return new WaitForSeconds(.1f);

        attackCooldown = false;
        playerInput.playerState = PlayerInput.PlayerState.idle;
    }
}
