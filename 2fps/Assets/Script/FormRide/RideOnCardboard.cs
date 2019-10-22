using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RideOnCardboard : MonoBehaviour
{
    //private GameObject mainCamera;
    private GameObject player;
    private GameObject newfriends;
    private float speed = 18f;
    private GameObject[] friends;
    [SerializeField]private bool isAttack;
    [SerializeField] private Slider hpSlider;
    private Rigidbody rb;
    [SerializeField] private bool onGround;
    public Animator anmCardboard;
    public GameObject wave;
    private AudioSource se;
    //private bool isStealth;
    // Use this for initialization
    void Start()
    {
        SystemCtrl.formChange = 1;
        rb = GetComponent<Rigidbody>();
        player = (GameObject)Resources.Load("Player");
        newfriends = (GameObject)Resources.Load("NowFriend");
        //mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //mainCamera.transform.parent = transform;
        //mainCamera.transform.position = new Vector3(transform.position.x, 3.5f, transform.position.z);
        friends = GameObject.FindGameObjectsWithTag("NowFriend");
        for (int i = 0; i < friends.Length; i++)
        {
            Destroy(friends[i]);
        }
        hpSlider.value = 10f;
        se = GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        if (SystemCtrl.canCtrl == true)
        {
            KeyCtrl();
        }
        BreakUp();
        //Attack();
        PosReset();
        se.volume = OptionCtrl.volume / 10;
    }
    void KeyCtrl()
    {
        if (Input.GetButtonDown("Jump") == true||hpSlider.value <= 0)
        {
            Instantiate(player, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.rotation);           
            for (int i = 0; i < GroupCtrl.fnum; i++)
            {
                Instantiate(newfriends, new Vector3(transform.position.x + i * 0.5f, 0.25f, transform.position.z + i * 0.5f), Quaternion.identity);
            }
            Destroy(gameObject);
        }
        if(SystemCtrl.isStealth == false)
        {
            if (Input.GetButtonDown("Fire3") == true && onGround == true)
            {
                StartCoroutine(Tackle());
                //isAttack = true;
                //rb.AddForce(transform.up * 10f + transform.forward * 3f,ForceMode.Impulse);
            }
            if (Input.GetButtonDown("Cancel") == true && isAttack == false && SystemCtrl.isWarning == false)
            {
                SystemCtrl.isStealth = true;
                anmCardboard.SetBool("Stealth", true);
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime * Input.GetAxis("Vertical"));
            transform.Translate(Vector3.left * speed * Time.deltaTime * Input.GetAxis("Horizontal"));
            transform.eulerAngles += new Vector3(0, Input.GetAxis("Horizontal2")* (2f + OptionCtrl.sensitivity) * OptionCtrl.horizontalReverse, 0);
        }
        else if(SystemCtrl.isStealth == true)
        {   
            if (Input.GetButtonDown("Cancel") == true)
            {
                ReStealth();
            }
        }        
    }

    private void BreakUp()
    {
        if(isAttack == true)
        {
            hpSlider.value -= 0.05f;
        }
    }

    private IEnumerator Tackle()
    {
        isAttack = true;
        rb.AddForce(transform.up * 10f + transform.forward * 3f, ForceMode.Impulse);
        wave.SetActive(true);
        se.PlayOneShot(se.clip);
        yield return null;
        while (onGround == false)
        {
            yield return null;
        }
        wave.SetActive(false);
        isAttack = false;
        yield return null;
    }

    /*void Attack()
    {
        if (isAttack == true)
        {
            wave.SetActive(true);
            attackTime++;
            if (attackTime >= 40)
            {
                isAttack = false;
                attackTime = 0;
                isDown = true;
            }
        }
        else
        {
            wave.SetActive(false);
        }
        if(isDown == true)
        {
            attackTime++;
            if (attackTime >= 50)
            {
                attackTime = 0;
                isDown = false;
            }
        }
    }*/

    void ReStealth()
    {
        SystemCtrl.isStealth = false;
        anmCardboard.SetBool("Stealth", false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ScientistP")
        {
            if(isAttack == true)
            {
                Destroy(collision.gameObject);
                GameObject exprosion = (GameObject)Resources.Load("Effect/Explosion");
                Instantiate(exprosion, collision.gameObject.transform.position, Quaternion.identity);
                GameObject clothes = (GameObject)Resources.Load("Clothes");
                Instantiate(clothes, collision.gameObject.transform.position, Quaternion.identity);
            }
            if(SystemCtrl.isStealth == true)
            {
                ReStealth();
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

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }

    void PosReset()
    {
        if (transform.position.y < -0.2f)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }
    }
}
