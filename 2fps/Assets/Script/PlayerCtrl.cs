using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerCtrl : MonoBehaviour {
    private float speed;
    private bool onGround;
    private bool onDesk;
    private bool onOther;
    private Rigidbody rb;
    public CinemachineVirtualCamera camera;
    private CinemachineTransposer transposer;
    public CinemachineCollider cColider;
    private Transform playerP;
    private bool isClambing;
    private AudioSource se01, se02;
    public Animator anmPlayer;
    public GameObject clambmark;
    //static public bool canCtrl;
    //static public int fnum;
    // Use this for initialization
    void Start () {
        SystemCtrl.formChange = 0;
        onGround = false;
        onDesk = false;
        rb = GetComponent<Rigidbody>();
        transposer = camera.GetCinemachineComponent<CinemachineTransposer>();
        cColider.m_AvoidObstacles = true;
        playerP = GameObject.FindGameObjectWithTag("ChaseTarget").transform;
        isClambing = false;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        se01 = audioSources[0]; se02 = audioSources[1];
        //canCtrl = true;
        //camera = GameObject.FindGameObjectWithTag("MainCamera");
        //subcamera = GameObject.FindGameObjectWithTag("SubCamera");
    }

    // Update is called once per frame
    void Update () {
        if(SystemCtrl.canCtrl == true)
        {
            KeyCtrl();
            AnmCtrl();
        }
        if(onGround == true||onDesk == true)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                speed = 10f;
            }
            else
            {
                speed = 2f;
            }
        }
        if(onGround == false&& onDesk == false)
        {
            speed = 3f;
        }
        if (onGround == true && onDesk == true&&isClambing == false)
        {
            speed = -30f;
        }
        PosReset();
        se01.volume = OptionCtrl.volume / 10;
        se02.volume = OptionCtrl.volume / 10;
    }

    void AnmCtrl()
    {
        if(isClambing == false)
        {
            clambmark.SetActive(false);
            if (Input.GetAxis("Vertical") != 0)
            {
                anmPlayer.SetBool("IsMove", true);
                
            }
            else
            {
                anmPlayer.SetBool("IsMove", false);
            }
        }
        else
        {
            clambmark.SetActive(true);
            anmPlayer.SetBool("IsMove", false);
        }
    }

    void KeyCtrl()
    {
        if(isClambing == false)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime * Input.GetAxis("Vertical"));
            transform.Translate(Vector3.left * speed/2 * Time.deltaTime * Input.GetAxis("Horizontal"));
            transform.eulerAngles += new Vector3(0, Input.GetAxis("Horizontal2") *(3f+OptionCtrl.sensitivity)*OptionCtrl.horizontalReverse, 0);
            cColider.m_AvoidObstacles = true;
            if(transposer.m_FollowOffset.z < -3f)
            {
                transposer.m_FollowOffset.z += 0.1f;
            }
            if (Input.GetButtonDown("Cancel") == true && (onGround == true||onDesk == true))
            {
                rb.AddForce(transform.up * 400f); se01.PlayOneShot(se01.clip);
            }
        }
        else if(isClambing == true)
        {
            transform.Translate(transform.up * speed * Time.deltaTime); //* Input.GetAxis("Vertical"));
            cColider.m_AvoidObstacles = false; ;
            if (transposer.m_FollowOffset.z > -5f)
            {
                transposer.m_FollowOffset.z -= 0.1f;
            }
            if (Input.GetButtonDown("Cancel") == true)
            {
                rb.AddForce(-transform.forward * 4f,ForceMode.Impulse); se01.PlayOneShot(se01.clip);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CanClamb" && onDesk == false)
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "CanClamb" && onDesk == false)
        {
            isClambing = true;
            rb.useGravity = false;
            anmPlayer.SetBool("IsClamb", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CanClamb")
        {
            isClambing = false;
            rb.useGravity = true;
            anmPlayer.SetBool("IsClamb", false);
            rb.AddForce(transform.forward* 300f + transform.up * 400f);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
        
        if(collision.gameObject.tag == "Desk")
        {
            onDesk = true;
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Friend")
        {
            GroupCtrl.fnum += 1;
            GroupCtrl.nowStageFnum += 1;
            Destroy(collision.transform.parent.gameObject);
            GameObject starEffect = (GameObject)Resources.Load("Effect/Star_Burst");
            Instantiate(starEffect, collision.gameObject.transform.position + new Vector3(0, 0.2f, 0), collision.gameObject.transform.rotation);
            GameObject friendOut = (GameObject)Resources.Load("Effect/MorumotoOut");
            Instantiate(friendOut, collision.gameObject.transform.position, collision.gameObject.transform.rotation);            
            se02.PlayOneShot(se02.clip);
        }
        /*if (collision.gameObject.tag == "Clothes" )//&& GroupCtrl.fnum >= 10)
        {
            GameObject clothes_player 
             = (GameObject)Resources.Load("Clothes(Player)");
            Instantiate(clothes_player, new Vector3(collision.gameObject.transform.position.x,8f, collision.gameObject.transform.position.z), transform.rotation);
            //camera.transform.parent = null; 
            //transform.position = new Vector3(0, 200f, 0);
            //canCtrl = false;
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }*/

        if (collision.gameObject.tag == "BigFriends"&&GroupCtrl.bigfnum > 0)
        {
            GroupCtrl.fnum += GroupCtrl.bigfnum;
            se02.PlayOneShot(se02.clip);
            GroupCtrl.bigfnum = 0;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = false;
        }
        if(collision.gameObject.tag == "Desk")
        {
            onDesk = false;
        }
    }

    void PosReset()
    {
        if(transform.position.y < -0.2f)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}
