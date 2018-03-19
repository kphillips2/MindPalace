using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

// Attaching this script to an object will make it so that when the user grabs the object, the xz
// coordinates will be updated but the object will remain stuck to the ground

public class SnapToGround : MonoBehaviour {

    void Start()
    {
        //Set functions to be used when object is grabbed or ungrabbed
        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(ObjectGrabbed);
        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += new InteractableObjectEventHandler(ObjectUngrabbed);
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
       // Destroy(GetComponent<Rigidbody>());
        //transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        //transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
    }
}
