using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideOnCup : MonoBehaviour {
    //private GameObject mainCamera;
    private Rigidbody rb;
    private GameObject player;
    private float drift;
    private AudioSource se01, se02;
    // Use this for initialization
    void Start () {
        //player = GameObject.Find("Player");
        player = (GameObject)Resources.Load("Player");
        //rb = GetComponent<Rigidbody>();
        SystemCtrl.formChange = 4;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        se01 = audioSources[0]; se02 = audioSources[1];
        StartCoroutine(SpeedUp());
        //se01.Play();
        //Invoke("BreakSE", 9.5f);
        //Invoke("BreakUp", 10f);
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //mainCamera.transform.parent = transform;
        //mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y,1f);
        //mainCamera.transform.position = new Vector3(0, 1f, 0);
    }
	
	// Update is called once per frame
	void Update () {
        //rb.AddForce(Vector3.right * 4000f * Time.deltaTime, ForceMode.Force);       
        if (SystemCtrl.canCtrl == true)
        {
            rb.AddForce(transform.forward * 3000f * Time.deltaTime, ForceMode.Force);
            KeyCtrl();
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
        PosReset();
        se01.volume = OptionCtrl.volume / 10;
        se02.volume = OptionCtrl.volume / 10;
    }

    private IEnumerator SpeedUp()
    {
        rb = GetComponent<Rigidbody>();
        while (SystemCtrl.canCtrl == false)
        {
            yield return null;
        }
        rb.AddForce(Vector3.right * 4000f * Time.deltaTime, ForceMode.Force);
        Invoke("BreakSE", 9.5f);
        Invoke("BreakUp", 10f);
    }

    void KeyCtrl()
    {
        transform.eulerAngles += new Vector3(0, Input.GetAxis("Horizontal2")*drift* (2f + OptionCtrl.sensitivity) * OptionCtrl.horizontalReverse, 0);
        if (Input.GetButtonDown("Jump") == true)
        {
            rb.velocity = Vector3.zero;
            Instantiate(player, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if(Input.GetButton("Cancel")== true)
        {
            rb.AddForce(-transform.forward * 100f * Time.deltaTime, ForceMode.Force);
            if(drift < 3f)
            {
                drift += 0.2f;
            }
        }
        else
        {
            drift = 1f;
        }
    }

    void BreakUp()
    {
        rb.velocity = Vector3.zero;
        Instantiate(player, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    void BreakSE()
    {
        se02.PlayOneShot(se02.clip);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "CanClamb")
        {
            rb.AddForce(transform.up * 400f);
        }
    }

    void PosReset()
    {
        if (transform.position.y < -0.1f)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}
