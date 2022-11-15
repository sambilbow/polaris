using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Need this for getting MRTK Slider Values
using Microsoft.MixedReality.Toolkit.UI;

public class automatonism_0_params : MonoBehaviour
{
    // Start by initialising variables for the patch and all sliders we will be using
    public LibPdInstance pdPatch;
    
    public GameObject inSeqTrigger_button;

    public GameObject inOscFreq_slider;
    public GameObject inPWMLFOFreq_slider;
    public GameObject inPWMLFODepth_slider;
    public GameObject inFilterCF_slider;
    public GameObject inFilterQ_slider;
    public GameObject inFilterFM_slider;
    public GameObject inCFLFOFreq_slider;
    public GameObject inCFLFODepth_slider;

    // Default the values for the sliders
    float inOscFreq_sliderValue = 0.0f;
    float inPWMLFOFreq_sliderValue = 0.0f;
    float inPWMLFODepth_sliderValue = 0.0f;
    float inFilterCF_sliderValue = 0.0f;
    float inFilterQ_sliderValue = 0.0f;
    float inFilterFM_sliderValue = 0.0f;
    float inCFLFOFreq_sliderValue = 0.0f;
    float inCFLFODepth_sliderValue = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //Send values to Pd patch scaled to MIDI values from sliders.
        inOscFreq_sliderValue = inOscFreq_slider.GetComponent<PinchSlider>().SliderValue;
        pdPatch.SendFloat("inOscFreq", Mathf.RoundToInt(inOscFreq_sliderValue*127f));

        inPWMLFOFreq_sliderValue = inPWMLFOFreq_slider.GetComponent<PinchSlider>().SliderValue;
        pdPatch.SendFloat("inPWMLFOFreq", Mathf.RoundToInt(inPWMLFOFreq_sliderValue*127f));

        inPWMLFODepth_sliderValue = inPWMLFODepth_slider.GetComponent<PinchSlider>().SliderValue;
        pdPatch.SendFloat("inPWMLFODepth", Mathf.RoundToInt(inPWMLFODepth_sliderValue*127f));

        inFilterCF_sliderValue = inFilterCF_slider.GetComponent<PinchSlider>().SliderValue;
        pdPatch.SendFloat("inFilterCF", Mathf.RoundToInt(inFilterCF_sliderValue*127f));

        inFilterQ_sliderValue = inFilterQ_slider.GetComponent<PinchSlider>().SliderValue;
        pdPatch.SendFloat("inFilterQ", Mathf.RoundToInt(inFilterQ_sliderValue*127f));

        inFilterFM_sliderValue = inFilterFM_slider.GetComponent<PinchSlider>().SliderValue;
        pdPatch.SendFloat("inFilterFM", Mathf.RoundToInt(inFilterFM_sliderValue*127f));

        inCFLFOFreq_sliderValue = inCFLFOFreq_slider.GetComponent<PinchSlider>().SliderValue;
        pdPatch.SendFloat("inCFLFOFreq", Mathf.RoundToInt(inCFLFOFreq_sliderValue*127f));

        inCFLFODepth_sliderValue = inCFLFODepth_slider.GetComponent<PinchSlider>().SliderValue;
        pdPatch.SendFloat("inCFLFODepth", Mathf.RoundToInt(inCFLFODepth_sliderValue*127f));
    }

    public void buttonPress()
    {

        pdPatch.SendBang("inSeqTrigger");

    }

}
