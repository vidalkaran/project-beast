using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This plugin allows rotation of the camera based on input
 */
public class PlayerCameraController : MonoBehaviour
{
    PlayerController controller;
    public float rotateSpeed = 1f;

    private void Awake()
    {
        controller = GetComponent<PlayerController>(); 
    }
    //Handle input
    private void Update()
    {
        //Camera
        if (Input.GetKey(KeyCode.Q))
            controller.cameraSmoother.Rotate(Vector3.up * rotateSpeed, Space.World);
        if (Input.GetKey(KeyCode.E))
            controller.cameraSmoother.Rotate(Vector3.down * rotateSpeed, Space.World);
    }

    public void ShakeScreen(float mod)
    {
        StartCoroutine(ShakeScreenCoroutine(mod));
    }

    IEnumerator ShakeScreenCoroutine(float mod)
    {
        while (mod > 0)
        {
            controller.cameraAnchor.localPosition = controller.cameraAnchor.localPosition + Random.insideUnitSphere * .1f;
            mod -= Time.deltaTime;
            yield return null;
        }
        controller.cameraAnchor.localPosition = transform.localPosition;
    }
}
