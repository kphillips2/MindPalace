using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attaching this script to an object will make it so that when the user grabs the object, the xz
// coordinates will be updated but the object will remain stuck to the ground

public class SnapToGround : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);

        if (transform.position.y != 0) {
            //Rigidbody is added to object when it's picked up, needs to be removed or else object slides
            Destroy(GetComponent<Rigidbody>());
            transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
    }
}
