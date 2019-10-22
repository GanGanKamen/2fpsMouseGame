using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class OpMovie : MonoBehaviour
{
    [SerializeField] private GameObject scientist;
    [SerializeField] private GameObject player;
    public CinemachineVirtualCamera vcamera1, vcamera2;
    [SerializeField] private float time;
    private bool isAction;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Moviestart());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(isAction == true)
        {
            player.transform.Translate(transform.forward * Time.deltaTime);
            scientist.transform.Translate(transform.forward * Time.deltaTime * 2);
        }
    }

    private IEnumerator Moviestart()
    {
        while(time <= 0.5f)
        {
            yield return null;
        }
        vcamera2.Priority = 20;
        while (time <= 1.5f)
        {
            yield return null;
            isAction = true;
        }

    }
}
