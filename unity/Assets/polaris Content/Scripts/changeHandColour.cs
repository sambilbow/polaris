using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeHandColour : MonoBehaviour
{
    public GameObject handMeshL;
    public GameObject handMeshR;
    public GameObject handMenu;
    // public GameObject R_index_particle;
    // public GameObject R_ring_particle;
    // private ParticleSystem ps;
    // private ParticleSystem ps2;
    Color mainColor;
    Color outlineColor;
    
    // Start is called before the first frame update
    void Start()
    {
        changeColour();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeColour()
    {
        //Create some random numbers
        mainColor = new Color(Random.Range(0.0f,0.2f),Random.Range(0.0f,0.2f),Random.Range(0.0f,0.2f),1);
        outlineColor = new Color(mainColor.r+0.6f,mainColor.g+0.6f,mainColor.b+0.6f,mainColor.a);        
        
        //Get the Renderer component from the hand
        var handRendererL = handMeshL.GetComponent<Renderer>();
        var handRendererR = handMeshR.GetComponent<Renderer>();
        var menuRenderer = handMenu.GetComponent<Renderer>();
        
        //Call SetColor using the shader property name "_Color" and setting the color to the randomly generated colors
        handRendererL.material.SetColor("_MainColor", mainColor);
        handRendererL.material.SetColor("_OutlineColor", outlineColor);

        handRendererR.material.SetColor("_MainColor", mainColor);
        handRendererR.material.SetColor("_OutlineColor", outlineColor);

        menuRenderer.material.SetColor("_Color", new Color(mainColor.r+0.2f,mainColor.g+0.2f,mainColor.b+0.2f,mainColor.a));

        // ps = R_index_particle.GetComponent<ParticleSystem>();
        // var main = ps.main;
        // main.startColor =  new Color(mainColor.r+0.45f,mainColor.g+0.45f,mainColor.b+0.45f,mainColor.a);

        // ps2 = R_ring_particle.GetComponent<ParticleSystem>();
        // var main2 = ps2.main;
        // main2.startColor =  new Color(mainColor.r+0.45f,mainColor.g+0.45f,mainColor.b+0.45f,mainColor.a);
        
        Debug.Log("Changed the colors!");
    }
}
