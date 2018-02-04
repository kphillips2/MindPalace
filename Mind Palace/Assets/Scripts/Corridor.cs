using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Corridor {

    public int[] corrDoors;
    public float[] centre;
    public string[] materials;
    public int angle;

    public Corridor() {

    }

    public Corridor(int[] corrDoors, float[] centre, string[] materials, int angle) {
        this.corrDoors = corrDoors;
        this.centre = centre;
        this.materials = materials;
        this.angle = angle;
    }
}