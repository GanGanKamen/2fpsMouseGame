using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialCtrl : MonoBehaviour {
    public Animator WindowAnm;
    public GameObject gate, gate1, gate2, gate3,gate4;
    public Text text;
    //public GameObject scientistNo3;
    private GameObject[] scientists;
    private GameObject[] scientists2;
    private GameObject[] cardboards;
    private GameObject[] trashes;
    public GameObject load;
    private int textSwitch;
    private int preText;
    static public bool isAttack;
    static public bool isClear;
	// Use this for initialization
	void Start () {
        textSwitch = 0;
        preText = 0;
        isAttack = false;
        isClear = false;
    }
	
	// Update is called once per frame
	void Update () {
        scientists = GameObject.FindGameObjectsWithTag("ScientistP");
        scientists2 = GameObject.FindGameObjectsWithTag("Scientist");
        cardboards = GameObject.FindGameObjectsWithTag("Cardboard");
        trashes = GameObject.FindGameObjectsWithTag("Trash");
        GateOpen();
        WindowCtrl();
    }

    void GateOpen()
    {
        if(GroupCtrl.fnum > 0)
        {
            gate.SetActive(false);
        }

        if (GroupCtrl.fnum >= 3)
        {
            gate1.SetActive(false);
        }
        if (scientists.Length <3)
        {
            gate2.SetActive(false);
        }
        if (scientists.Length<2)
        {
            gate3.SetActive(false);
            isAttack = true;
            //scientistNo3.SetActive(false);
            //GameObject scientistNo3Chase = (GameObject)Resources.Load("Enemy/ScientistTest");
            //Instantiate(scientistNo3Chase, scientistNo3.transform.position, Quaternion.Euler(0, scientistNo3.transform.rotation.eulerAngles.y, 0));
        }
        if(scientists.Length + scientists2.Length == 0)
        {
            gate4.SetActive(false);
        }
    }

    void WindowCtrl()
    {
        if (SystemCtrl.canCtrl == true && GroupCtrl.fnum == 0 && scientists.Length == 3)
        {
            textSwitch = 1;
        }
        if (GroupCtrl.fnum == 1 && scientists.Length == 3)
        {
            textSwitch = 2;
        }
        if(GroupCtrl.fnum == 2 && scientists.Length == 3)
        {
            textSwitch = 11;
        }
        if (GroupCtrl.fnum == 3 && scientists.Length == 3)
        {
            textSwitch = 3;
        }
        if (GroupCtrl.fnum == 3 && scientists.Length == 2)
        {
            textSwitch = 4;
        }
        if (GroupCtrl.fnum == 5 && scientists.Length == 3 && cardboards.Length == 3)
        {
            textSwitch = 10;
        }
        if (GroupCtrl.fnum == 5 && scientists.Length == 2 && cardboards.Length == 2)
        {
            textSwitch = 5;
        }
        if (GroupCtrl.fnum == 5 && scientists.Length == 2 && trashes.Length == 2)
        {
            textSwitch = 6;
        }
        if(SystemCtrl.life == 2 && GroupCtrl.fnum == 0 && scientists.Length == 1)
        {
            textSwitch = 7;
        }
        if(SystemCtrl.life == 2 && GroupCtrl.fnum == 5 && scientists.Length == 1)
        {
            textSwitch = 8;
        }
        if(isClear == true)
        {
            textSwitch = 9;
            Invoke("TutorialClear", 2f);
        }

        switch (textSwitch)
        {
            case 1:
                text.text = "まずは操作方法を確認して 仲間を助けよう";
                break;
            case 2:
                text.text = "緑の面から机を登って 仲間を助けよう";
                break;
            case 3:
                text.text = "また研究員だ 何か使えるものないかな...";
                break;
            case 4:
                text.text = "安心するのはまだ早い 早く仲間を助けよう";
                break;
            case 5:
                text.text = "操作方法が少し変わってるよ　もう一回確認して";
                break;
            case 6:
                text.text = "操作方法が少し変わってるよ　もう一回確認して";
                break;
            case 7:
                text.text = "大きいケージを探して もう一回仲間を助けよう！";
                break;
            case 8:
                text.text = "早くここから出よう 今度はバレないように慎重に行動しよう";
                break;
            case 9:
                text.text = "やった！これでチュートリアルは終わり おめでとうございます!";
                break;
            case 10:
                text.text = "研究員に気を付けて 出口まであと少しだ";
                break;
            case 11:
                text.text = "研究員に気を付けて バレないように進もう！";
                break;
        }
        
        if(preText != textSwitch)
        {
            WindowAnm.SetTrigger("Change");
            preText = textSwitch;
        }
    }

    void TutorialClear()
    {
        SystemCtrl.canCtrl = false;
        load.SetActive(true);
        GroupCtrl.fnum = 0;
        SceneManager.LoadScene("Title");
    }
}
