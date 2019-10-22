using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerTest : MonoBehaviour {
    public float speed;
    public GameObject attack;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.forward * speed * Time.deltaTime * Input.GetAxis("Vertical"));
        transform.Translate(Vector3.left * speed * Time.deltaTime * Input.GetAxis("Horizontal"));
        transform.eulerAngles += new Vector3(0, Input.GetAxis("Horizontal2") , 0);
        if (Input.GetButton("Fire3"))
        {
            attack.SetActive(true);
        }
        else
        {
            attack.SetActive(false);
        }
    }
}
