using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class RideOnFireEX : MonoBehaviour {
    private GameObject player;
    private GameObject newfriends;
    private GameObject[] friends;
    [SerializeField] private CinemachineVirtualCamera vcamera1 ,vcamera2;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private GameObject fireSmoke;
    private AudioSource se01,se02;
    [SerializeField] private bool isAttack;
    // Use this for initialization
    void Start () {
        SystemCtrl.formChange = 6;
        player = (GameObject)Resources.Load("Player");
        newfriends = (GameObject)Resources.Load("NowFriend");
        friends = GameObject.FindGameObjectsWithTag("NowFriend");
        for (int i = 0; i < friends.Length; i++)
        {
            Destroy(friends[i]);
        }
        hpSlider.value = 10f;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        se01 = audioSources[0]; se02 = audioSources[1];
        isAttack = false;
        StartCoroutine(StartCamera(0.5f));
        se02.PlayOneShot(se02.clip);
    }
	
	// Update is called once per frame
	void Update () {
        if (SystemCtrl.canCtrl == true)
        {
            KeyCtrl();
        }
        BreakUp();
        PosReset();
        se02.volume = OptionCtrl.volume / 10;
    }

    private void KeyCtrl()
    {
        if (Input.GetButtonDown("Jump") == true || hpSlider.value <= 0)
        {
            Instantiate(player, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.rotation);
            for (int i = 0; i < GroupCtrl.fnum; i++)
            {
                Instantiate(newfriends, new Vector3(transform.position.x + i * 0.5f, 0.25f, transform.position.z + i * 0.5f), Quaternion.identity);
            }
            Destroy(gameObject);
        }
        transform.eulerAngles += new Vector3(0, Input.GetAxis("Horizontal2") * (2f + OptionCtrl.sensitivity) * OptionCtrl.horizontalReverse, 0);
        if (Input.GetButton("Fire3"))
        {
            fireSmoke.SetActive(true);
            se01.volume = OptionCtrl.volume / 10;
            isAttack = true;
        }
        else
        {
            fireSmoke.SetActive(false);
            se01.volume = 0;
            isAttack = false;
        }
    }

    private void BreakUp()
    {
        if (isAttack == true)
        {
            hpSlider.value -= 0.02f;
        }
    }

    private void PosReset()
    {
        if (transform.position.y < -0.2f)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }

    private IEnumerator StartCamera(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        vcamera2.Priority = 0;
        yield return null;
    }
}
