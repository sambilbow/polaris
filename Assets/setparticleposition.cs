using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setparticleposition : MonoBehaviour
{
    public GameObject particle_l;
    public GameObject particle_r;
    public GameObject index_l;
    public GameObject index_r;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       particle_l.transform.position =  index_l.transform.position;
       particle_r.transform.position =  index_r.transform.position;
    }
}
