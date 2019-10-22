using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GroupCtrl : MonoBehaviour {
    static public int fnum;
    static public int bigfnum = 0;
    static public int nowStageFnum = 0;
    [SerializeField]  private GameObject[] friends;
    [SerializeField] private int prefnum = 0;
    [SerializeField] private GameObject player;
    [SerializeField]  private GameObject newfriends;
    static public int nowStage;
    [SerializeField] private int preStage;
    // Use this for initialization
    void Start () {
        fnum = 0;
        newfriends = (GameObject)Resources.Load("NowFriend");
        DontDestroyOnLoad(this);
        nowStage = 0;
        preStage = nowStage;
    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(bigfnum);
        player = GameObject.FindGameObjectWithTag("Player");
        GotoNextStage();
        if (prefnum < fnum)
        {
            prefnum += 1;
            Instantiate(newfriends, new Vector3(player.transform.position.x + 0.1f,0.1f, player.transform.position.z + 0.1f), Quaternion.identity);
        }
        else if(prefnum > fnum)
        {
            prefnum = fnum;
        }
        friends = GameObject.FindGameObjectsWithTag("NowFriend");
    }

    void GotoNextStage()
    {
        if (nowStage > preStage)
        {
            preStage += 1;
            bigfnum = 0;
            switch (nowStage)
            {
                case 0:
                    break;
                case 1:
                    fnum = 0;
                    SceneManager.LoadScene("Stage");
                    break;
                case 2:
                    SceneManager.LoadScene("Stage2");
                    break;
                case 3:
                    SceneManager.LoadScene("Stage3");
                    break;
                case 4:
                    SystemCtrl.movieSwitch = 10;
                    break;
            }
        }
        else if(nowStage < preStage)
        {
            preStage = nowStage;
        }
    }
}
