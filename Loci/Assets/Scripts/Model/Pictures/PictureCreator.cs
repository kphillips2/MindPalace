using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PictureCreator : MonoBehaviour {

    private float scale = 1.8f; //Increasing this value will make the pictures larger
    private float maxWidth = 3.55f; //Maximum width that a picture can be
    private float maxHeight = 4f; //Maximum height that a picture can be

    //Loads a jpg or png image from the given file path and places it at position indicated by pos.
    //roty specifies the value by which to rotate the image around the y-axis.
    public GameObject placePicture(string filePath, float roty, Vector3 pos)
    {
        //Loads image, returns if image cannot be found
        Texture2D img = LoadImg(filePath);
        if (img == null) return null;

        //Creates a cube and places it at the given position. Rotates it around the y-axis.
        GameObject pic = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(pic.GetComponent<Rigidbody>());
        pic.transform.position = pos;
        pic.transform.Rotate(0f, roty, 0f);

        //Calculates the width and height of the cube. Sets the larger of the two dimensions
        //to equal the width-height ratio of the image, and the other dimension to 1.
        float w, h;
        if(img.width >= img.height)
        {
            w = (float)img.width / img.height;
            h = 1f;

            //Scale down if too long
            if(w * scale > maxWidth)
            {
                h = h * maxWidth / (w*scale);
                w = maxWidth/scale;
            }
        }
        else
        {
            w = 1f;
            h = (float)img.height/ img.width;

            //Scale down if too tall
            if (h * scale > maxHeight)
            {
                w = w * maxHeight / (h*scale);
                h = maxHeight/scale;
            }
        }
 
        pic.transform.localScale = new Vector3(0.05f, scale * h, scale * w); //Scales the cube
        pic.GetComponent<Renderer>().material.mainTexture = img; //Textures cube with the image
        //framePicture(roty, pos, scale * w, scale * h);
        return pic;
    }

    //Places a frame around a picture that is positioned at pos and rotated around the y-axis by roty.
    //The picture has a width of w and a height of h.
    //Frame code is meant for pictures on walls at 90 degree angles, so will return without making 
    //frames if the picture is rotated at an odd angle
    private static void framePicture(float roty, Vector3 pos, float w, float h)
    {
        if (roty % 90 != 0) return;
        createSideFrames(roty, pos, w, h);
        createTopBottomFrames(roty, pos, w, h);
    }

    //Places a scaled cube both above and below an image so as to form half of a picture
    //frame. The image is at position pos, rotated around the y-axis by roty, with width
    //w and height h.
    private static void createTopBottomFrames(float roty, Vector3 pos, float w, float h)
    {
        //Create two cubes
        GameObject topFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(topFrame.GetComponent<Rigidbody>());
        GameObject bottomFrame = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(bottomFrame.GetComponent<Rigidbody>());

        //Move frames along y-axis to get proper positioning
        topFrame.transform.position = pos + new Vector3(0f, h / 2 + 0.025f, 0f);
        bottomFrame.transform.position = pos - new Vector3(0f, h / 2 + 0.025f, 0f);

        //Rotate frames by same value as picture. Frames have a slightly larger
        //width than picture so as to connect with the side frames
        topFrame.transform.Rotate(0f, roty, 0f);
        topFrame.transform.localScale = new Vector3(0.05f, 0.05f, w + 0.1f);
        bottomFrame.transform.Rotate(0f, roty, 0f);
        bottomFrame.transform.localScale = new Vector3(0.05f, 0.05f, w + 0.1f);
    }

    //Places two scaled cubes on either side of an image so as to form half of a picture
    //frame. The image is at position pos, rotated around the y-axis by roty, with width
    //w and height h.
    private static void createSideFrames(float roty, Vector3 pos, float w, float h)
    {
        //Create two cubes to be placed on sides of picture
        GameObject sideFrame1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(sideFrame1.GetComponent<Rigidbody>());
        GameObject sideFrame2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Destroy(sideFrame2.GetComponent<Rigidbody>());

        //If image is rotated 0 or 180 degrees around y-axis, the frames will need to be 
        //moved along the z-axis to have the proper positioning. If image is rotated 90 or
        //270 degrees, the frames will be moved along the x-axis.
        if ((roty / 90) % 2 == 0)
        {
            sideFrame1.transform.position = pos + new Vector3(0f, 0f, w / 2 + 0.025f);
            sideFrame2.transform.position = pos - new Vector3(0f, 0f, w / 2 + 0.025f);   
        }
        else
        {
            sideFrame1.transform.position = pos + new Vector3(w / 2 + 0.025f, 0f, 0f);
            sideFrame2.transform.position = pos - new Vector3(w / 2 + 0.025f, 0f, 0f);
        }

        //Rotate frames by same value as picture. Frames have same height as picture
        sideFrame1.transform.Rotate(0f, roty, 0f);
        sideFrame1.transform.localScale = new Vector3(0.05f, h, 0.05f); 
        sideFrame2.transform.Rotate(0f, roty, 0f);
        sideFrame2.transform.localScale = new Vector3(0.05f, h, 0.05f);
    }

    //Creates a Texture2D object and loads the image at the given file path into it
    //Returns the texture object, or returns null if image cannot be found
    public static Texture2D LoadImg(string filePath)
    {
        Texture2D texture = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
            texture = new Texture2D(2, 2);
            texture.LoadImage(fileData);
        }
        return texture;
    }

    //Get byte array version of image data
    public static byte[] GetImageData(string filePath)
    {
        byte[] fileData = null;

        if (File.Exists(filePath))
        {
            fileData = File.ReadAllBytes(filePath);
        }
        return fileData;
    }

    // Creates a Sprite version of picture to be used in menus. Can be jpg or png
    public static Sprite LoadNewSprite(string filePath)
    {
        float pixelsPerUnit = 100.0f;
        Sprite newSprite = new Sprite();
        Texture2D spriteTexture = LoadImg(filePath);
        newSprite = Sprite.Create(spriteTexture, new Rect(0, 0, spriteTexture.width, spriteTexture.height), new Vector2(0, 0), pixelsPerUnit);
        return newSprite;
    }
}
