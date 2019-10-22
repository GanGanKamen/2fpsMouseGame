using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour {
    private GameObject[] lookat;
    public Camera miniCamera;
    [SerializeField] private int cameraZoom = 60;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        lookat = GameObject.FindGameObjectsWithTag("Player");
        if(lookat.Length == 1)
        {
            for(int i = 0;i < 1; i++)
            {
                transform.position = new Vector3(lookat[0].transform.position.x, 200f, lookat[0].transform.position.z);
            }
        }
        Zoom();

    }

    private void Zoom()
    {
        if(SystemCtrl.canCtrl == true)
        {
            if(Input.GetAxis("Zoom") > 0 && cameraZoom >= 40)
            {
                cameraZoom -= 2;
            }
            if(Input.GetAxis("Zoom")<0 && cameraZoom <= 80)
            {
                cameraZoom += 2;
            }
        }
        miniCamera.orthographicSize = cameraZoom;
    }
}
