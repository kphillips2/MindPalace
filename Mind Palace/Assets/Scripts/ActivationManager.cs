using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationManager : MonoBehaviour {

    private bool Activated = false;
    private GameObject ActivatedObject = null;

	void Start () {
		
	}

    public void ActivateMenu(GameObject NewActivation)
    {
        if (Activated == false)
        {
            ActivatedObject = NewActivation;
            Activated = true;
        }
        else
        {
            ActivatedObject.GetComponent<subMenuButtons>().DefaultState();
            ActivatedObject = NewActivation;
        }
    }

    public void NoActive()
    {
        Activated = false;
    }
}
