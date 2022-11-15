using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BEERLabs;
using Freya;
using Leap;
using Leap.Unity;
using Leap.Unity.Attributes;


public class hand_params : MonoBehaviour
{
    public Transform hmd_t;
    public Vector3 hmd_pos;
    public float hmd_distance;
    public float hand_distance;
    public GameObject hand_l;
    public GameObject hand_r;
    public bool toggleButtonState;
    public LibPdInstance pdPatch;
    public GameObject hand_model_l;
    public GameObject hand_model_r;
    public HandModelBase hand_model_base_l;
    public HandModelBase hand_model_base_r;
    public float angleTo;
    public float extensionFactor;

    // Start is called before the first frame update
    void Start()
    {
        hmd_t = GameObject.Find("NorthStarRigRealsenseRGB").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //maths
        //GO POS
        if(this.gameObject.name == "rh_instr_hand_l"){
            this.transform.position =hand_l.transform.position;
            var scr = hand_model_l.GetComponent<PalmDirection>();
            angleTo = scr.angleTo;

            Hand hand;
            // Set hand variable equal to the attached hand model and get Leap instance
            hand = hand_model_base_l.GetLeapHand();
            // Initialise variable for all fingers in the hand
            var fingers = hand.Fingers;

            extensionFactor = Vector3.Distance(hand.Fingers[1].TipPosition.ToVector3(),hand.Fingers[0].TipPosition.ToVector3());
            extensionFactor = Mathfs.RemapClamped(0f,0.14f,0f,1f,extensionFactor);
            Debug.Log(extensionFactor);
        }
        
        if(this.gameObject.name == "rh_instr_hand_r"){
            this.transform.position =hand_r.transform.position;
            var scr = hand_model_r.GetComponent<PalmDirection>();
            angleTo = scr.angleTo;
            Hand hand;
            // Set hand variable equal to the attached hand model and get Leap instance
            hand = hand_model_base_r.GetLeapHand();
            // Initialise variable for all fingers in the hand
            var fingers = hand.Fingers;

            extensionFactor = Vector3.Distance(hand.Fingers[1].TipPosition.ToVector3(),hand.Fingers[0].TipPosition.ToVector3());
            extensionFactor = Mathfs.RemapClamped(0f,0.14f,0f,1f,extensionFactor);
            Debug.Log(extensionFactor);
        }

        angleTo = Mathfs.Remap(0f,180f,0f,1f,angleTo);

        //PARAMS
        hmd_pos = hmd_t.position;
        
        hmd_distance = Vector3.Distance(hmd_pos,this.transform.position);

        hand_distance = Vector3.Distance(hand_l.transform.position,hand_r.transform.position);

        //AUDIO ONOFF
        if(toggleButtonState==true){
            pdPatch.SendBang("patchOn");
        }
        
        if(toggleButtonState==false){
            pdPatch.SendBang("patchOff");
        }



        // sends
        // commented for dconf pdPatch.SendBang("mega1clock");
        
        
        pdPatch.SendFloat("af1cutoff",hmd_distance);
        pdPatch.SendFloat("af1q",angleTo);
        pdPatch.SendFloat("af1vca",extensionFactor);
        
        
        // wevsters
        // pdPatch.SendBang("audiofreeze1t");           
    }

    public void toggleButtonOn(){
        toggleButtonState = true; 
    }

    public void toggleButtonOff(){
        toggleButtonState = false;
    }
}
