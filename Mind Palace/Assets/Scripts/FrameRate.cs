using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRate : MonoBehaviour {
    int i = 0;
    public int rate = 15;
    float sum = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (i == rate)
        {
            print(1/(sum / rate));
            i = 0;
            sum = 0;
        }
        else
        {
            i++;
            sum += Time.deltaTime;
        }
	}
}
