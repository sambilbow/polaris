using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freya;

public class triggerManager : MonoBehaviour
{
    public GameObject top;
    public Transform paramspace;
    public LibPdInstance pdPatch;
    public Vector3 closestPoint;
    public Transform bank;
    private float x1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void removeCollisions() {
        Physics.IgnoreCollision(GetComponent<Collider>(), top.GetComponent<Collider>(), true); 
    
    }

    public void addbackCollisions(){
        Physics.IgnoreCollision(GetComponent<Collider>(), top.GetComponent<Collider>(), false); 
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.name == "backboard"){
            GetComponent<Collider>().attachedRigidbody.useGravity = true;

        }
        else{
            GetComponent<Collider>().attachedRigidbody.useGravity = false;


            closestPoint = other.transform.InverseTransformPoint(this.transform.position);
            
            

            GetComponentInChildren<TextMesh>().text = Mathfs.Remap(-0.5f,0.5f,0f,1f,closestPoint.x).ToString();

            pdPatch.SendFloat("lfo3",Mathfs.Remap(-0.5f,0.5f,0f,1f,closestPoint.x));
        }
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "paramspace"){
            pdPatch.SendBang("marbleOn");
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "paramspace"){
            pdPatch.SendBang("marbleOff");
        }
    }


}
