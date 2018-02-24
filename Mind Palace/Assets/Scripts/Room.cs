﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for storing information about a room

[System.Serializable]
public class Room {

    private int[] roomDoors; // Must be length 12
    private float[] centre; // Must be length 3
    private string[] materials; // Must be length 3 (floor material, roof material, wall material)

    // Constructors
    public Room() {
    }

    public Room(int[] roomDoors, float[] centre, string[] materials){
        if (roomDoors.Length != 12) Debug.Log("Error: roomDoors array length must be 12");
        else this.roomDoors = roomDoors;

        if (centre.Length != 3) Debug.Log("Error: centre array length must be 3");
        else this.centre = centre;

        if (materials.Length != 3) Debug.Log("Error: materials array length must be 3");
        else this.materials = materials;
    }

    public Room(int[] roomDoors, Vector3 centre, string[] materials)
    {
        if (roomDoors.Length != 12) Debug.Log("Error: roomDoors array length must be 12");
        else this.roomDoors = roomDoors;

        this.centre = new float[] { centre.x, centre.y, centre.z};

        if (materials.Length != 3) Debug.Log("Error: materials array length must be 3");
        else this.materials = materials;
    }

    // Getters
    public int[] getRoomDoors() { return roomDoors; }
    public float[] getCentre() { return centre; }
    public string[] getMaterials() { return materials; }

    // Setters
    public void setRoomDoors(int[] roomDoors)
    {
        if (roomDoors.Length != 12) Debug.Log("Error: roomDoors array length must be 12");
        else this.roomDoors = roomDoors;
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

    public void setRoomDoor(int index, int value)
    {
        if (index >= roomDoors.Length) Debug.Log("Error: Index is too large for roomDoors attribute");
        else roomDoors[index] = value;
    }
}