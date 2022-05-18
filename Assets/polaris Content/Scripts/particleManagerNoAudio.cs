using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Unity.Attributes;
using Microsoft.MixedReality.Toolkit.UI;


public class particleManagerNoAudio : MonoBehaviour
{
    public HandModelBase leftHandModel;
    public HandModelBase rightHandModel;
    public ParticleSystem leftHandParticleSystem;
    public ParticleSystem rightHandParticleSystem;
    public bool toggleButtonState;
    // Update is called once per frame
   void Start(){
       
    //    var interactableScript = toggleButton.GetComponent<Interactable>();
    //    toggleButtonState = interactableScript.GetDimensionIndex();
   }
   
    void Update()
    {
        // left hand 
        if(leftHandModel != null && leftHandModel.IsTracked && toggleButtonState == true)
        {
            leftHandParticleSystem.Play();
        }
        else
        {
            leftHandParticleSystem.Stop();
        }
                

        // right hand
        if(rightHandModel != null && rightHandModel.IsTracked && toggleButtonState == true)
        {
            rightHandParticleSystem.Play();
        }
        else
        {
            rightHandParticleSystem.Stop();
        }
        //Debug.Log(toggleButtonState);
    }

    public void toggleButtonOn(){
        toggleButtonState = true; 
    }

    public void toggleButtonOff(){
        toggleButtonState = false;
    }
}
