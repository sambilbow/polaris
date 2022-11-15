using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Unity.Attributes;
using Microsoft.MixedReality.Toolkit.UI;


public class particleManager : MonoBehaviour
{
    public HandModelBase leftHandModel;
    public HandModelBase rightHandModel;
    public ParticleSystem leftHandParticleSystem;
    public ParticleSystem rightHandParticleSystem;
    public particleAudio leftHandAudio;
    public particleAudio rightHandAudio;
    //public GameObject toggleButton;
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
            leftHandAudio.startAudio();
        }
        else
        {
            leftHandParticleSystem.Stop();
            leftHandAudio.stopAudio();
        }
                

        // right hand
        if(rightHandModel != null && rightHandModel.IsTracked && toggleButtonState == true)
        {
            rightHandParticleSystem.Play();
            rightHandAudio.startAudio();
        }
        else
        {
            rightHandParticleSystem.Stop();
            rightHandAudio.stopAudio();
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
