using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PictureCreator : MonoBehaviour {

	void Start ()
    {
        placePicture("Assets/Resources/seadragon.jpg", new Vector3(0f, 0f, 0f), new Vector3(3.978f, 2.6f, -3.11f));
        placePicture("Assets/Resources/owl.jpg", new Vector3(0f, 90f, 0f), new Vector3(2.135f, 2.577f, -4.7f));
        placePicture("Assets/Resources/treepic.jpg", new Vector3(0f, 0f, 0f), new Vector3(4.02f, 2.577f, 1.72f));
        placePicture("Assets/Resources/tamarin.jpg", new Vector3(0f, -90f, -0f), new Vector3(2.41f, 2.577f, 3.72f));
    }

    //Loads jpg from given file path and places it at the given position in the scene. Will be
    //rotated according to rot
    public void placePicture(string filePath, Vector3 rot, Vector3 pos)
    {
        Texture2D img = LoadImg(filePath);
        if (img == null) return;

        GameObject pic = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(pic.GetComponent<Rigidbody>());
        pic.transform.position = pos;
        pic.transform.Rotate(rot);

        int w = img.width;
        int h = img.height;
        if(w >= h)
            pic.transform.localScale = new Vector3(0.05f, 1f, (float) w / h);
        else
            pic.transform.localScale = new Vector3(0.05f, (float) h / w, 1f);

        pic.GetComponent<Renderer>().material.mainTexture = img;
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
