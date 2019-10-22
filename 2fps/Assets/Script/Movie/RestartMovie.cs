using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RestartMovie : MonoBehaviour {
    public CinemachineVirtualCamera vcamera1, vcamera2, vcamera3,vcamera4;
    public Animator scientistAnm;
    public Animator playerAnm;
    private AudioSource se01, se02;
    // Use this for initialization
    void Start () {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        se01 = audioSources[0]; se02 = audioSources[1];
        StartCoroutine(MovieStart());
	}
	
	// Update is called once per frame
	void Update () {
        AnimatorStateInfo scientistAnmInfo = scientistAnm.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo playerAnmInfo = playerAnm.GetCurrentAnimatorStateInfo(0);
        if(scientistAnmInfo.IsName("Base Layer.Scientist Movie001"))
        {
            vcamera2.Priority = 100;
        }
        if(scientistAnmInfo.IsName("Base Layer.Scientist Movie003"))
        {
            vcamera3.Priority = 200;
        }
        if (scientistAnmInfo.IsName("Base Layer.Scientist Movie004"))
        {
            vcamera4.Priority = 300;
        }
    }

    private IEnumerator MovieStart()
    {
        vcamera1.Priority = 10;
        while (vcamera2.Priority != 100)
        {
            yield return null;
        }
        while(vcamera3.Priority != 200)
        {
            yield return null;
        }
        se01.PlayOneShot(se01.clip);
        while (vcamera4.Priority != 300)
        {
            yield return null;
        }
        se02.PlayOneShot(se02.clip);
        Debug.Log("Complete");
    }
}
