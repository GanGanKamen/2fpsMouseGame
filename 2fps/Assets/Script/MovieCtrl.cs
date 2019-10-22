using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class MovieCtrl : MonoBehaviour {
    static public bool complete = false;
    private VideoPlayer videoPlayer;
    private RawImage rendering;
    // Use this for initialization

    void Start()
    {
        StartCoroutine(VideoPlayStart());
    }
    private IEnumerator VideoPlayStart()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.playOnAwake = false;
        videoPlayer.SetDirectAudioVolume(0, OptionCtrl.volume / 10);
        videoPlayer.Prepare();
        rendering = GetComponent<RawImage>();
        rendering.color = Color.black;
        SystemCtrl.canCtrl = false;
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }
        rendering.color = Color.white;
        SystemCtrl.isMovieLoad = false;
        videoPlayer.Play();
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
        videoPlayer.Stop();
        if(SystemCtrl.isRestart == true)
        {
            SystemCtrl.isRestart = false;
            SystemCtrl.bgmSwitch = 1;
        }        
        complete = true;
        while (complete == false)
        {
            yield return null;
        }
        if (gameObject.name.StartsWith("Movie10"))
        {
            GroupCtrl.nowStage = 0;
            SceneManager.LoadScene("Credit");
        }
        else
        {
            SystemCtrl.canCtrl = true;
        }
        
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {

	}
}
