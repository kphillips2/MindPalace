using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Written by: Max Baker
 * Last Modified: 10/1/17
 * 
 * Purpose: Test run for implementing Vive movement via trackpad.
 * 
 * Usage: Drag to a Controller object in unity, then drag the [CameraRig] into the cameraRigTransform field.
 */

public class BetterController : MonoBehaviour {

    private SteamVR_TrackedObject trackedObj;
    public Transform cameraRigTransform;
    public Vector3 viewDir;
    public Vector3 normalViewDir;

    private float height;
    private SteamVR_Controller.Device device;
    private float speed = 5.0f;
	void Start () {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if(device.GetAxis().x != 0 || device.GetAxis().y != 0)
        {
            viewDir = SteamVR_Render.Top().GetRay().direction;
            normalViewDir = new Vector3(viewDir.z, 0, -viewDir.x);
            height = cameraRigTransform.position.y;
            cameraRigTransform.position = cameraRigTransform.position + speed*Time.deltaTime*device.GetAxis().y * viewDir;
            cameraRigTransform.position = cameraRigTransform.position + speed*Time.deltaTime * device.GetAxis().x * normalViewDir;
            cameraRigTransform.position = new Vector3(cameraRigTransform.position.x, height, cameraRigTransform.position.z);
        }
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            viewDir = SteamVR_Render.Top().GetRay().direction;
            Debug.Log(viewDir);
        }
	}
}
