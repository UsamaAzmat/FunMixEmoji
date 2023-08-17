using UnityEngine;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour
{
    public List<ParticleSystem> particles = new List<ParticleSystem>();

    // Play a specific particle effect by name
    public void PlayParticle(string particleName, Vector3 pos, bool applyPos = true)
    {
        ParticleSystem particle = particles.Find(p => p.name == particleName);
        if (particle != null)
        {
            particle.Play();
            if (applyPos) particle.transform.position = pos;
        }
        else
        {
            Debug.LogWarning("Particle effect not found: " + particleName);
        }
    }

    // Play a random particle effect from the list
    public void PlayRandomParticle()
    {
        if (particles.Count > 0)
        {
            int randomIndex = Random.Range(0, particles.Count);
            ParticleSystem randomParticle = particles[randomIndex];
            randomParticle.Play();

        }
        else
        {
            Debug.LogWarning("No particles in the list.");
        }
    }

    // Stop a specific particle effect by name
    public void StopParticle(string particleName)
    {
        ParticleSystem particle = particles.Find(p => p.name == particleName);
        if (particle != null)
        {
            particle.Stop();
        }
        else
        {
            Debug.LogWarning("Particle effect not found: " + particleName);
        }
    }
}
