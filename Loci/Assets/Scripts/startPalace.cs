using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startPalace : MonoBehaviour {


	void Start () {
	}

    public void startGame()
    {
        SceneManager.LoadScene("OrangeDemo");
    }
	
}
