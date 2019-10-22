using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Team : MonoBehaviour {
    public Image fadeCanvas;
    private float colorA;
    private float nowTime;
	// Use this for initialization
	void Start () {
        colorA = 0;
    }
	
	// Update is called once per frame

	void Update () {
        nowTime += Time.deltaTime;
        if(nowTime > 0 && nowTime <= 3f)
        {
            Fadein();
        }
        else if(nowTime >3f && nowTime <= 6f)
        {
            Fadeout();
        }
        else if(nowTime >= 7f)
        {
            SceneManager.LoadScene("Opening");
        }
	}

    void Fadein()
    {
        fadeCanvas.color = new Color(255f, 255f, 255f, colorA);
        colorA += 0.01f;
    }
    void Fadeout()
    {
        fadeCanvas.color = new Color(255f, 255f, 255f, colorA);
        colorA -= 0.01f;
    }
}
