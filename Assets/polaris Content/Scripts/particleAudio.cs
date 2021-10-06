using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Unity.Attributes;

public class particleAudio : MonoBehaviour
{
    public LibPdInstance pdPatch;
    public GameObject handModel;
    private Transform headsetModel;
    // Start is called before the first frame update
    void Start()
    {
        // Find headset Transform
        headsetModel = GameObject.Find("EyeCenter").transform;
        // initisalise palm direction script
        var palmDirectionScript = handModel.GetComponent<PalmDirection>();
        // Set script target to headset
        palmDirectionScript.TargetObject = headsetModel;
    }

    // Update is called once per frame
    void Update()
    {
        // - HANDS --------------------
        
        // ---- PD PHASOR SETTINGS ----
        // Initialise palm direction script
        var palmDirectionScript = handModel.GetComponent<PalmDirection>();
        float angleTo = palmDirectionScript.angleTo;
        // Reverse angle range and fit to 0-1 range for Pd
        angleTo = Mathf.InverseLerp(180f, 0f, angleTo);
        angleTo = Mathf.Lerp(0f, 1f, angleTo);
        // Set phasor strength in Pd
        pdPatch.SendFloat("pd3Phasor",angleTo);

        // - PD CUTOFF SETTINGS - 
        // Initisalise particle position script
        var particlePositionScript = gameObject.GetComponent<particlePosition>();
        // Initisalise finger extension float and set equal from position script
        var extensionFactor = particlePositionScript.extensionFactor;
        extensionFactor = Mathf.InverseLerp(1f, 0f, extensionFactor);
        //extensionFactor = Mathf.Lerp(0f, 1f, extensionFactor);
        // Set cutoff frequency in Pd
        pdPatch.SendFloat("pd1Cutoff",extensionFactor);
        
        // ----------------------------
        // ----------------------------



        // - BODY ---------------------

        // ----------------------------
        // ----------------------------

    }

    
    public void startAudio(){
        pdPatch.SendFloat("pdNoiseToggle",1);
    }
    
    public void stopAudio(){
        pdPatch.SendFloat("pdNoiseToggle",0);
    }
}
