using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Room {

    public int[] roomDoors;
    public float[] centre;
    public string[] materials;

    public Room() {
    }

    public Room(int[] roomDoors, float[] centre, string[] materials){
        this.roomDoors = roomDoors;
        this.centre = centre;
        this.materials = materials;
    }
}