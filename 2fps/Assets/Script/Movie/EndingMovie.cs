using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EndingMovie : MonoBehaviour
{
    public GameObject building;
    public GameObject fire;
    public GameObject player;
    public GameObject canvas;
    public CinemachineVirtualCamera vcamera1, vcamera2;
    [SerializeField] private float time = 0;
    private bool isMove = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MovieStart());
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        PlayerMove();
    }

    private IEnumerator MovieStart()
    {
        fire.SetActive(false);
        vcamera1.Priority = 10;
        while (time <= 1f)
        {
            yield return null;
        }
        GameObject explosion2 = (GameObject)Resources.Load("Effect/Explosion2");
        Instantiate(explosion2, building.transform.position + new Vector3(0, 10f, 0), Quaternion.identity);
        while(time <= 2f)
        {
            yield return null;
        }
        fire.SetActive(true);
        Instantiate(explosion2, building.transform.position + new Vector3(0, 7f, 0), Quaternion.identity);
        while (time <= 3f)
        {
            yield return null;
        }
        GameObject explosion = (GameObject)Resources.Load("Effect/Explosion");
        Destroy(building);
        Instantiate(explosion, building.transform.position + new Vector3(3, 5f, -2.5f), Quaternion.identity);
        
        vcamera2.Priority = 20;
        player.SetActive(true);
        isMove = true;
        while (time <= 4f)
        {
            yield return null;
        }
        canvas.SetActive(true);
    }

    private void PlayerMove()
    {
        if(isMove == true)
        {
            player.transform.Translate(transform.forward * Time.deltaTime);
        }
    }
}
