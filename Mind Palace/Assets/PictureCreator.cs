using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PictureCreator : MonoBehaviour {

	void Start ()
    {
        placePicture("Assets/Resources/seadragon.jpg", 180f, new Vector3(-4.045f, 2.6f, -3.11f));
        placePicture("Assets/Resources/owl.jpg", 90f, new Vector3(2.135f, 2.577f, -4.7f));
        placePicture("Assets/Resources/treepic.jpg", 0f, new Vector3(4.02f, 2.577f, 1.72f));
        placePicture("Assets/Resources/tamarin.jpg", -90f, new Vector3(2.41f, 2.577f, 3.72f));
    }

    //Loads jpg from given file path and places it at the given position in the scene. Will be
    //rotated around the y-axis using roty
    public void placePicture(string filePath, float roty, Vector3 pos)
    {
        Texture2D img = LoadImg(filePath);
        if (img == null) return;

        GameObject pic = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(pic.GetComponent<Rigidbody>());
        pic.transform.position = pos;
        pic.transform.Rotate(0f, roty, 0f);

        float w, h;
        if(img.width >= img.height)
        {
            w = (float)img.width / img.height;
            h = 1f;
        }
        else
        {
            w = 1f;
            h = (float)img.height/ img.width;
        }
 
        pic.transform.localScale = new Vector3(0.05f, h, w);

        pic.GetComponent<Renderer>().material.mainTexture = img;

        if (roty % 90 != 0) return; //Cannot make frames unless roty%90 = 0

        GameObject sideFrame1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(sideFrame1.GetComponent<Rigidbody>());
        GameObject sideFrame2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(sideFrame2.GetComponent<Rigidbody>());

        if((roty / 90) % 2 == 0)
        {
            sideFrame1.transform.position = pos + new Vector3(0f, 0f, w/2 + 0.025f);
            sideFrame1.transform.Rotate(0f, roty, 0f);
            sideFrame1.transform.localScale = new Vector3(0.05f, h, 0.05f);
            sideFrame2.transform.position = pos - new Vector3(0f, 0f, w/2 + 0.025f);
            sideFrame2.transform.Rotate(0f, roty, 0f);
            sideFrame2.transform.localScale = new Vector3(0.05f, h, 0.05f);
        }
        else
        {
            sideFrame1.transform.position = pos + new Vector3(w/2 + 0.025f, 0f, 0f);
            sideFrame1.transform.Rotate(0f, roty, 0f);
            sideFrame1.transform.localScale = new Vector3(0.05f, h, 0.05f);
            sideFrame2.transform.position = pos - new Vector3(w/2 + 0.025f, 0f, 0f);
            sideFrame2.transform.Rotate(0f, roty, 0f);
            sideFrame2.transform.localScale = new Vector3(0.05f, h, 0.05f);
        }

        GameObject topFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(topFrame.GetComponent<Rigidbody>());
        topFrame.transform.position = pos + new Vector3(0f, h/2 + 0.025f, 0f);
        topFrame.transform.Rotate(0f, roty, 0f);
        topFrame.transform.localScale = new Vector3(0.05f, 0.05f, w + 0.1f);

        GameObject bottomFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(bottomFrame.GetComponent<Rigidbody>());
        bottomFrame.transform.position = pos - new Vector3(0f, h / 2 + 0.025f, 0f);
        bottomFrame.transform.Rotate(0f, roty, 0f);
        bottomFrame.transform.localScale = new Vector3(0.05f, 0.05f, w + 0.1f);
    }

    //Returns a Texture2D object containing the image at the given filePath
    public static Texture2D LoadImg(string filePath)
    {
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);
        }
        return tex;
    }
}
