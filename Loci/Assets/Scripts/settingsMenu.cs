using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settingsMenu : MonoBehaviour {
    public GameObject settingsCanvas;
    public GameObject mainCanvas;
	// Use this for initialization
	void Start () {
		
	}

    public void gotoSettings()
    {
        mainCanvas.SetActive(true);
        settingsCanvas.SetActive(true);
    }
}
