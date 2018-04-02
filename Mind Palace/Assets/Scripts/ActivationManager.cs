using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationManager : MonoBehaviour {

    private bool Activated = false;
    private GameObject ActivatedObject = null;
    public GameObject ImageMenu;

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
            ActivatedObject.GetComponent<SubMenuHandler>().DefaultState();
            ActivatedObject = NewActivation;
            ImageMenu.transform.position = new Vector3(0, -100, 0); //Like Deactivating, but deactivating breaks it
        }
    }

    public void NoActive()
    {
        Activated = false;
    }
}
