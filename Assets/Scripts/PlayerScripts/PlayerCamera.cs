using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerCamera : MonoBehaviour
{
    //Dependencies
    PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void Rotate(Vector3 dir)
    {
        playerController.cameraSmoother.Rotate(dir, Space.World);
    }

    //public void Shake()
    //
}
