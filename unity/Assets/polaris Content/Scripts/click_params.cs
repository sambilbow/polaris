using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freya;

public class click_params : MonoBehaviour
{
    public GameObject rh_instr_click_s;
    public GameObject rh_instr_click_m;
    public float s_distance;
    public ParticleSystem ps;
    private Material material;
    public Vector3 collision_point_s;
    public float s_angular;
    public LibPdInstance pdPatch;

    // Start is called before the first frame update
    void Start()
    {
        material = rh_instr_click_s.GetComponent<Renderer>().material;
        pdPatch.Bind("click");
    }

    // Update is called once per frame
    void Update()
    {
        s_distance = Mathfs.RemapClamped(0f,0.68f,0f,1f,Vector3.Distance(rh_instr_click_s.transform.position, rh_instr_click_m.transform.position));
        
        float reverse_s_distance = Mathfs.Remap(0f,1f,1f,0.4f,s_distance);
        ps.startLifetime = 2*reverse_s_distance;

        s_angular = Mathfs.RemapClamped(0f,3f,0f,1f,rh_instr_click_s.GetComponent<Rigidbody>().angularVelocity.magnitude);

        pdPatch.SendFloat("lfo2freq",s_distance);
        pdPatch.SendFloat("lfo3freq",collision_point_s.x);
        pdPatch.SendFloat("chorus1rate",collision_point_s.y);
        pdPatch.SendFloat("chorus1depth",collision_point_s.z);
        pdPatch.SendFloat("starlight1wet",s_angular);
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.name == "rh_instr_click_s"){
            // ps.Play();
            pdPatch.SendBang("patchOn");
            material.SetColor("_WireColor", new Color(0,115,255,255));
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.name == "rh_instr_click_s"){
            // ps.Stop();
            pdPatch.SendBang("patchOff");
            material.SetColor("_WireColor", new Color(255,255,255,255));            
        }
    }

    void OnTriggerStay (Collider other){
        if(other.gameObject.name == "rh_instr_click_s"){
            collision_point_s = this.transform.InverseTransformPoint(other.transform.position);
            collision_point_s.x = Mathfs.Abs(collision_point_s.x*2);
            collision_point_s.y = Mathfs.Abs(collision_point_s.y*2);
            collision_point_s.z = Mathfs.Abs(collision_point_s.z*2);
            collision_point_s = new Vector3(collision_point_s.x,collision_point_s.y,collision_point_s.z);
        }
    }

    public void BangReceive(string name){
        if(name == "click"){
            ps.Emit(400);
            //wevsters trevsters pudding and pie
            // Debug.Log("received bang from pd");
        }
    }
}
