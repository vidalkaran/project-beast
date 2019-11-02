using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEmitterScript : MonoBehaviour
{
    ParticleSystem[] hitParticles;
    Transform cameraTransform;

    // Start is called before the first frame update
    void Awake()
    {
        hitParticles = transform.GetComponentsInChildren<ParticleSystem>();
        cameraTransform = Camera.main.transform;
        Start();
    }

    private void Update()
    {
        transform.LookAt(cameraTransform);

        foreach (ParticleSystem particle in hitParticles)
        {
            if (!particle.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }

    public void Start()
    {
        foreach (ParticleSystem particle in hitParticles)
        {
            particle.Play();
        }
    }
    
}
