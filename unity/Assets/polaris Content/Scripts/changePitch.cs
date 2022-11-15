using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Freya;

public class changePitch : MonoBehaviour
{
    analogPinValues analogPinValues;
    float pitchValue;

    // Start is called before the first frame update
    void Start()
    {
        analogPinValues = GameObject.Find("Arduino Nano").GetComponent<analogPinValues>();
    }

    // Update is called once per frame
    void Update()
    {
        pitchValue = Mathfs.RemapClamped(60f, 130f, 0.2f, 0.3f, analogPinValues.heartRate);
        transform.GetComponent<AudioSource>().pitch = pitchValue;
    }
}
