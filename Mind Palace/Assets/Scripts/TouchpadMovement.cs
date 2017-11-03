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
public class TouchpadMovement : MonoBehaviour {

    public Transform cameraRigTransform;
    public Transform pauseMenu;
    public Transform settingsMenu;

    private SteamVR_TrackedObject trackedObj;
    private Vector3 oldCamera;
    private Vector3 displacement;
    private Vector3 viewDir;
    private Vector3 normalViewDir;
    private float height;
    private float menuHeight;
    private SteamVR_Controller.Device device;
    private float speed = 5.0f;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetAxis().x != 0 || device.GetAxis().y != 0)
        {
            viewDir = SteamVR_Render.Top().GetRay().direction;
            normalViewDir = new Vector3(viewDir.z, 0, -viewDir.x);
            height = cameraRigTransform.position.y;
            oldCamera = cameraRigTransform.position;
            //move camera
            cameraRigTransform.position = cameraRigTransform.position + speed * Time.deltaTime * device.GetAxis().y * viewDir;
            cameraRigTransform.position = cameraRigTransform.position + speed * Time.deltaTime * device.GetAxis().x * normalViewDir;
            cameraRigTransform.position = new Vector3(cameraRigTransform.position.x, height, cameraRigTransform.position.z);
        }
    }
}
