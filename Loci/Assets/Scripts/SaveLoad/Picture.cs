using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Class for storing informaiton about a picture

[System.Serializable]
public class Picture {

    private string filePath; //File path it exists at
    private float roty; //Rotation around y-axis
    private float[] location = new float[3]; //Vector3 Location in palace

    // Constructors
    public Picture(){
    }

    public Picture(string filePath, float roty, Vector3 location)
    {
        this.filePath = String.Copy(filePath);
        this.roty = roty;
        this.location = new float[] { location.x, location.y, location.z };
    }

    // Getters
    public string getFilePath() { return String.Copy(filePath); }
    public float getRoty() { return roty; }
    public Vector3 getLocation()
    {
        return new Vector3(location[0],location[1],location[2]);
    }
}
