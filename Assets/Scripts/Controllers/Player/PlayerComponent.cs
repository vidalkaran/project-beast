using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerActor))]
public class PlayerComponent : MonoBehaviour
{
    protected PlayerActor actor;

    public void Awake()
    {
        actor = GetComponent<PlayerActor>();
    }
}
