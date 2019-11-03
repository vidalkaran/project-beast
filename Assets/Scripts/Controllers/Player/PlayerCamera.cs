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

    public void ShakeScreen(float mod)
    {
        StartCoroutine(ShakeScreenCoroutine(mod));
    }

    IEnumerator ShakeScreenCoroutine(float mod)
    {
        while (mod > 0)
        {
            playerController.cameraAnchor.localPosition = playerController.cameraAnchor.localPosition + Random.insideUnitSphere * .1f;
            mod -= Time.deltaTime;
            yield return null;
        }
        playerController.cameraAnchor.localPosition = transform.localPosition;
    }
}
