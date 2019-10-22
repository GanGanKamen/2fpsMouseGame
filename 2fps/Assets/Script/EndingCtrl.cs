using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCtrl : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EndingStart());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        KeyCtrl();
        bgm.volume = OptionCtrl.volume / 10;
    }

    private IEnumerator EndingStart()
    {
        while (time < 5)
        {
            yield return null;
        }
        bgm.PlayOneShot(bgm.clip);
        while(time < 40)
        {
            yield return null;
        }
        GroupCtrl.fnum = 0; GroupCtrl.nowStage = 0; GroupCtrl.bigfnum = 0; GroupCtrl.nowStageFnum = 0;
        SceneManager.LoadScene("Title");
    }

    private void KeyCtrl()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            GroupCtrl.fnum = 0; GroupCtrl.nowStage = 0; GroupCtrl.bigfnum = 0; GroupCtrl.nowStageFnum = 0;
            SceneManager.LoadScene("2fps");
        }
    }
}
