using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Unity.Attributes;

public class particlePosition : MonoBehaviour
{

    public HandModelBase handModel;
    public Transform palm;
    private Vector3 fingerCentrePosition;
    public float extensionFactor;
    //public GameObject debugSphere;
    public AnimationCurve logCurve;

    // Update is called once per frame
    void Update()
    {
            // Check if hand is active and being tracked by Leap
            if(handModel != null && handModel.IsTracked){
                
                // ---- COMPUTE VALUES ----

                // Initisalise hand variable
                Hand hand;
                // Set hand variable equal to the attached hand model and get Leap instance
                hand = handModel.GetLeapHand();
                // Initialise variable for all fingers in the hand
                var fingers = hand.Fingers;
                // For each finger stored in our finger variable, add their position to the Vector3 for the central position 
                float fingerCentrePositionX = 0f;
                float fingerCentrePositionY = 0f;
                float fingerCentrePositionZ = 0f;

                foreach (var finger in fingers)
                {
                   fingerCentrePositionX+=finger.TipPosition.x;
                   fingerCentrePositionY+=finger.TipPosition.y;
                   fingerCentrePositionZ+=finger.TipPosition.z;
                };
                // Add the lower palm coordinate too, to make sure its not to weighted a the top of the hand 
                //fingerCentrePositionX+=palm.transform.position.x;
                //fingerCentrePositionY+=palm.transform.position.y;
                //fingerCentrePositionZ+=palm.transform.position.z;
                
                float centrex = fingerCentrePositionX / 5;
                float centrey = fingerCentrePositionY / 5;
                float centrez = fingerCentrePositionZ / 5;

                // Set fingerCentrePosition variable equal to the average centre of all the fingers + palm // Divide the values by 6 (5 hands + palm) to find average distance
                fingerCentrePosition = new Vector3(centrex,centrey,centrez);
      
                // Set extensionFactor equal to the relative distance between pinky and thumb
                extensionFactor = Vector3.Distance(hand.Fingers[1].TipPosition.ToVector3(),hand.Fingers[0].TipPosition.ToVector3());
                //Debug.Log(extensionFactor);
                
                // ------ DEBUG --------

                // Place a sphere at the proposed central position
                //debugSphere.transform.position = fingerCentrePosition;
                // Translate the sphere down the arm length 5cm
                //debugSphere.transform.Translate(Vector3.right*0.03f,Space.Self);
                //debugSphere.transform.Translate(Vector3.up*0.05f,Space.Self);
                //debugSphere.transform.Translate(Vector3.forward*0.05f,Space.Self);
                // Push this sphere position forwards along the palm normal by an amount proportional to extensionFactor
                // debugSphere.transform.Translate(Vector3.forward*extensionFactor, palm.transform);
                //debugSphere.transform.rotation = palm.transform.rotation;
                // Draw a ray from the lower palm to to the central position
                //Debug.DrawRay(fingerCentrePosition,palm.transform.position-fingerCentrePosition);
                // Draw a ray from each finger's TipPosition to the central position
                // foreach (var finger in fingers)
                // {
                //     Debug.DrawRay(fingerCentrePosition, new Vector3(finger.TipPosition.x,finger.TipPosition.y,finger.TipPosition.z)-fingerCentrePosition);
                // };



                // ------ SET VALUES -------


                
                // Set the position of the Particle System equal tot he central position
                transform.position = fingerCentrePosition;
                // Push this position forwards along the palm normal by an amount proportional to extensionFactor
                //transform.Translate(Vector3.forward*extensionFactor, palm.transform);
                // Set the rotation equal to that of the palm
                transform.rotation = palm.transform.rotation;


                // ------ PARTICLE SYSTEM FX -------

                // Find Particle System component
                ParticleSystem ps = GetComponent<ParticleSystem>();
                
                // Initialise variable for the shape module of the component
                var sh = ps.shape;

                // Store a scaled value of the extension factor
                //extensionFactor = SuperLerp(0.0001f, 1.0f, 0.0f, 0.16f, extensionFactor);
                //Debug.Log(radiusValue);
                extensionFactor = Mathf.InverseLerp(0f, 0.16f, extensionFactor);
                // Debug.Log(normal);
                extensionFactor = Mathf.Lerp(0f, 1f, extensionFactor);
                float radiusValue = (logCurve.Evaluate(extensionFactor))*40;
                // float radiusValue = Mathf.Pow(-2.718f,(-5.0f)*extensionFactor) + 1f;
                // Set radius of the particle stream equal to this scaled value
                 sh.radius = radiusValue;
                //Debug.Log(radiusValue);

            };

    }

}
