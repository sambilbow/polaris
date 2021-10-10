using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class particleLeftCount : MonoBehaviour
{
    public int leftCount;
    //public int rightCount;
    void OnParticleTrigger()
    {
        ParticleSystem ps = gameObject.GetComponent<ParticleSystem>();
 
        // particles
        List<ParticleSystem.Particle> inside = new List<ParticleSystem.Particle>();
        List<ParticleSystem.Particle> insideLeft = new List<ParticleSystem.Particle>();

        // get
        int numInside = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside, out var insideData);
 
        // iterate
        for (int i = 0; i < numInside; i++)
        {

            ParticleSystem.Particle p = inside[i];
            
            // If two colliders are triggered by this particle
            if (insideData.GetColliderCount(i) == 2)
            {
                // and if either of the colliders are the left sphere
                if (insideData.GetCollider(i, 0) == ps.trigger.GetCollider(1) || insideData.GetCollider(i,1) == ps.trigger.GetCollider(1))
                {
                    // color the particle red
                    // p.startColor = new Color32(255, 0, 0, 255);
                    //leftCount+=1;
                    insideLeft.Add(p);

                }
                   
                // otherwise, if either of the colliders are the right sphere
                else if (insideData.GetCollider(i, 0) == ps.trigger.GetCollider(2) || insideData.GetCollider(i,1) == ps.trigger.GetCollider(2))
                {
                    // color the particle blue
                    //p.startColor = new Color32(0, 0, 255, 255);
                    // rightCount+=1;
                }
                    
            }
            
            inside[i] = p;
            leftCount = insideLeft.Count;
            // set
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Inside, inside);
            // ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        }
    }
}