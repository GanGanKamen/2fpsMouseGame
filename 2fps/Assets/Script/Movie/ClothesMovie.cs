using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ClothesMovie : MonoBehaviour {
    public CinemachineVirtualCamera vcamera1, vcamera2, vcamera3, vcamera4;
    public GameObject canvas, canvas1;
    public GameObject afterHenshin;
    [SerializeField] private float time = 0;
    private AudioSource se;
    // Use this for initialization
    void Start () {
        StartCoroutine(MovieStart());
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        Debug.Log(time);
	}

    private IEnumerator MovieStart()
    {
        vcamera1.Priority = 10;
        se = GetComponent<AudioSource>();
        while(time < 0.2f)
        {
            yield return null;
        }
        vcamera2.Priority = 11;
        while (time < 0.3f)
        {
            yield return null;
        }
        se.PlayOneShot(se.clip);
        while (time < 0.5f)
        {
            yield return null;
        }
        canvas.SetActive(true);
        while (time < 1.5f)
        {
            yield return null;
        }
        canvas.SetActive(false);
        vcamera3.Priority = 12;
        canvas1.SetActive(true);
        while (time < 3f)
        {
            yield return null;
        }
        canvas1.SetActive(false);
        afterHenshin.SetActive(true);
        vcamera4.Priority = 13;
    }
}
