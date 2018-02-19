using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for storing information about a corridor

[System.Serializable]
public class Corridor {

    private int[] corrDoors; // Must be length 8
    private float[] centre; // Must be length 3
    private string[] materials; // Must be length 3 (floor material, roof material, wall material)
    private int angle; 

    // Constructors
    public Corridor() {

    }

    public Corridor(int[] corrDoors, float[] centre, string[] materials, int angle) {
        if (corrDoors.Length != 8) Debug.Log("Error: corrDoors array length must be 8");
        else this.corrDoors = corrDoors;

        if (centre.Length != 3) Debug.Log("Error: centre array length must be 3");
        else this.centre = centre;

        if (materials.Length != 3) Debug.Log("Error: materials array length must be 3");
        else this.materials = materials;

        this.angle = angle;
    }

    public Corridor(int[] corrDoors, Vector3 centre, string[] materials, int angle)
    {
        if (corrDoors.Length != 8) Debug.Log("Error: corrDoors array length must be 8");
        else this.corrDoors = corrDoors;

        this.centre = new float[] { centre.x, centre.y, centre.z };

        if (materials.Length != 3) Debug.Log("Error: materials array length must be 3");
        else this.materials = materials;

        this.angle = angle;
    }

    // Getters
    public int[] getCorrDoors() { return corrDoors; }
    public float[] getCentre() { return centre; }
    public string[] getMaterials() { return materials; }
    public int getAngle() { return angle; }

    // Setters
    public void setCorrDoors(int[] corrDoors)
    {
        if (corrDoors.Length != 8) Debug.Log("Error: corrDoors array length must be 8");
        else this.corrDoors = corrDoors;
    }

    public void setCentre(float[] centre)
    {
        if (centre.Length != 3) Debug.Log("Error: centre array length must be 3");
        else this.centre = centre;
    }

    public void setMaterials(string[] materials)
    {
        if (materials.Length != 3) Debug.Log("Error: materials array length must be 3");
        else this.materials = materials;
    }

    public void setAngle(int angle)
    {
        this.angle = angle;
    }
}