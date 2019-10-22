using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class MedicineMovie : MonoBehaviour {
    public CinemachineVirtualCamera vcamera1, vcamera2,vcamera3;
    public Animator mediAnm;
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
        AnimatorStateInfo mediAnmInfo = mediAnm.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo playerAnmInfo = playerAnm.GetCurrentAnimatorStateInfo(0);
        if(playerAnmInfo.IsName("Base Layer.Player_Medicine02"))
        {
            vcamera2.Priority = 100;
        }
        if(mediAnmInfo.IsName("Base Layer.Medicine02"))
        {
            vcamera3.Priority = 200;
        }
    }

    private IEnumerator MovieStart()
    {
        vcamera1.Priority = 10;
        while (vcamera2.Priority != 100)
        {
            yield return null;
        }
        se01.PlayOneShot(se01.clip);
        while (vcamera3.Priority != 200)
        {
            yield return null;
        }
        se02.PlayOneShot(se02.clip);
        Debug.Log("Complete");
    }
}
