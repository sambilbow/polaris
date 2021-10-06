using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Unity.Attributes;

public class showMenu : MonoBehaviour
{
    public HandModelBase handModel;
    public GameObject menu;

    // Update is called once per frame
    void Update()
    {
        if(handModel != null && handModel.IsTracked){
            menu.SetActive(true);
        }
        else{
            menu.SetActive(false);
        }
    }
}
