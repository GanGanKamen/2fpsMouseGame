using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RideOnTrash : MonoBehaviour {
    private GameObject player;
    private GameObject newfriends;
    private GameObject[] friends;
    private Rigidbody rb;
    private float speed;
    public GameObject flame1,flame2;
    private bool isAttack;
    private Animator trashAnm;
    [SerializeField] private AudioSource se;
    [SerializeField] private Slider hpSlider;
    // Use this for initialization
    void Start () {
        SystemCtrl.formChange = 3;
        rb = GetComponent<Rigidbody>();
        trashAnm = GetComponent<Animator>();
        player = (GameObject)Resources.Load("Player");
        newfriends = (GameObject)Resources.Load("NowFriend");
        friends = GameObject.FindGameObjectsWithTag("NowFriend");
        for (int i = 0; i < friends.Length; i++)
        {
            Destroy(friends[i]);
        }
        speed = 0;
        isAttack = false;
        se = GetComponent<AudioSource>();
        hpSlider.value = 10;
    }
	
	// Update is called once per frame
	void Update () {
        if (SystemCtrl.canCtrl == true)
        {
            KeyCtrl();
            FlareDrive();
        }
        AnmCtrl();
        PosReset();
        Volume();
        BreakUp();
    }

   void Volume()
    {
        if(speed >= 25)
        {
            se.volume = OptionCtrl.volume / 10;
        }
        else
        {
            se.volume = 0;
        }
    }

    void KeyCtrl()
    {
        rb.velocity = transform.right * speed;
        if (Input.GetAxis("Vertical") > 0)
        {
            if(speed <= 30f)
            {
                speed += 0.4f;
            }
        }
        else if(Input.GetAxis("Vertical") < 0)
        {
            if (speed >= -15f&&speed<=0)
            {
                speed -= 0.2f;
            }
            else if(speed > 0)
            {
                speed -= 1f;
            }
        }
        else
        {
            if(speed > 0)
            {
                speed -= 1f;
            }
            else if(speed < 0)
            {
                speed += 1f;
            }
        }
        transform.eulerAngles += new Vector3(0, Input.GetAxis("Horizontal2") * (2f + OptionCtrl.sensitivity) * OptionCtrl.horizontalReverse, 0);
        if (Input.GetButtonDown("Jump") == true||hpSlider.value <= 0)
        {
            Instantiate(player, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.rotation);
            Destroy(gameObject);
            for (int i = 0; i < GroupCtrl.fnum; i++)
            {
                Instantiate(newfriends, new Vector3(transform.position.x + i * 0.5f, 0.25f, transform.position.z + i * 0.5f), Quaternion.identity);
            }
        }
    }

    void BreakUp()
    {
        if(speed >= 20)
        {
            hpSlider.value -= 0.02f;
        }
    }

    void FlareDrive()
    {
        if(speed >= 20f)
        {
            flame1.SetActive(true);
        }
        else
        {
            flame1.SetActive(false);
        }
        if(speed >= 25f)
        {
            flame2.SetActive(true);
            isAttack = true;
        }
        else
        {
            flame2.SetActive(false);
            isAttack = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ScientistP")
        {
            if(isAttack == true)
            {
                Destroy(collision.gameObject);
                GameObject clothes = (GameObject)Resources.Load("Clothes");
                Instantiate(clothes, collision.gameObject.transform.position, Quaternion.identity);
                GameObject exprosion = (GameObject)Resources.Load("Effect/Explosion");
                Instantiate(exprosion, collision.gameObject.transform.position, Quaternion.identity);
            }
        }
        if (collision.gameObject.tag == "Robot")
        {
            if (isAttack == true)
            {
                Animator animator = collision.gameObject.GetComponent<Animator>();
                animator.SetBool("Down", true);
            }
        }
    }

    private void AnmCtrl()
    {
        trashAnm.SetFloat("Speed", speed / 10f);
    }

    void PosReset()
    {
        if (transform.position.y < -0.1f)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}
