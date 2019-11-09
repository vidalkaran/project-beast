using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuyCombat : MonoBehaviour
{
    BadGuyController badGuyController;

    private void Awake()
    {
        badGuyController = GetComponent<BadGuyController>();
    }

    public void GetHit()
    {
        Debug.Log("ya boi got hit");
    }

}
