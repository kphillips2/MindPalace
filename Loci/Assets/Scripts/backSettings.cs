using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backSettings : MonoBehaviour {
    public GameObject settingsCanvas;
    public GameObject mainCanvas;

    // Use this for initialization
    void Start () {
		
	}

    public void gotoMain()
    {
        mainCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
    }
}
