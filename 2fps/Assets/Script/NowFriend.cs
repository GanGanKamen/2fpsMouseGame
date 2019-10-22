using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NowFriend : MonoBehaviour {
    private GameObject playerP;
    private NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        playerP = GameObject.FindGameObjectWithTag("ChaseTarget");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 target = new Vector3(playerP.transform.position.x+Random.Range(-1f,1f), 0.5f, playerP.transform.position.z + Random.Range(-1f, 1f));
        agent.destination = target;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Scientist")
        {
            GroupCtrl.fnum -= 1;
            Destroy(gameObject);
        }
    }
}
