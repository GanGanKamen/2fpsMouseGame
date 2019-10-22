using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SystemCtrl : MonoBehaviour {
    static public int life;
    static public bool isRestart = false;
    private int prelife;
    private GameObject nowPlayer;
    private GameObject newfriends;
    //public Text lifetext;
    public Text friendtext;
    public Text targettext;
    public GameObject warningMark;
    private GameObject[] scientists;
    private int nowScientists;
    static public bool isWarning = false;
    static public bool canCtrl = false;
    public GameObject load;
    static public int movieSwitch = 0;
    static public int bgmSwitch = 0;
    static public int formChange;
    static public bool isLightOff;
    static public bool isStealth;
    public Light direLight;
    private AudioSource bgm01, bgm02;
    public GameObject memo;
    public GameObject miniMap;
    public GameObject canvas;
    static public bool isLoading;
    static public bool isMovieLoad;
    private int targetNum;
    public Material light, nolight;
    private GameObject[] lightobj;
    public GameObject movieCanvas;
    [SerializeField] private GameObject movie1;
    [SerializeField] private GameObject movie2;
    [SerializeField] private GameObject movie3;
    [SerializeField] private GameObject movie4;
    [SerializeField] private GameObject movie5;
    [SerializeField] private GameObject movie6;
    [SerializeField] private GameObject movie7;
    [SerializeField] private GameObject movie8;
    [SerializeField] private GameObject movie9;
    [SerializeField] private GameObject movie10;
    [SerializeField] private RectTransform opWindow;
    [SerializeField] private RectTransform manuWindow; 
    // Use this for initialization
    void Start () {
        canCtrl = false;
        life = 3;
        prelife = 3;
        nowScientists = 0;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        bgm01 = audioSources[0]; bgm02 = audioSources[1];
        isLightOff = false;
        isStealth = false;
        GroupCtrl.nowStageFnum = 0;
        nowPlayer = GameObject.FindGameObjectWithTag("Player");
        newfriends = (GameObject)Resources.Load("NowFriend");
        Invoke("LoadFriends", 0.5f);
        isLoading = true;
        Invoke("LoadOver", 3f);
        lightobj = GameObject.FindGameObjectsWithTag("LightObj");
        for(int i = 0; i < lightobj.Length; i++)
        {
            lightobj[i].GetComponent<Renderer>().material = light;
        }
        movie1 = (GameObject)Resources.Load("Movies/Movie01");
        movie2 = (GameObject)Resources.Load("Movies/Movie02");
        movie3 = (GameObject)Resources.Load("Movies/Movie03");
        movie4 = (GameObject)Resources.Load("Movies/Movie04");
        movie5 = (GameObject)Resources.Load("Movies/Movie05");
        movie6 = (GameObject)Resources.Load("Movies/Movie06");
        movie7 = (GameObject)Resources.Load("Movies/Movie07");
        movie8 = (GameObject)Resources.Load("Movies/Movie08");
        movie9 = (GameObject)Resources.Load("Movies/Movie09");
        movie10 = (GameObject)Resources.Load("Movies/Movie10");
    }
	
	// Update is called once per frame
	void Update () {
        nowPlayer = GameObject.FindGameObjectWithTag("Player");
        ReStart();
        //lifetext.text = "×" + life.ToString();
        friendtext.text = "×" + GroupCtrl.fnum.ToString();
        targettext.text = "×" + (targetNum - GroupCtrl.nowStageFnum).ToString();
        scientists = GameObject.FindGameObjectsWithTag("Scientist");
        //Debug.Log(scientists.Length);
        if(isRestart == false)
        {
            WarningCheck();
        }        
        SwitchBGM();
        SwitchMovie();
        LightOff();
        TargetNumCount();
        if (Input.GetButton("PS") == true)
        {
            memo.SetActive(true);
        }
        else
        {
            memo.SetActive(false);
        }
        bgm01.volume = OptionCtrl.volume / 10; bgm02.volume = OptionCtrl.volume / 10;
    }

    public void OptionOpen()
    {
        if (canCtrl == true)
        {
            canCtrl = false;
            opWindow = GameObject.Find("OptionCanvas/Window").GetComponent<RectTransform>();
            opWindow.localScale = new Vector3(1, 1, 1);
        }
    }

    public void ManualOpen()
    {
        if(canCtrl == true)
        {
            canCtrl = false;
            manuWindow = GameObject.Find("MovieCanvas/Manual").GetComponent<RectTransform>();
            manuWindow.localScale = new Vector3(1, 1, 1);
        }
    }

    public void ManualClose()
    {
        manuWindow = GameObject.Find("MovieCanvas/Manual").GetComponent<RectTransform>();
        manuWindow.localScale = Vector3.zero;
        canCtrl = true;
    }

    void LoadOver()
    {
        canCtrl = true;
        isLoading = false;
        load.SetActive(false);
        bgmSwitch = 1;
        if(GroupCtrl.nowStage == 1&&OptionCtrl.movieSkip == false)
        {
            movieSwitch = 9;
        }
    }

    void ReStart()
    {
        if(prelife > life)
        {
            prelife = life;
            isRestart = true;
            bgmSwitch = 100;
            //Destroy(nowPlayer);
            if(OptionCtrl.movieSkip == false)
            {
                movieSwitch = 5;
                canCtrl = false;
            }
            else
            {
                Invoke("ReStart2", 0.02f);
            }
            GameObject newplayer = (GameObject)Resources.Load("Player");                   
            switch (GroupCtrl.nowStage)
            {
                case 0:
                    Instantiate(newplayer, new Vector3(-95f, 0, 0), Quaternion.Euler(0, 90f, 0));
                    break;
                case 1:
                    Instantiate(newplayer, new Vector3(-34f,6.8f,0), Quaternion.Euler(0, 90f, 0));
                    break;
                case 2:
                    Instantiate(newplayer, new Vector3(380f, 0, 330f), Quaternion.Euler(0, 90f, 0));
                    break;
                case 3:
                    Instantiate(newplayer, new Vector3(-25f, 0, 0), Quaternion.Euler(0, 90f, 0));
                    break;
            }
        }
    }

    private void ReStart2()
    {
        isRestart = false;
    }
    void WarningCheck()
    {
        if(scientists.Length > nowScientists)
        {
            warningMark.SetActive(true);
            isWarning = true;
            bgmSwitch = 2;
            nowScientists = scientists.Length;
        }
        else if(scientists.Length == 0 && nowScientists != 0)
        {
            warningMark.SetActive(false);
            isWarning = false;
            bgmSwitch = 1;
            nowScientists = 0;
        }
    }
    void LoadFriends()
    {
        for (int i = 0; i < GroupCtrl.fnum; i++)
        {
            Instantiate(newfriends, new Vector3(nowPlayer.transform.position.x + i, 0.1f, nowPlayer.transform.position.z + i), Quaternion.identity);
        }
    }

    void MiniMap()
    {
        miniMap.transform.position = new Vector3(nowPlayer.transform.position.x, 200f, nowPlayer.transform.position.z);
    }

    void SwitchBGM()
    {
        switch (bgmSwitch)
        {
            case 1:
                bgm01.Play(); 
                bgm02.Stop();
                bgmSwitch = 0;
                break;
            case 2:
                bgm02.Play(); 
                bgm01.Stop();
                bgmSwitch = 0;
                break;
            case 100:
                bgm01.Stop();bgm02.Stop();
                bgmSwitch = 0;
                break;

        }
    }

    void SwitchMovie()
    {
        switch (movieSwitch)
        {
            case 1:
                GameObject moviePlaying1 = Instantiate(movie1, Vector3.zero, Quaternion.identity, movieCanvas.transform);
                isMovieLoad = true;
                movieSwitch = 0;
                break;
            case 2:
                GameObject moviePlaying2 = Instantiate(movie2,Vector3.zero,Quaternion.identity,movieCanvas.transform);
                isMovieLoad = true;
                movieSwitch = 0;
                break;
            case 3:
                GameObject moviePlaying3 = Instantiate(movie3, Vector3.zero, Quaternion.identity, movieCanvas.transform);
                isMovieLoad = true;
                movieSwitch = 0;
                break;
            case 4:
                GameObject moviePlaying4 = Instantiate(movie4, Vector3.zero, Quaternion.identity, movieCanvas.transform);
                isMovieLoad = true;
                movieSwitch = 0;
                break;
            case 5:
                GameObject moviePlaying5 = Instantiate(movie5, Vector3.zero, Quaternion.identity, movieCanvas.transform);
                isMovieLoad = true;
                movieSwitch = 0;
                break;
            case 6:
                GameObject moviePlaying6 = Instantiate(movie6, Vector3.zero, Quaternion.identity, movieCanvas.transform);
                isMovieLoad = true;
                movieSwitch = 0;
                break;
            case 7:
                GameObject moviePlaying7 = Instantiate(movie7, Vector3.zero, Quaternion.identity, movieCanvas.transform);
                isMovieLoad = true;
                movieSwitch = 0;
                break;
            case 8:
                GameObject moviePlaying8 = Instantiate(movie8, Vector3.zero, Quaternion.identity, movieCanvas.transform);
                isMovieLoad = true;
                movieSwitch = 0;
                break;
            case 9:
                GameObject moviePlaying9 = Instantiate(movie9, Vector3.zero, Quaternion.identity, movieCanvas.transform);
                isMovieLoad = true;
                movieSwitch = 0;
                break;
            case 10:
                GameObject moviePlaying10 = Instantiate(movie10, Vector3.zero, Quaternion.identity, movieCanvas.transform);
                isMovieLoad = true;
                movieSwitch = 0;
                bgmSwitch = 100;
                break;
        }
        if(isMovieLoad == true)
        {
            load.SetActive(true);
        }
        else if(isMovieLoad == false && isLoading == false)
        {
            load.SetActive(false);
        }
    }

    void LightOff()
    {
        float random = Random.Range(18f, 25f);
        if(isLightOff == true)
        {
            direLight.intensity = 0;
            for (int i = 0; i < lightobj.Length; i++)
            {
                lightobj[i].GetComponent<Renderer>().material = nolight;
            }
            Invoke("LightOn", random);
        }        
    }

    void LightOn()
    {
        direLight.intensity = 1f;
        isLightOff = false;
        for (int i = 0; i < lightobj.Length; i++)
        {
            lightobj[i].GetComponent<Renderer>().material = light;
        }
    }

    void TargetNumCount()
    {
        switch (GroupCtrl.nowStage)
        {
            case 0:
                targetNum = 5;
                break;
            case 1:
                targetNum = 9;
                break;
            case 2:
                targetNum = 10;
                break;
            case 3:
                targetNum = 11;
                break;
        }
    }
}
