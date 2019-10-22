using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class OpCtrl : MonoBehaviour
{
    public GameObject op;
    public Image Image;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(VideoPlayStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            SceneManager.LoadScene("Title");
        }
    }

    private IEnumerator VideoPlayStart()
    {
        VideoPlayer videoPlayer = op.GetComponent<VideoPlayer>();
        RawImage rendering = op.GetComponent<RawImage>();
        rendering.color = Color.black;
        Image.color = Color.black;
        videoPlayer.SetDirectAudioVolume(0, OptionCtrl.volume / 10);
        videoPlayer.Prepare();
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
        Image.color = Color.white;
        videoPlayer.Stop();
        yield return null;
        Destroy(op);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Title");
    }
}
