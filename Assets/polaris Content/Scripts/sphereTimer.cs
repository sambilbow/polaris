using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class sphereTimer : MonoBehaviour
{
    private TimeSpan timePlaying_L;
    private bool timerGoing_L;
    public float elapsedTime_L;

    private TimeSpan timePlaying_R;
    private bool timerGoing_R;
    public float elapsedTime_R;

    // Start is called before the first frame update
    void Start()
    {
        timerGoing_L = false;
        timerGoing_R = false;
    }

    // Update is called once per frame
    void Update()
    {
        // if(timerGoing_L == true){
        //     Debug.Log(gameObject.name+" + Left Hand: "+elapsedTime_L);
        // }
        
        // if(timerGoing_R == true){
        //     Debug.Log(gameObject.name+" + Right Hand: "+elapsedTime_R);
        // }
    }
    
    // ------- LEFT HAND ---------
    // ---------------------------
    // ---------------------------
    public void beginTimer_L(){
        // Debug.Log("Pointing at Sphere 1");
        timerGoing_L = true;
        elapsedTime_L = 0f;
        StartCoroutine(UpdateTimer_L());
    }

    public void endTimer_L(){
        // Debug.Log("Stopped pointing at Sphere 1");
        timerGoing_L = false;
        //elapsedTime_L = 0f;
        // Send some restart line message to puredata
    }

    private IEnumerator UpdateTimer_L()
    {
        while(timerGoing_L)
        {
            elapsedTime_L += Time.deltaTime;
            timePlaying_L = TimeSpan.FromSeconds(elapsedTime_L);

            yield return null;   
        }
    }
    // ---------------------------



    // ------- RIGHT HAND --------
    // ---------------------------
    // ---------------------------    
    public void beginTimer_R(){
        // Debug.Log("Pointing at Sphere 1");
        timerGoing_R = true;
        elapsedTime_R = 0f;

        StartCoroutine(UpdateTimer_R());
    }

    public void endTimer_R(){
        // Debug.Log("Stopped pointing at Sphere 1");
        timerGoing_R = false;
        //elapsedTime_R = 0f;
        // Send some restart line message to puredata
    }

    private IEnumerator UpdateTimer_R()
    {
        while(timerGoing_R)
        {
            elapsedTime_R += Time.deltaTime;
            timePlaying_R = TimeSpan.FromSeconds(elapsedTime_R);

            yield return null;   
        }
    }
    // ---------------------------   
}

