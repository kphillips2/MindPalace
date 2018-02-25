using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Class for storing information about a corridor

[System.Serializable]
public class Corridor {

    private int[] corrDoors = new int[8]; // Must be length 8
    private float[] centre = new float[3]; // Must be length 3
    private string[] materials = new string[3]; // Must be length 3 (floor material, roof material, wall material)
    private int angle; 

    // Constructors
    public Corridor() {

    }

    public Corridor(int[] corrDoors, float[] centre, string[] materials, int angle) {
        if (corrDoors.Length != 8) Debug.Log("Error: corrDoors array length must be 8");
        else Array.Copy(corrDoors, this.corrDoors, corrDoors.Length);

        if (centre.Length != 3) Debug.Log("Error: centre array length must be 3");
        else Array.Copy(centre, this.centre, centre.Length);

        if (materials.Length != 3) Debug.Log("Error: materials array length must be 3");
        else Array.Copy(materials, this.materials, materials.Length);

        this.angle = angle;
    }

    public Corridor(int[] corrDoors, Vector3 centre, string[] materials, int angle)
    {
        if (corrDoors.Length != 8) Debug.Log("Error: corrDoors array length must be 8");
        else Array.Copy(corrDoors, this.corrDoors, corrDoors.Length);

        this.centre = new float[] { centre.x, centre.y, centre.z };

        if (materials.Length != 3) Debug.Log("Error: materials array length must be 3");
        else Array.Copy(materials, this.materials, materials.Length);

        this.angle = angle;
    }

    // Getters
    public int[] getCorrDoors()
    {
        int[] nCorrDoors = new int[corrDoors.Length];
        Array.Copy(corrDoors, nCorrDoors, corrDoors.Length);
        return nCorrDoors;
    }
    public float[] getCentre()
    {
        float[] nCentre = new float[centre.Length];
        Array.Copy(centre, nCentre, centre.Length);
        return nCentre;
    }
    public string[] getMaterials()
    {
        string[] nMaterials = new string[materials.Length];
        Array.Copy(materials, nMaterials, materials.Length);
        return nMaterials;
    }
    public int getAngle() { return angle; }

    // Setters
    public void setMaterials(string[] materials)
    {
        if (materials.Length != 3) Debug.Log("Error: materials array length must be 3");
        else Array.Copy(materials, this.materials, materials.Length);
    }

    public void setCorridorDoor(int index, int value)
    {
        if (index >= corrDoors.Length) Debug.Log("Error: Index is too large for corrDoors attribute");
        else corrDoors[index] = value;
    }
}