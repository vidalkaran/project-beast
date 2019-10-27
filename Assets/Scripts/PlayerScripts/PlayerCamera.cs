using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //Dependencies
    public Transform cameraSmoother;

    public void Rotate(Vector3 dir)
    {
        cameraSmoother.Rotate(dir, Space.World);
    }
}
