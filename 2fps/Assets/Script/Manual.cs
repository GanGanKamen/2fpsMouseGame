using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manual : MonoBehaviour
{
    public GameObject keyboard;
    public GameObject dualshock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnKeyboard()
    {
        keyboard.SetActive(true);
        dualshock.SetActive(false);
    }

    public void OnDualshock()
    {
        keyboard.SetActive(false);
        dualshock.SetActive(true);
    }
}
