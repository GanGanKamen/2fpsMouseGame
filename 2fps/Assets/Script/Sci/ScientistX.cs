using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistX : MonoBehaviour
{
    [SerializeField] private AudioSource se01, se02;
    [SerializeField] private float time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(SystemCtrl.canCtrl == true)
        {
            if (time >= 10)
            {
                StartCoroutine(VcStart());  
            }
            else if(time < 10)
            {
                time += Time.deltaTime;
            }
        }
        se01.volume = OptionCtrl.volume / 10; se02.volume = OptionCtrl.volume / 10;
    }

    private IEnumerator VcStart()
    {
        time = 0;
        int value = Random.Range(1, 3);
        yield return null;
        switch (value)
        {
            case 1:
                se01.PlayOneShot(se01.clip);
                break;
            case 2:
                se02.PlayOneShot(se02.clip);
                break;
        }
        yield return null;
    }
}
