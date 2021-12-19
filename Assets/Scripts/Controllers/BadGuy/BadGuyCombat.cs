using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuyCombat : MonoBehaviour
{
    BadGuyActor badGuyController;

    private void Awake()
    {
        badGuyController = GetComponent<BadGuyActor>();
    }

    public void GetHit()
    {
        Debug.Log("ya boi got hit");
    }

}
