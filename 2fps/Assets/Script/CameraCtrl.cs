using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraCtrl : MonoBehaviour {
    private CinemachineVirtualCamera vcamera;
    private CinemachineComposer cp;
    //private GameObject mainlookat;
    //private GameObject sublookat;
    // Use this for initialization
    void Start () {
        vcamera = GetComponent<CinemachineVirtualCamera>();
        cp = vcamera.GetCinemachineComponent<CinemachineComposer>();
    }
	
	// Update is called once per frame
	void Update () {      
        if (SystemCtrl.canCtrl == true)
        {
            KeyCtrl();            
        }
    }

    void KeyCtrl()
    {
        switch (SystemCtrl.formChange)
        {
            case 0:
                if (Input.GetAxis("Vertical2") * OptionCtrl.verticalReverse < 0 && cp.m_ScreenY < 0.9f)
                {
                    cp.m_ScreenY += 0.02f + OptionCtrl.sensitivity/1000f;
                }
                else if (Input.GetAxis("Vertical2") * OptionCtrl.verticalReverse > 0 && cp.m_ScreenY > 0.4f)
                {
                    cp.m_ScreenY -= 0.02f + OptionCtrl.sensitivity / 1000f;
                }
                if (Input.GetButtonDown("Submit") == true)
                {
                    cp.m_ScreenY = 0.5f; cp.m_ScreenX = 0.5f;
                }
                break;
            case 1:
                if (Input.GetAxis("Vertical2") * OptionCtrl.verticalReverse < 0 && cp.m_ScreenY < 0.9f)
                {
                    cp.m_ScreenY += 0.02f + OptionCtrl.sensitivity / 1000f;
                }
                else if (Input.GetAxis("Vertical2") * OptionCtrl.verticalReverse > 0 && cp.m_ScreenY > 0.3f)
                {
                    cp.m_ScreenY -= 0.02f + OptionCtrl.sensitivity / 1000f;
                }
                if (Input.GetButtonDown("Submit") == true)
                {
                    cp.m_ScreenY = 0.5f;
                }
                break;
            case 2:
                break;
            case 3:
                if (Input.GetAxis("Vertical2") * OptionCtrl.verticalReverse < 0 && cp.m_ScreenY < 0.6f)
                {
                    cp.m_ScreenY += 0.01f + OptionCtrl.sensitivity / 1000f;
                }
                else if (Input.GetAxis("Vertical2") * OptionCtrl.verticalReverse > 0 && cp.m_ScreenY > 0.4f)
                {
                    cp.m_ScreenY -= 0.01f + OptionCtrl.sensitivity / 1000f;
                }
                if (Input.GetButtonDown("Submit") == true)
                {
                    cp.m_ScreenY = 0.5f; cp.m_ScreenX = 0.5f;
                }
                break;
            case 4:
                break;
            case 5:
                if (Input.GetAxis("Vertical2") * OptionCtrl.verticalReverse < 0 && cp.m_ScreenY < 0.7f)
                {
                    cp.m_ScreenY += 0.01f + OptionCtrl.sensitivity / 1000f;
                }
                else if (Input.GetAxis("Vertical2") * OptionCtrl.verticalReverse > 0 && cp.m_ScreenY > 0.4f)
                {
                    cp.m_ScreenY -= 0.01f + OptionCtrl.sensitivity / 1000f;
                }
                if (Input.GetButtonDown("Submit") == true)
                {
                    cp.m_ScreenY = 0.5f; cp.m_ScreenX = 0.5f;
                }
                break;
            case 6:
                if (Input.GetAxis("Vertical2") * OptionCtrl.verticalReverse < 0 && cp.m_ScreenY < 0.8f)
                {
                    cp.m_ScreenY += 0.01f + OptionCtrl.sensitivity / 1000f;
                }
                else if (Input.GetAxis("Vertical2") * OptionCtrl.verticalReverse > 0 && cp.m_ScreenY > 0.4f)
                {
                    cp.m_ScreenY -= 0.01f + OptionCtrl.sensitivity / 1000f;
                }
                if (Input.GetButtonDown("Submit") == true)
                {
                    cp.m_ScreenY = 0.5f; cp.m_ScreenX = 0.5f;
                }
                break;

        }
    }
}
