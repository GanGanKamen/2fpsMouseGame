using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameramove : MonoBehaviour {
    private Vector3 startpos;
	// Use this for initialization
	void Start () {
		startpos = new Vector3(0, 0.5f, -3f);
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(transform.position);
	}

    /*private void OnCollisionStay(Collision collision)
    {
        transform.Translate(transform.forward);
    }*/
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("stay");
        transform.Translate(new Vector3(0, 0, 1f));
    }

}
