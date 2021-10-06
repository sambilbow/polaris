using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity;

public class emissionSpherePd : MonoBehaviour
{
    public LibPdInstance pdPatch;
    public ParticleSystem ps;
    public Transform handModel;
    private FingerDirection fingerDirection;
    private float particleCount;
    //private FingerDirection fingerDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        particleCount = ps.particleCount;
        pdPatch.SendFloat("emission_delay", (particleCount/10000));

        //float angle = handModel.GetComponent<FingerDirection>().angleTo;
        float angle = handModel.GetComponent<PalmDirection>().angleTo;
        angle = ((((angle/180)+1)/-1)+2);
        pdPatch.SendFloat("emission_resonance", angle);

    }

    public void activateAudio(){
        
        pdPatch.SendBang("emission_toggle");
    }
    
    public void activateParticle(){
        if(ps.isEmitting == false){
            ps.Play();
        }
        else{
            ps.Stop();
        }
         
    }
}
