using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour {
    static public int fnum;
	// Use this for initialization
	void Start () {
        fnum = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Friend")
        {
            fnum++;
        }
    }
}
