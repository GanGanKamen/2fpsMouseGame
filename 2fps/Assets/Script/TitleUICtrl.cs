using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class TitleUICtrl : MonoBehaviour {
    public GameObject load;
    [SerializeField] private RectTransform opWindow;
    [SerializeField] private AudioSource bgm;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        bgm.volume = OptionCtrl.volume / 10;
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("Opening");
        }
	}

    public void OnStart()
    {
        load.SetActive(true);
        GroupCtrl.nowStage = 1;
        SceneManager.LoadScene("Stage");
    }

    public void OnTutorial()
    {
        load.SetActive(true);
        GroupCtrl.nowStage = 0;
        SceneManager.LoadScene("Tutorial");
    }

    public void OnOption()
    {
        opWindow = GameObject.Find("OptionCanvas/Window").GetComponent<RectTransform>();
        opWindow.localScale = new Vector3(1, 1, 1);
    }

    public void OnCredit()
    {
        SceneManager.LoadScene("Credit");
    }
}
