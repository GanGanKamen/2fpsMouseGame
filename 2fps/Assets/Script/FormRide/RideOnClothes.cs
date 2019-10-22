using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RideOnClothes : MonoBehaviour {
    //private GameObject mainCamera;
    private GameObject player;
    private float speed = 10f;
    private GameObject[] friends;
    private GameObject newfriends;
    private GameObject rocket;
    private Rigidbody rb;
    private bool isAttack;
    public Transform muzzle;
    public Animator anm;
    private AnimatorStateInfo anmInfo;
    public GameObject lineEffect;
    public Slider hpSlider;
    [SerializeField] private AudioSource se;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        SystemCtrl.formChange = 2;
        isAttack = false;
        player = (GameObject)Resources.Load("Player");
        rocket = (GameObject)Resources.Load("RocketPunch");
        newfriends = (GameObject)Resources.Load("NowFriend");
        friends = GameObject.FindGameObjectsWithTag("NowFriend");
        for (int i=0; i <friends.Length; i++)
        {
            Destroy(friends[i]);
        }
        lineEffect.SetActive(false);
        hpSlider.value = 10;
    }
	
	// Update is called once per frame
	void Update () {
        anmInfo = anm.GetCurrentAnimatorStateInfo(0);
        if (SystemCtrl.canCtrl == true && isAttack == false)
        {
            KeyCtrl();
        }
        se.volume = OptionCtrl.volume / 10;
    }

    void KeyCtrl()
    {
        if (Input.GetButtonDown("Jump") == true || hpSlider.value <= 0)
        {            
            Instantiate(player, new Vector3(transform.position.x, transform.position.y+1f, transform.position.z), Quaternion.Euler(0, 90f, 0));            
            for (int i = 0; i < GroupCtrl.fnum; i++)
            {
                Instantiate(newfriends, new Vector3(transform.position.x + i * 0.5f, 0.25f, transform.position.z + i * 0.5f), Quaternion.identity);
            }
            Destroy(gameObject);
        }
        if(Input.GetButtonDown("Fire3") == true)
        {
            StartCoroutine(RoketPunch());
        }
        if(Input.GetButtonDown("Cancel")==true)
        {
            //isAttack = true;
            //rb.AddForce(-transform.forward * 10f + transform.up*5f, ForceMode.Impulse);
            StartCoroutine(Avoidance());
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime * Input.GetAxis("Vertical"));
        transform.Translate(Vector3.left * speed * Time.deltaTime * Input.GetAxis("Horizontal"));
        transform.eulerAngles += new Vector3(0, Input.GetAxis("Horizontal2")* (1f + OptionCtrl.sensitivity) * OptionCtrl.horizontalReverse, 0);
    }

    private IEnumerator RoketPunch()
    {
        isAttack = true;
        anm.SetTrigger("Attack");
        while(!anmInfo.IsName("Base Layer.Mesh_Attack02"))
        {
            yield return null;
        }
        se.PlayOneShot(se.clip);
        lineEffect.SetActive(true);
        GameObject attack = Instantiate(rocket, muzzle.position, transform.rotation);
        Destroy(attack, 1f);
        while (!anmInfo.IsName("Base Layer.Mesh_Idle"))
        {
            yield return null;
        }
        lineEffect.SetActive(false);
        isAttack = false;
        hpSlider.value -= 2.5f;
        yield return null;
    }

    private IEnumerator Avoidance()
    {
        isAttack = true;
        anm.SetTrigger("Avoidance");
        rb.AddForce(-transform.forward * 15f + transform.up * 5f, ForceMode.Impulse);
        while (!anmInfo.IsName("Base Layer.Mesh_Idle"))
        {
            yield return null;
        }
        
        isAttack = false;
        yield return null;
    }
}
