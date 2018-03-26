using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class RoomData {
    private float width;
    private float length;
    private float[] centre = new float[3];
    private List<float[]>[] doorsAndWindows = new List<float[]>[4];
    private string[] materials = new string[3];

    public RoomData(float wid, float len, float[] loc){
        width = wid;
        length = len;

        if (loc.Length != 3)
            Debug.Log ("Error: centre array length must be 3");
        else
            Array.Copy (loc, centre, 3);
    }
    
    // setters
    public void setMaterials(string[] mats){
        if (mats.Length != 3)
            Debug.Log ("Error: materials array length must be 3");
        else
            Array.Copy (mats, materials, 3);
    }
    public void setDoorsAndWindows(List<float[]>[] wallData){
        for (int k = 0; k < 4; k++) {
            doorsAndWindows [k].Clear ();
            foreach (float[] loc in wallData [k])
                doorsAndWindows [k].Add (loc);
        }
    }
    
    // getters
    public float getWidth() { return width; }
    public float getLength() { return length; }
    public float[] getCentre() { return centre; }
    public List<float[]>[] getWallData() { return doorsAndWindows; }
    public string[] getMaterials() { return materials; }
}
