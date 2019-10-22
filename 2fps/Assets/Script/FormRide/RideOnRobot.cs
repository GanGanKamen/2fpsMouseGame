using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RideOnRobot : MonoBehaviour {
    private GameObject player;
    private GameObject newfriends;
    private float speed = 18f;
    private GameObject[] friends;
    private Rigidbody rb;
    public Animator robotAnm;
    private AnimatorStateInfo robotAnmInfo,robotAnmInfo1;
    [SerializeField] private bool isAttack;
    public Animator playerAnm;
    public Animator colorAnm;
    public Transform muzzle;
    public GameObject spin;
    [SerializeField] private Slider hpSlider;
    private AudioSource se01, se02;
    // Use this for initialization
    void Start () {
        SystemCtrl.formChange = 5;
        rb = GetComponent<Rigidbody>();
        player = (GameObject)Resources.Load("Player");
        newfriends = (GameObject)Resources.Load("NowFriend");
        friends = GameObject.FindGameObjectsWithTag("NowFriend");
        for (int i = 0; i < friends.Length; i++)
        {
            Destroy(friends[i]);
        }
        isAttack = false;
        spin.SetActive(false);
        hpSlider.value = 10;
        AudioSource[] audioSources = GetComponents<AudioSource>();
        se01 = audioSources[0]; se02 = audioSources[1];
    }
	
	// Update is called once per frame
	void Update () {
        robotAnmInfo = robotAnm.GetCurrentAnimatorStateInfo(0);
        robotAnmInfo1 = robotAnm.GetCurrentAnimatorStateInfo(1);       
        se01.volume = 0;
        se02.volume = OptionCtrl.volume / 10;       
        if (SystemCtrl.canCtrl == true)
        {
            KeyCtrl();
            BreakUp();
            hpSlider.value -= 0.01f;
        }
	}

    private void KeyCtrl()
    {
        if (Input.GetButtonDown("Jump") == true && isAttack == false)
        {
            Instantiate(player, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.rotation);           
            for (int i = 0; i < GroupCtrl.fnum; i++)
            {
                Instantiate(newfriends, new Vector3(transform.position.x + i * 0.5f, 0.25f, transform.position.z + i * 0.5f), Quaternion.identity);
            }
            Destroy(gameObject);
        }

        if(Input.GetButtonDown("Fire3") == true && isAttack == false)
        {
            StartCoroutine(BeamAttack());
        }

        if(Input.GetButtonDown("Cancel") == true && isAttack == false && SystemCtrl.formChange == 5)
        {
            isAttack = true; 
            robotAnm.SetBool("Spin Attack", true);
            speed = 0;
            spin.SetActive(true);
            SystemCtrl.formChange = 50;
        }
        else if (Input.GetButtonDown("Cancel") == true && isAttack == true && SystemCtrl.formChange == 50)
        {
            isAttack = false;
            robotAnm.SetBool("Spin Attack", false);
            speed = 18f;
            spin.SetActive(false);
            SystemCtrl.formChange = 5;
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime * Input.GetAxis("Vertical"));
        transform.Translate(Vector3.left * speed * Time.deltaTime * Input.GetAxis("Horizontal"));
        transform.eulerAngles += new Vector3(0, Input.GetAxis("Horizontal2") * (1f + OptionCtrl.sensitivity) * OptionCtrl.horizontalReverse, 0);
        AnmCtrl();
    }

    private void AnmCtrl()
    {
        robotAnm.SetFloat("Forward", Input.GetAxis("Vertical"));
        robotAnm.SetFloat("Strafe", -Input.GetAxis("Horizontal"));
    }

    private IEnumerator BeamAttack()
    {
        isAttack = true;
        robotAnm.SetBool("Right Aim", true);
        speed = 0;
        while (!robotAnmInfo1.IsName("Upper Body.Right Aim"))
        {
            yield return null;
        }
        robotAnm.SetTrigger("Right Blast Attack");
        playerAnm.SetTrigger("Jump");
        GameObject beam = (GameObject)Resources.Load("Beam");
        Instantiate(beam, muzzle.position, muzzle.rotation);
        se02.PlayOneShot(se02.clip);
        while (robotAnmInfo1.IsName("Upper Body.Right Blast Attack"))
        {
            yield return null;
        }        
        robotAnm.SetBool("Right Aim", false);
        while (!robotAnmInfo1.IsName("Upper Body.Empty"))
        {
            yield return null;
        }
        isAttack = false;
        speed = 18f;
        hpSlider.value -= 1.5f;
        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Scientist"|| collision.gameObject.tag == "ScientistP")
        {
           if(SystemCtrl.formChange == 50)
            {
                Destroy(collision.gameObject);
                GameObject exprosion = (GameObject)Resources.Load("Effect/Explosion");
                Instantiate(exprosion, collision.gameObject.transform.position, Quaternion.identity);
            }
        }
    }

    private void BreakUp()
    {
        if(SystemCtrl.formChange == 50)
        {
            hpSlider.value -= 0.05f;
        }

        if(hpSlider.value <= 5)
        {
            se01.volume = OptionCtrl.volume / 10; colorAnm.SetBool("Flash", true);
        }
        if(hpSlider.value <= 0)
        {
            StartCoroutine(Down());
        }

    }

    private IEnumerator Down()
    {        
        isAttack = true;
        speed = 0;
        yield return null;        
        Instantiate(player, new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.rotation);
        for (int i = 0; i < GroupCtrl.fnum; i++)
        {
            Instantiate(newfriends, new Vector3(transform.position.x + i * 0.5f, 0.25f, transform.position.z + i * 0.5f), Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
