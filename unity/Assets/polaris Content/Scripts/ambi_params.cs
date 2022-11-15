using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freya;
using Leap;
using Leap.Unity;
using Leap.Unity.Attributes;

public class ambi_params : MonoBehaviour
{
    public Vector3 ambi_pos;
    public float ambi_scale;
    public float ambi_distance_l = 2;
    public float ambi_distance_r = 2;
    public LibPdInstance pdPatch;
    public GameObject hand_l;
    public GameObject hand_r;
    public HandModelBase hand_model_l;
    public HandModelBase hand_model_r;
    public ParticleSystem particle_l;
    public ParticleSystem particle_r;
    public Vector3 collision_point_l;
    public Vector3 collision_point_r;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // maths
        //particle_l.transform = 
        ambi_pos = transform.position;
        ambi_scale = transform.localScale.x;
        //ambi_vel = this.GetComponent<Rigidbody>().velocity;
        
        ambi_distance_l = Mathfs.Remap(0.15f,1f,0f,1f,(Vector3.Distance(hand_l.transform.position, ambi_pos)));
        ambi_distance_r = Mathfs.Remap(0.15f,1f,0f,1f,(Vector3.Distance(hand_r.transform.position, ambi_pos)));
        
        float reverse_ambi_distance_l = Mathfs.RemapClamped(0.15f,1f,1f,0f,(Vector3.Distance(hand_l.transform.position, ambi_pos)));
        float reverse_ambi_distance_r = Mathfs.RemapClamped(0.15f,1f,1f,0f,(Vector3.Distance(hand_r.transform.position, ambi_pos)));
        

        // sends
        if (hand_model_l!= null && hand_model_l.IsTracked){
            pdPatch.SendFloat("osc1pitch", collision_point_l.y);    
            //pdPatch.SendFloat("vca1",reverse_ambi_distance_l);
            pdPatch.SendFloat("megaverb1wet",collision_point_l.x);
            pdPatch.SendFloat("starlight1wet",collision_point_l.z);
        }
        
        if (hand_model_r!= null && hand_model_r.IsTracked){
            pdPatch.SendFloat("osc2pitch", collision_point_r.y);
            //pdPatch.SendFloat("vca2",reverse_ambi_distance_r);
            pdPatch.SendFloat("lp1cutoff",collision_point_r.z);
            pdPatch.SendFloat("trigons1pitch",collision_point_r.x);
        }
        
        pdPatch.SendFloat("lp2cutoff",Mathfs.RemapClamped(0.0001f,1.8f,0.8f,0.0001f,ambi_scale));

        particle_l.startSize = Mathfs.RemapClamped(0.0001f,1.8f,0.5f,5f,ambi_scale);
        particle_r.startSize = Mathfs.RemapClamped(0.0001f,1.8f,0.5f,5f,ambi_scale);
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "L_index_end"){
            pdPatch.SendBang("patchOn");
                particle_l.Play();
                
            Debug.Log("L Enter");
        }
        if(other.gameObject.name == "R_index_end"){
            pdPatch.SendBang("patch2On");
                particle_r.Play();
                
            Debug.Log("R Enter");
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.name == "L_index_end"){
            pdPatch.SendBang("patchOff");
                particle_l.Stop();
                
            Debug.Log("L Exit");
        }
        if(other.gameObject.name == "R_index_end"){
            pdPatch.SendBang("patch2Off");
                particle_r.Stop();
                
            Debug.Log("R Exit");
        }
    }

    // void OnTriggerStay(Collider other){
    //     if(other.gameObject.name == "L_index_end"){
    //         collision_point_l = this.transform.InverseTransformPoint(other.transform.position);
    //         collision_point_l.x = Mathfs.Remap(-2f,2f,0f,1f,collision_point_l.x);
    //         collision_point_l.y = Mathfs.Remap(-2f,2f,0f,1f,collision_point_l.y);
    //         collision_point_l.z = Mathfs.Remap(-2f,2f,0f,1f,collision_point_l.z);
    //         collision_point_l = new Vector3(collision_point_l.x,collision_point_l.y,collision_point_l.z);
    //         //Debug.Log(collision_point_l);
    //     }
        
    //     if(other.gameObject.name == "R_index_end"){
    //         collision_point_r = this.transform.InverseTransformPoint(other.transform.position);
    //         collision_point_r.x = Mathfs.Remap(-2f,2f,0f,1f,collision_point_r.x);
    //         collision_point_r.y = Mathfs.Remap(-2f,2f,0f,1f,collision_point_r.y);
    //         collision_point_r.z = Mathfs.Remap(-2f,2f,0f,1f,collision_point_r.z);
    //         collision_point_r = new Vector3(collision_point_r.x,collision_point_r.y,collision_point_r.z);
    //         //Debug.Log(collision_point_r);
    //     }
        
    // }

    void OnTriggerStay(Collider other){
        if(other.gameObject.name == "L_index_end"){
            collision_point_l = this.transform.InverseTransformPoint(other.transform.position);
            collision_point_l.x = Mathfs.Abs(collision_point_l.x/2);
            collision_point_l.y = Mathfs.Abs(collision_point_l.y/2);
            collision_point_l.z = Mathfs.Abs(collision_point_l.z/2);
            collision_point_l = new Vector3(collision_point_l.x,collision_point_l.y,collision_point_l.z);
            //Debug.Log(collision_point_l);
        }
        
        if(other.gameObject.name == "R_index_end"){
            collision_point_r = this.transform.InverseTransformPoint(other.transform.position);
            collision_point_r.x = Mathfs.Abs(collision_point_r.x/2);
            collision_point_r.y = Mathfs.Abs(collision_point_r.y/2);
            collision_point_r.z = Mathfs.Abs(collision_point_r.z/2);
            collision_point_r = new Vector3(collision_point_r.x,collision_point_r.y,collision_point_r.z);
            //Debug.Log(collision_point_r);
        }
        
    }
}
