using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

// Attach this script to an object to define how it behaves when it is grabbed, ungrabbed, and used

public class ObjectBehavior : MonoBehaviour {

    void Start()
    {
        //Set functions to be used when object is grabbed or ungrabbed
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(ObjectGrabbed);
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += new InteractableObjectEventHandler(ObjectUngrabbed);
        GetComponent<VRTK_InteractableObject>().InteractableObjectUsed += new InteractableObjectEventHandler(ObjectUsed);
    }
 
    // Update is called once per frame
    void Update ()
    {
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        if (transform.position.y < 0)
        {
            Destroy(GetComponent<Rigidbody>());
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }

    //Called when user grabs the object
    private void ObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        Destroy(GetComponent<Rigidbody>());
    }

    //Called when user lets go of object
    private void ObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
    }

    //Called when object is used
    private void ObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        //Delete object
        Destroy(transform.root.gameObject);

        //Tell controllers that an object is no longer being held
        GameObject controllerGameObj = VRTK_DeviceFinder.GetControllerLeftHand();
        GameObject controllerGameObj2 = VRTK_DeviceFinder.GetControllerRightHand();
        VRTK_InteractGrab myGrab = controllerGameObj.GetComponent<VRTK_InteractGrab>();
        VRTK_InteractGrab myGrab2 = controllerGameObj2.GetComponent<VRTK_InteractGrab>();
        myGrab.ForceRelease();
        myGrab2.ForceRelease();
    }
}
