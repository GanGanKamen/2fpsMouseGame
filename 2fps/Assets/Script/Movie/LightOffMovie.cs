using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LightOffMovie : MonoBehaviour {
    public CinemachineVirtualCamera vcamera1, vcamera2, vcamera3;
    public Animator switchboardAnm;
    public Animator playerAnm;
    // Use this for initialization
    void Start () {
        StartCoroutine(MovieStart());
	}
	
	// Update is called once per frame
	void Update () {
        AnimatorStateInfo switchboardAnmInfo = switchboardAnm.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo playerAnmInfo = playerAnm.GetCurrentAnimatorStateInfo(0);
        if(switchboardAnmInfo.IsName("Base Layer.SwitchBoard02"))
        {
            vcamera2.Priority = 100;
        }
        if(playerAnmInfo.IsName("Base Layer.Player_SwitchBoard02"))
        {
            vcamera3.Priority = 1000;
        }
	}
    private IEnumerator MovieStart()
    {
        vcamera1.Priority = 10;
        while(vcamera2.Priority != 100)
        {
            yield return null;
        }
        while(vcamera3.Priority != 1000)
        {
            yield return null;
        }
        Debug.Log("Complete");
    }
}
