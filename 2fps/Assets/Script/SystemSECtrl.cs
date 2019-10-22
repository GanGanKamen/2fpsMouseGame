using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSECtrl : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private int num;
    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
        for(int i = 0; i < num; i++)
        {
            AudioSource se = audioSources[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = OptionCtrl.volume / 10;
        }
    }
}
