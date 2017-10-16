using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PictureCreator : MonoBehaviour {

	void Start () {
        placePicture("Assets/Resources/seadragon.jpg", 0f, 0f, 0f, new Vector3(3.978f, 2.6f, -3.11f));
        placePicture("Assets/Resources/owl.jpg", 0f, 90f, 0f, new Vector3(2.135f, 2.577f, -4.7f));
        placePicture("Assets/Resources/treepic.jpg", 0f, 0f, 0f, new Vector3(4.02f, 2.577f, 1.72f));
        placePicture("Assets/Resources/tamarin.jpg", 0f, -90f, 0f, new Vector3(2.41f, 2.577f, 3.72f));
    }

    //Loads jpg from given file path and places it at the given position in the scene. Will be
    //rotated according to rotx, roty, rotz values
    public void placePicture(string filePath, float rotx, float roty, float rotz, Vector3 pos)
    {
        Texture2D img = LoadImg(filePath);
        GameObject gameObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(gameObj.GetComponent<Rigidbody>());
        gameObj.transform.position = pos;
        gameObj.transform.Rotate(rotx, roty, rotz);

        int w = img.width;
        int h = img.height;
        if(w >= h)
            gameObj.transform.localScale = new Vector3(0.05f, 1f, (float) w / h);
        else
            gameObj.transform.localScale = new Vector3(0.05f, (float) h / w, 1f);

        gameObj.GetComponent<Renderer>().material.mainTexture = img;
    }

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
