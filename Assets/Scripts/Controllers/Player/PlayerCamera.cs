using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerCamera : MonoBehaviour
{
    //Dependencies
    PlayerController playerController;

    //vars
    public float shakeConstant = .1f;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    public void Rotate(Vector3 dir)
    {
        playerController.cameraSmoother.Rotate(dir, Space.World);
    }

    public void ShakeScreen()
    {
        StartCoroutine("ShakeScreenCoroutine");
        Debug.Log("Test1");
    }

    IEnumerator ShakeScreenCoroutine()
    {
        float shake = shakeConstant;

        while (shake > 0)
        {
            playerController.cameraAnchor.localPosition = playerController.cameraAnchor.localPosition + Random.insideUnitSphere * .1f;
            shake -= Time.deltaTime;
            yield return null;
        }

        playerController.cameraAnchor.localPosition = transform.localPosition;
    }
}
