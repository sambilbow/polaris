using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Unity.Attributes;


public class ambi_onOff : MonoBehaviour
{
    public LibPdInstance pdPatch;
    public HandModelBase hand_model;
    public ParticleSystem particle_l;
    public ParticleSystem particle_r;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other){
        if(hand_model!= null && hand_model.IsTracked && this.gameObject.name == "L_index_end" && other.gameObject.name == "ambi_onoff"){
            pdPatch.SendBang("patchOn");
            particle_l.Play();
            Debug.Log("L Enter");
        }
        if(hand_model!= null && hand_model.IsTracked && this.gameObject.name == "R_index_end" && other.gameObject.name == "ambi_onoff"){
            pdPatch.SendBang("patch2On");
            particle_r.Play();
            Debug.Log("R Enter");
        }
    }

    void OnTriggerExit(Collider other){
        if(hand_model!= null && hand_model.IsTracked && this.gameObject.name == "L_index_end" && other.gameObject.name == "ambi_onoff"){
            pdPatch.SendBang("patchOff");
            particle_l.Stop();
            Debug.Log("L Exit");
        }
        if(hand_model!= null && hand_model.IsTracked && this.gameObject.name == "R_index_end" && other.gameObject.name == "ambi_onoff"){
            pdPatch.SendBang("patch2Off");
            particle_r.Stop();
            Debug.Log("R Exit");
        }
    }
}
