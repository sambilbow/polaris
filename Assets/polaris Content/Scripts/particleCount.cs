using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class particleCount : MonoBehaviour
{
    void OnParticleTrigger()
    {
        ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
 
        // particles
        List<UnityEngine.ParticleSystem.Particle> inside = new List<UnityEngine.ParticleSystem.Particle>();
        List<UnityEngine.ParticleSystem.Particle> exit = new List<UnityEngine.ParticleSystem.Particle>();
 
        // get
        int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside, out var insideData);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
 
        // iterate
        for (int i = 0; i < numInside; i++)
        {
            ParticleSystem.Particle p = inside[i];
            if (insideData.GetColliderCount(i) == 1)
            {
                if (insideData.GetCollider(i, 0) == ps.trigger.GetCollider(0))
                    p.startColor = new Color32(255, 0, 0, 255);
                else
                    p.startColor = new Color32(0, 0, 255, 255);
            }
            else if (insideData.GetColliderCount(i) == 2)
            {
                p.startColor = new Color32(0, 255, 0, 255);
            }
            inside[i] = p;
        }
        for (int i = 0; i < numExit; i++)
        {
            ParticleSystem.Particle p = exit[i];
            p.startColor = new Color32(1, 1, 1, 255);
            exit[i] = p;
        }
        Debug.Log(inside);
        // set
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
    }
}