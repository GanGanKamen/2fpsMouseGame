using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ScientistTest : MonoBehaviour {
    private NavMeshAgent agent;
    private GameObject target;
    private bool relaxStart;
    private int relaxCount;
    private GameObject[] friends;
    private int hp = 3;
    // Use this for initialization
    void Start () {
        relaxStart = false;
        relaxCount = 0;
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        target = GameObject.FindGameObjectWithTag("Player");
        if (SystemCtrl.canCtrl == true)
        {
            agent.destination = target.transform.position;
        }
        else
        {
            agent.destination = transform.position;
        }
        if (relaxStart == true)
        {
            relaxCount++;
            if (relaxCount >= 500)
            {
                rePatrol();
            }
        }
        if (SystemCtrl.isLightOff == true)
        {
            rePatrol();
        }
    }

    void rePatrol()
    {
        relaxCount = 0;
        relaxStart = false;
        GameObject prefab = (GameObject)Resources.Load("Enemy/Scientist(Patrol)Test");
        Instantiate(prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z),
        Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));
        Destroy(gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            relaxStart = true;
            agent.speed = 10f;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            relaxStart = false;
            relaxCount = 0;
            agent.speed = 20f;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            relaxStart = false;
            relaxCount = 0;
            agent.speed = 20f;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GroupCtrl.bigfnum += GroupCtrl.fnum;
            friends = GameObject.FindGameObjectsWithTag("NowFriend");
            for (int i = 0; i < friends.Length; i++)
            {
                Destroy(friends[i]);
            }
            Destroy(collision.gameObject);
            Debug.Log("destory");
            SystemCtrl.life -= 1;
            GroupCtrl.fnum = 0;
            rePatrol();
        }
        if (collision.gameObject.tag == "Rocket")
        {
            if (hp > 1)
            {
                hp -= 1;
            }
            else if (hp == 1)
            {
                GameObject clothes = (GameObject)Resources.Load("Clothes");
                Instantiate(clothes, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
