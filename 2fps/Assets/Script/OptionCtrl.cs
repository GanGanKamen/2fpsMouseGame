using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionCtrl : MonoBehaviour {
    public RectTransform window;
    static public bool movieSkip; public Text movieText;
    static public int verticalReverse = 1; public Text verText;
    static public int horizontalReverse = 1; public Text horiText;
    static public float volume; public Slider volSlider; public Text volText;
    static public float brightness; public Slider briSlider; public Text briText;
    static public float sensitivity; public Slider senSlider; public Text senText;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        window.localScale = Vector3.zero;
        movieSkip = false; movieText.text = "OFF";
        verText.text = "OFF"; horiText.text = "OFF";
	}
	
	// Update is called once per frame
	void Update () {
        Vertical();
        Horizontal();
        Volume();
        Brightness();
        Sensitivity();
    }

    public void WindowOpen()
    {
        window.localScale = new Vector3(1, 1, 1);
    }

    public void WindowClose()
    {
        window.localScale = Vector3.zero;
        SystemCtrl.canCtrl = true;
    }

    public void MovieSkip()
    {
        if (movieSkip == false)
        {
            movieSkip = true;
            movieText.text = "ON";
        }
        else
        {
            movieSkip = false;
            movieText.text = "OFF";
        }
    }

    public void VerticalReverse()
    {
        verticalReverse = verticalReverse * -1;
    }

    private void Vertical()
    {
        if (verticalReverse > 0)
        {
            verText.text = "OFF";
        }
        else
        {
            verText.text = "ON";
        }
    }

    public void HorizontalReverse()
    {
        horizontalReverse = horizontalReverse * -1;
    }

    private void Horizontal()
    {
        if(horizontalReverse > 0)
        {
            horiText.text = "OFF";
        }
        else
        {
            horiText.text = "ON";
        }
    }

    private void Volume()
    {
        volume = volSlider.value;
        volText.text = volume.ToString();
    }

    private void Brightness()
    {
        brightness = briSlider.value;
        briText.text = brightness.ToString();
        RenderSettings.ambientIntensity = 1 + (brightness - 5) * 0.1f;
    }

    private void Sensitivity()
    {
        sensitivity = senSlider.value;
        if(sensitivity < 0)
        {
            senText.text = sensitivity.ToString();
        }
        else
        {
            senText.text = "+" + sensitivity.ToString();
        }
    }

    public void BackToTitle()
    {
        GroupCtrl.fnum = 0; GroupCtrl.nowStage = 0; GroupCtrl.bigfnum = 0; GroupCtrl.nowStageFnum = 0;
        window.localScale = Vector3.zero;
        SceneManager.LoadScene("Title");
    }
}
