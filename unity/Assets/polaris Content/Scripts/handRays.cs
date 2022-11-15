using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;

public class handRays : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PointerUtils.SetGazePointerBehavior(PointerBehavior.AlwaysOff);
        PointerUtils.SetHandRayPointerBehavior(PointerBehavior.AlwaysOff);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
