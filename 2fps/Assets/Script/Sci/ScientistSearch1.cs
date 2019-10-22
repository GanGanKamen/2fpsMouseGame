using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistSearch1 : MonoBehaviour {
    public Animator anmScientist;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        Patrol();
        if (SystemCtrl.canCtrl == true)
        {
            anmScientist.speed = 1;
        }
        else
        {
            anmScientist.speed = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && SystemCtrl.isLightOff == false && SystemCtrl.isStealth == false)
        {
            GameObject prefab = (GameObject)Resources.Load("Enemy/Scientist1");
            Instantiate(prefab, new Vector3(transform.position.x, transform.position.y, transform.position.z),
            Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));
            Destroy(gameObject);
        }
    }
    void Patrol()
    {
        if (SystemCtrl.isLightOff == false)
        {
            anmScientist.SetBool("Stop", false);
        }
        else
        {
            anmScientist.SetBool("Stop", true);
        }
    }
}
