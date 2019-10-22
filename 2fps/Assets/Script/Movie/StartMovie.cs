using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class StartMovie : MonoBehaviour {
    public CinemachineVirtualCamera vcamera1, vcamera2,vcamera3, vcamera4;
    public Animator playerAnm;
    [SerializeField] private float time = 0;
    public AudioSource se;
    // Use this for initialization
    void Start () {
		StartCoroutine(MovieStart());
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
    }

    private IEnumerator MovieStart()
    {
        yield return new WaitForSeconds(0.5f);
        vcamera2.Priority = 20;
        while(time <= 2f)
        {
            yield return null;
        }
        vcamera3.Priority = 30;
        while (time <= 4f)
        {
            yield return null;
        }
        vcamera4.Priority = 40;
        while (time <= 4.5f)
        {
            yield return null;
        }
        playerAnm.SetBool("Action", true);
        while (time <= 5f)
        {
            yield return null;
        }
        se.PlayOneShot(se.clip);
        yield return null;
    }
}
