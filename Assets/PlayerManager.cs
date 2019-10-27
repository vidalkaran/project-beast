using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private float facing;

    private void Start()
    {
        facing = Input.GetAxisRaw("Horizontal");
    }
}
