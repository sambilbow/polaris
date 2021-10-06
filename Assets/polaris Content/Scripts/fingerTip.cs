using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fingerTip : MonoBehaviour
{
    

    private Transform rightHand;
    private Transform leftHand;

    public Material rightHandMaterial;
    public Material leftHandMaterial;
    private Collider groundCollider;

    // Start is called before the first frame update
    void Start()
    {
        var leapContactL = GameObject.Find("Left Interaction Hand Contact Bones");
        leftHand = leapContactL.transform.GetChild(6).gameObject.transform; 
        
        var leapContactR = GameObject.Find("Right Interaction Hand Contact Bones");
        rightHand = leapContactR.transform.GetChild(6).gameObject.transform; 
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {

        // Find the closest point on the collider to the left hand
        Vector3 closestToLeftHand  = other.ClosestPoint(leftHand.position);
        if (Vector3.Distance(closestToLeftHand, leftHand.position) < .03f)
        {
            LeaveTrail(closestToLeftHand, 0.01f, leftHandMaterial);
        }

        // Find the closest point on the collider to the right hand
        Vector3 closestToRightHand = other.ClosestPoint(rightHand.position);
        if (Vector3.Distance(closestToRightHand, rightHand.position) < .03f)
        {
            LeaveTrail(closestToRightHand, 0.01f, rightHandMaterial);
        }
    }

    private void LeaveTrail(Vector3 point, float scale, Material material)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = Vector3.one * scale;
        sphere.transform.position = point;
        sphere.transform.parent = transform.parent;
        sphere.GetComponent<Collider>().enabled = false;
        sphere.GetComponent<Renderer>().material = material;
        sphere.GetComponent<Renderer>().material.SetColor("_Color", new Color(Random.Range(0f,1.0f),Random.Range(0f,1.0f),Random.Range(0f,1.0f),1));
        Destroy(sphere, 1f);
    }
}
