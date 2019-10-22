using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVRobot : MonoBehaviour
{
    public Material red;
    public GameObject robot;
    public GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            effect.SetActive(true);
        }
    }
}
