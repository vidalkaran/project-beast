using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : PlayerComponent
{
    public void Rotate(Vector3 dir)
    {
        controller.cameraSmoother.Rotate(dir, Space.World);
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
