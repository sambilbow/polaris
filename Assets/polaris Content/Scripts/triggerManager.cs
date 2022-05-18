using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freya;

public class triggerManager : MonoBehaviour
{
    public GameObject top;
    public Transform paramspace;
    public LibPdInstance pdPatch;
    public Vector3 marblePosition;
    public Transform bank;

    // NOCLIP
    public void removeCollisions() {
        Physics.IgnoreCollision(GetComponent<Collider>(), top.GetComponent<Collider>(), true); 
    
    }

    // CLIP
    public void addbackCollisions(){
        Physics.IgnoreCollision(GetComponent<Collider>(), top.GetComponent<Collider>(), false); 
    }

    // WHEN TRIGGERING A COLLIDER
    private void OnTriggerStay(Collider other) {
        
        // IF COLLIDING WITH BACKBOARD TURN ON GRAVITY
        if (other.gameObject.name == "backboard"){
            GetComponent<Collider>().attachedRigidbody.useGravity = true;

        }
        
        // IF COLLIDING WITH ANYTHING OTHER THAN BACKBOARD TURN OFF GRAVITY
        else{

            GetComponent<Collider>().attachedRigidbody.useGravity = false;

            // GET POSITION RELATIVE TO CONTAINER
            marblePosition = other.transform.InverseTransformPoint(this.transform.position);
            
            // DEFINE INDIVIDUAL POSITIONS 
            float marblePositionX = marblePosition.x;
            float marblePositionY = marblePosition.y;
            float marblePositionZ = marblePosition.z;

            // SHOW DEBUG TEXT FOR POSITIONS
            paramspace.GetChild(0).GetComponent<TextMesh>().text = Mathfs.Remap(-0.5f,0.5f,0f,1f,marblePositionX).ToString();
            paramspace.GetChild(1).GetComponent<TextMesh>().text = Mathfs.Remap(-0.5f,0.5f,0f,1f,marblePositionY).ToString();
            paramspace.GetChild(2).GetComponent<TextMesh>().text = Mathfs.Remap(-0.5f,0.5f,0f,1f,marblePositionZ).ToString();                        
            
            
            // AUDIO EFFECTS
            pdPatch.SendFloat("lfo3",Mathfs.Remap(-0.5f,0.5f,0f,1f,marblePosition.x));
        }
        
    }

    // TURN ON PATCH WHEN ENTERING CONTAINER
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "paramspace"){
            pdPatch.SendBang("marbleOn");
        }
    }

    // TURN OFF PATCH WHEN EXITING CONTAINER
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.name == "paramspace"){
            pdPatch.SendBang("marbleOff");
        }
    }


}
