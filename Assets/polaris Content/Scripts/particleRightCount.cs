using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class particleRightCount : MonoBehaviour
{
    private List<ParticleSystem.Particle> left;
    private List<ParticleSystem.Particle> right;
    //public int leftCount;
    public int rightCount;
    void OnParticleTrigger()
    {
        ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
 
        // particles
        List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();
 
        // get
        int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside, out var insideData);
        int numExit = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
 
        // iterate
        for (int i = 0; i < numInside; i++)
        {
            ParticleSystem.Particle p = inside[i];
            
            // If two colliders are triggered by this particle
            if (insideData.GetColliderCount(i) == 2)
            {
                // otherwise, if either of the colliders are the right sphere
                if (insideData.GetCollider(i, 0) == ps.trigger.GetCollider(2) || insideData.GetCollider(i,1) == ps.trigger.GetCollider(2))
                {
                    // color the particle blue
                    //p.startColor = new Color32(0, 0, 255, 255);
                    inside[i] = p;
                    rightCount=inside.Count;
                }
                    
            }

        // set
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
        // ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);
        }


    }
}