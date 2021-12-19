using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : PlayerComponent
{
    public float rotationSpeed = .2f;
    public void Rotate(Vector3 dir)
    {
        actor.cameraSmoother.Rotate(dir * rotationSpeed, Space.World);
    }

    public void ShakeScreen(float mod)
    {
        StartCoroutine(ShakeScreenCoroutine(mod));
    }

    IEnumerator ShakeScreenCoroutine(float mod)
    {
        while (mod > 0)
        {
            actor.cameraAnchor.localPosition = actor.cameraAnchor.localPosition + Random.insideUnitSphere * .1f;
            mod -= Time.deltaTime;
            yield return null;
        }
        actor.cameraAnchor.localPosition = transform.localPosition;
    }
}
