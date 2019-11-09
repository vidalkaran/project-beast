using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerComponent : MonoBehaviour
{
    protected PlayerController controller;

    public void Awake()
    {
        controller = GetComponent<PlayerController>();
    }
}
