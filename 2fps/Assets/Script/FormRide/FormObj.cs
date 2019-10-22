using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormObj : MonoBehaviour {
    private bool canAction;
    [SerializeField] private int actionNum = 0;
    private GameObject player;
    public GameObject actionWindow;
    // Use this for initialization
    void Start () {
        canAction = false;
        if (gameObject.tag == "Clothes")
        {
            Destroy(gameObject, 30f);
        }
    }
	
	// Update is called once per frame
	void Update () {
        MarkOn();
        if(SystemCtrl.canCtrl == true)
        {
            KeyCtrl();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Body")
        {
            player = other.transform.parent.gameObject;
            if(gameObject.tag == "Cup"&& GroupCtrl.fnum == 0)
            {
                canAction = true;
                actionNum = 4;
            }
            if(gameObject.tag == "Cardboard" && GroupCtrl.fnum >= 5)
            {
                canAction = true;
                actionNum = 1;
            }
            if(gameObject.tag == "Clothes" && GroupCtrl.fnum >= 15)
            {
                canAction = true;
                actionNum = 2;
            }
            if(gameObject.tag == "Trash" && GroupCtrl.fnum >= 5)
            {
                canAction = true;
                actionNum = 3;
            }
            if(gameObject.tag == "FireEX" )//&& GroupCtrl.fnum >= 10)
            {
                canAction = true;
                actionNum = 5;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Body")
        {
            canAction = false;
        }
    }

    private void Action()
    {
        switch (actionNum)
        {
            case 0:
                break;
            case 1:
                GameObject cardboard_player = (GameObject)Resources.Load("Cardboard(Player)");
                Instantiate(cardboard_player, new Vector3(player.transform.position.x, 0, player.transform.position.z),player.transform.rotation);
                if(OptionCtrl.movieSkip == false)
                {
                    SystemCtrl.movieSwitch = 1;
                    SystemCtrl.canCtrl = false;
                }               
                Destroy(player);
                Destroy(gameObject);
                break;

            case 2:
                GameObject clothes_player
             = (GameObject)Resources.Load("Clothes(Player)");
                Instantiate(clothes_player, new Vector3(player.transform.position.x, 8f, player.transform.position.z), player.transform.rotation);
                if (OptionCtrl.movieSkip == false)
                {
                    SystemCtrl.movieSwitch = 7;
                    SystemCtrl.canCtrl = false;
                }
                Destroy(player);
                Destroy(gameObject);
                break;

            case 3:
                GameObject trash_player = (GameObject)Resources.Load("Trash(Player)");
                Instantiate(trash_player, player.transform.position, player.transform.rotation).transform.eulerAngles = player.transform.eulerAngles - new Vector3(0, 90f, 0);
                if (OptionCtrl.movieSkip == false)
                {
                    SystemCtrl.movieSwitch = 3;
                    SystemCtrl.canCtrl = false;
                }
                Destroy(player);
                Destroy(gameObject);
                break;
            case 4:
                GameObject cup_player = (GameObject)Resources.Load("Cup(Player)");
                Instantiate(cup_player, player.transform.position + new Vector3(0,0.2f,0), player.transform.rotation);
                if (OptionCtrl.movieSkip == false)
                {
                    SystemCtrl.movieSwitch = 6;
                    SystemCtrl.canCtrl = false;
                }
                Destroy(player);
                Destroy(gameObject);
                break;
            case 5:
                GameObject fire_player = (GameObject)Resources.Load("Fire(Player)");
                Instantiate(fire_player, transform.position, transform.rotation);
                Destroy(player);
                Destroy(gameObject);
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(gameObject.tag == "Clothes")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }
    }

    private void KeyCtrl()
    {
        if (Input.GetButtonDown("Fire3") && canAction == true)
        {
            Action();
        }
    }

    private void MarkOn()
    {
        if(canAction == true)
        {
            actionWindow.SetActive(true);
        }
        else
        {
            actionWindow.SetActive(false);
        }
    }
}
