using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Volume();
        Destroy(gameObject, 3f);
	}

    private void Volume()
    {
        if (gameObject.name.StartsWith("Explosion")|| gameObject.name.StartsWith("Smoke"))
        {
            AudioSource[] sources = GetComponents<AudioSource>();
            for(int i = 0;i < sources.Length; i++)
            {
                sources[i].volume = OptionCtrl.volume / 10;
            }
        }
    }
}
