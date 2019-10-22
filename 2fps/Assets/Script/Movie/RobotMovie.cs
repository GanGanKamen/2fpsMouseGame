using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RobotMovie : MonoBehaviour {
    [SerializeField] private float time = 0;
    public Animator robotAnm;
    public Animator playerAnm;
    private AnimatorStateInfo robotAnmInfo;
    public GameObject friends;
    public CinemachineVirtualCamera vcamera1, vcamera2;
    public Material red;
    public AudioSource se1,se2,se3;
    public GameObject greenRay, redRay;
    // Use this for initialization
    void Start () {
        StartCoroutine(MovieStart());
	}
	
	// Update is called once per frame
	void Update () {
        robotAnmInfo = robotAnm.GetCurrentAnimatorStateInfo(0);
        time += Time.deltaTime;
        friends.transform.Translate(-transform.forward * Time.deltaTime*2f);
        if(robotAnmInfo.IsName("Base Layer.Move"))
        {
            GameObject.Find("Robot Roller - Green").transform.Translate(transform.forward * Time.deltaTime * 2f);
        }
    }

    private IEnumerator MovieStart()
    {
        yield return null;
        while(robotAnmInfo.IsName("Base Layer.Died"))
        {
            yield return null;
        }
        friends.SetActive(false);
        se1.PlayOneShot(se1.clip);
        while (!robotAnmInfo.IsName("Base Layer.Idle"))
        {
            yield return null;
        }
        se2.PlayOneShot(se2.clip);
        playerAnm.SetTrigger("Jump");
        vcamera2.Priority = 20;
        GameObject.Find("Robot Roller - Green/Robot Roller").GetComponent<SkinnedMeshRenderer>().material = red;
        greenRay.SetActive(false);redRay.SetActive(true);
        while (!robotAnmInfo.IsName("Base Layer.Move"))
        {
            yield return null;
        }        
        se3.PlayOneShot(se3.clip);
    }
}
