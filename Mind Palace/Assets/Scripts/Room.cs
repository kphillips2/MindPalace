using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Class for storing information about a room

[System.Serializable]
public class Room {

    private int[] roomDoors = new int[12]; // Must be length 12
    private float[] centre = new float[3]; // Must be length 3
    private string[] materials = new string[3]; // Must be length 3 (floor material, roof material, wall material)

    // Constructors
    public Room() {
    }

    public Room(int[] roomDoors, float[] centre, string[] materials){
        if (roomDoors.Length != 12) Debug.Log("Error: roomDoors array length must be 12");
        else Array.Copy(roomDoors, this.roomDoors, roomDoors.Length);

        if (centre.Length != 3) Debug.Log("Error: centre array length must be 3");
        else Array.Copy(centre, this.centre, centre.Length);

        if (materials.Length != 3) Debug.Log("Error: materials array length must be 3");
        else Array.Copy(materials, this.materials, materials.Length);
    }

    public Room(int[] roomDoors, Vector3 centre, string[] materials)
    {
        if (roomDoors.Length != 12) Debug.Log("Error: roomDoors array length must be 12");
        else Array.Copy(roomDoors, this.roomDoors, roomDoors.Length);

        this.centre = new float[] { centre.x, centre.y, centre.z};

        if (materials.Length != 3) Debug.Log("Error: materials array length must be 3");
        else Array.Copy(materials, this.materials, materials.Length);
    }

    // Getters
    public int[] getRoomDoors()
    {
        int[] nRoomDoors = new int[roomDoors.Length];
        Array.Copy(roomDoors, nRoomDoors, roomDoors.Length);
        return nRoomDoors;
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

    // Setters
    public void setMaterials(string[] materials)
    {
        if (materials.Length != 3) Debug.Log("Error: materials array length must be 3");
        else Array.Copy(materials, this.materials, materials.Length);
    }

    public void setRoomDoor(int index, int value)
    {
        if (index >= roomDoors.Length) Debug.Log("Error: Index is too large for roomDoors attribute");
        else roomDoors[index] = value;
    }
}