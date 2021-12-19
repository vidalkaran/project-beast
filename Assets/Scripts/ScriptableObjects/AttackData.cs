using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This acts as a data container for the different attack types actors can have. Can maybe extend to projectiles later.
[CreateAssetMenu(fileName = "New Attack Data", menuName = "Actor/AttackData", order = 51)]
public class AttackData : ScriptableObject
{
    public GameObject HitEmitterPrefab;  //The hit effect Note: Should outsource this to an Object Pool later for performance.
    public GameObject hitboxPrefab; //The hitbox mesh of this attack.
    // Animation // Not sure how to do this yet but this should know the animation to use
    public float attackDamage; //Numerical damage of the attack
    //public AttackType attackType; //The damage type. To be implemented
    public float attackForce; //How much knockback the attack has
    public float attackWindup; //Windup in seconds
    public float attackCooldown; //Cooldown in seconds
    public float intensifyTime; //How long to intensify the light of the scene on impact
    public float intensifyMod; //How much to intensify the scene on impact
    public float shakeScreenMod; //How much to shake the screen
}
