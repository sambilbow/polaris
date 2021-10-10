using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Unity.Attributes;
using Freya;

public class sphereAudio : MonoBehaviour
{
    public LibPdInstance pdPatch;
    private sphereTimer timerScript;
    private float sphereTimer_L;
    private float sphereTimer_R;
    private float leftHandClamp = 20f;
    private float rightHandClamp = 20f;
    public HandModelBase leftHandModel;
    public HandModelBase rightHandModel;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        // If the left hand is tracked
        if(leftHandModel != null && leftHandModel.IsTracked)
        {
            // ----- LEFT HAND FX -----
            // ------------------------
            // Fade in Left hand FX
            fadeIn("_L");
            timerScript = gameObject.GetComponent<sphereTimer>();
            sphereTimer_L = timerScript.elapsedTime_L;
            // Clamp value to 20 seconds maxmimum for the effect and remap to [0-1]
            sphereTimer_L = Mathf.Clamp(sphereTimer_L,0f,leftHandClamp);
            sphereTimer_L = Mathfs.Remap(0f,20f,0f,1f,sphereTimer_L);
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
        if(rightHandModel != null && rightHandModel.IsTracked)
        {
            // ----- RIGHT HAND FX ----
            // ------------------------
            fadeIn("_R");
            timerScript = gameObject.GetComponent<sphereTimer>();
            sphereTimer_R = timerScript.elapsedTime_R;
            // Clamp value to 20 seconds maxmimum for the effect and remap to [0-1]
            sphereTimer_R = Mathf.Clamp(sphereTimer_R,0f,rightHandClamp);
            sphereTimer_R = Mathfs.Remap(0f,20f,0f,1f,sphereTimer_R);
            // Debug.Log(gameObject.name+" : "+sphereTimer_R);
            pdPatch.SendFloat("rightHandFX",sphereTimer_R);
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
        var receiveName = string.Format("fadeOut"+hand);
        pdPatch.SendBang(receiveName);
    }

    public void fadeIn(string hand)
    {
        var receiveName = string.Format("fadeIn"+hand);
        pdPatch.SendBang(receiveName);
    }

}
