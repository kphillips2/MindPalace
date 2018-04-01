using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for storing information about a single palace

[System.Serializable]
public class Loci {
    private List<float[]> objects; // Each float array must be length 4 (obj number, xcor, zcor, y rotation)
    private List<RoomData> rooms;

    // Constructor, all lists initialized to be empty
    public Loci(){
        objects = new List<float[]> ();
        rooms = new List<RoomData> ();
    }

    // Getters
    public List<float[]> getObjects() { return objects; }
    public List<RoomData> getRooms() { return rooms; }

    // Add to Lists
    public void addObject(float[] obj){
        if (obj.Length != 7)
            Debug.Log ("Error: Added objects must be represented by an array of length 7");
        else
            objects.Add (obj);
    }
    public void addRoom(RoomData room) { rooms.Add (room); }

    // Clear list
    public void clearObjects() { objects = new List<float[]> (); }
    public void clearRooms() { rooms = new List<RoomData> (); }
}