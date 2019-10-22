using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Test : MonoBehaviour {
    public CinemachineVirtualCamera vcamera;
    private CinemachineComposer cp;
    // Use this for initialization
    void Start () {
        cp = vcamera.GetCinemachineComponent<CinemachineComposer>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * 20f * Time.deltaTime * Input.GetAxis("Vertical"));
        transform.Translate(Vector3.left * 20f * Time.deltaTime * Input.GetAxis("Horizontal"));
        transform.eulerAngles += new Vector3(0, Input.GetAxis("Horizontal2"), 0);
        if(Input.GetAxis("Vertical2") < 0)
        {
            cp.m_ScreenY += 0.01f;
        }
    }
}
