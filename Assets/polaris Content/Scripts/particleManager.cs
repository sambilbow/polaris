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
    public GameObject toggleButton;
    private int toggleButtonState;
    // Update is called once per frame
   void Start(){
       
    //    var interactableScript = toggleButton.GetComponent<Interactable>();
    //    toggleButtonState = interactableScript.GetDimensionIndex();
   }
   
    void Update()
    {
        // left hand 
        if(leftHandModel != null && leftHandModel.IsTracked && toggleButtonState == 1){
            leftHandParticleSystem.Play();
            leftHandAudio.startAudio();
        }
        else{
            leftHandParticleSystem.Stop();
            leftHandAudio.stopAudio();
        }
                

        // right hand
        if(rightHandModel != null && rightHandModel.IsTracked && toggleButtonState == 1){
            rightHandParticleSystem.Play();
            rightHandAudio.startAudio();
        }
        else{
            rightHandParticleSystem.Stop();
            rightHandAudio.stopAudio();
        }
        //Debug.Log(toggleButtonState);
    }

    public void toggleButtonOn(){
        toggleButtonState = 1; 
    }

    public void toggleButtonOff(){
        toggleButtonState = 0;
    }
}
