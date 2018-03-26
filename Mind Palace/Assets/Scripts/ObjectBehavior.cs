using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

// Attach this script to an object to define how it behaves when it is grabbed, ungrabbed, and used

public class ObjectBehavior : MonoBehaviour {
    private bool isBeingGrabbed; //True if object is being grabbed
    private Rigidbody rbody; //The object's rigidbody

    void Start()
    {
        //Set functions to be used when object is grabbed or ungrabbed or used
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(ObjectGrabbed);
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += new InteractableObjectEventHandler(ObjectUngrabbed);
        GetComponent<VRTK_InteractableObject>().InteractableObjectUsed += new InteractableObjectEventHandler(ObjectUsed);

        isBeingGrabbed = true;
        rbody = transform.GetComponent<Rigidbody>();
    }
 
    // Update is called once per frame
    void Update ()
    {
        //If object isn't being grabbed and is not moving, reset the isKinematic property
        if (!isBeingGrabbed && rbody.IsSleeping())
        {
            rbody.isKinematic = true;
        }
    }

    //Called when user grabs the object
    private void ObjectGrabbed(object sender, InteractableObjectEventArgs e)
    {
        isBeingGrabbed = true;
        transform.GetComponent<Rigidbody>().isKinematic = false;
    }

    //Called when user lets go of object
    private void ObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        isBeingGrabbed = false;
        transform.GetComponent<Rigidbody>().isKinematic = false;
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
