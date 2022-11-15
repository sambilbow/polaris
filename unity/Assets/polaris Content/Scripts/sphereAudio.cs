using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Unity.Attributes;
using Freya;
using TMPro;

public class sphereAudio : MonoBehaviour
{
    public LibPdInstance pdPatch;
    private sphereTimer timerScript;
    private float sphereTimer_L;
    private float sphereTimer_R;
    private float leftHandClamp = 20f; // time for left hand fx to ramp up 
    private float rightHandClamp = 5f; // tiime for right hand fx to ramp up
    public HandModelBase leftHandModel;
    public HandModelBase rightHandModel;
    public ParticleSystem childParticleSystem;
    public particleManager particleManager;
    public bool toggleButtonState;

    // Start is called before the first frame update
    void Start()
    {
        pdPatch.SendBang("initialisePatch");
        pdPatch.SendSymbol("label",gameObject.name);
        pdPatch.Bind(gameObject.name+"-clock");
        // Debug.Log(gameObject.name);
        timerScript = gameObject.GetComponent<sphereTimer>();

    }

    // Update is called once per frame
    void Update()
    {
        toggleButtonState = particleManager.toggleButtonState;
        
        // Debug labels
        // var lhL = gameObject.transform.Find("leftHandLabel");
        // var lhTMP = lhL.GetComponent<TextMeshPro>();
        // lhTMP.SetText("left elapsed: {0}",timerScript.elapsedTime_L);

        // var rhL = gameObject.transform.Find("rightHandLabel");
        // var rhTMP = rhL.GetComponent<TextMeshPro>();
        // rhTMP.SetText("right elapsed: {0}",timerScript.elapsedTime_R);
        
        // If the left hand is tracked
        if(leftHandModel != null && leftHandModel.IsTracked && toggleButtonState == true)
        {
            // ----- LEFT HAND FX -----
            // ------------------------
            // Fade in Left hand FX
            fadeIn("_L");
            timerScript = gameObject.GetComponent<sphereTimer>();
            sphereTimer_L = timerScript.elapsedTime_L;
            // Clamp value to x seconds maxmimum for the effect and remap to [0-1]
            sphereTimer_L = Mathf.Clamp(sphereTimer_L,0f,leftHandClamp);
            sphereTimer_L = Mathfs.Remap(0f,leftHandClamp,0f,1f,sphereTimer_L);
            // Debug.Log(gameObject.name+" : "+sphereTimer_L);
            pdPatch.SendFloat("leftHandFX",sphereTimer_L);
            // ------------------------
        }
        
        // If the left hand is not tracked
        else
        {
            fadeOut("_L");
        }

        // If the right hand is tracked
        if(rightHandModel != null && rightHandModel.IsTracked && toggleButtonState == true)
        {
            // ----- RIGHT HAND FX ----
            // ------------------------
            fadeIn("_R");
            timerScript = gameObject.GetComponent<sphereTimer>();
            sphereTimer_R = timerScript.elapsedTime_R;
            // Clamp value to x seconds maxmimum for the effect and remap to [0-1]
            sphereTimer_R = Mathf.Clamp(sphereTimer_R,0f,rightHandClamp);
            sphereTimer_R = Mathfs.Remap(0f,rightHandClamp,0f,1f,sphereTimer_R);
            // Debug.Log(gameObject.name+" : "+sphereTimer_R);
            pdPatch.SendFloat("rightHandFX",sphereTimer_R);

            var no = childParticleSystem.noise;
            no.strength = sphereTimer_R;
            // ------------------------
        }
        
        // If the right hand is not tracked
        else
        {
            fadeOut("_R");
        }        

    }
    
    public void fadeOut(string hand)
    {
        if (toggleButtonState == true){
            var receiveNameOut = string.Format("fadeOut"+hand);
            pdPatch.SendBang(receiveNameOut);
            var no = childParticleSystem.noise;
            no.strength = 0;
        }
    }

    public void fadeIn(string hand)
    {
        if (toggleButtonState ==true){
            var receiveNameIn = string.Format("fadeIn"+hand);
            pdPatch.SendBang(receiveNameIn);
        }
    }

    public void BangReceive(string name)
    {
        // If the name received by the LibPdInstance event equals the object name
        if (name == gameObject.name+"-clock")
        {
            // Print that you received a bang
            //Debug.Log("Received bang from: "+name);
            
            // One day soon, do some awesome audiovisualisation of the Pd patch in 3D!
            childParticleSystem.Play();
        }
    }
}
