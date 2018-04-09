using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTracker : MonoBehaviour {
    public GameObject menu;
    public Transform cameraRigTransform;

    private Vector3 viewDir;
    private float height = 1.5f;
    private float distance = 3f;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(menu.activeSelf)
        {
            viewDir = SteamVR_Render.Top().GetRay().direction;
            menu.transform.position = cameraRigTransform.position + distance * new Vector3(viewDir.normalized.x, height / distance, viewDir.normalized.z);
            menu.transform.rotation = Quaternion.LookRotation(new Vector3(viewDir.x, 0, viewDir.z));
        }
		
	}
}
