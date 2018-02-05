using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for storing information about a single palace

[System.Serializable]
public class Loci {

    private string name; // Name that the user has given the Loci
    private List<float[]> objects; // Each float array must be length 4 (obj number, xcor, zcor, y rotation)
    private List<Room> rooms;
    private List<Corridor> corridors;

    // Constructor, all lists initialized to be empty
    public Loci (string name) {
        this.name = name;
        objects = new List<float[]>();
        rooms = new List<Room>();
        corridors = new List<Corridor>();
    }

    // Getters
    public string getName() { return name; }
    public List<float[]> getObjects() { return objects; }
    public List<Room> getRooms() { return rooms; }
    public List<Corridor> getCorridors() { return corridors; }

    // Set name or set full lists
    public void setName(string name) { this.name = name; }
    public void setObjects(List<float[]> objects) { this.objects = objects; }
    public void setRooms(List<Room> rooms) { this.rooms = rooms; }
    public void setCorridors(List<Corridor> corridors) { this.corridors = corridors; }

    // Add to Lists
    public void addObject(float[] obj)
    {
        if (obj.Length != 4) Debug.Log("Error: Added objects must be represented by an array of length 4");
        else objects.Add(obj);
    }
    public void addRoom(Room room) { rooms.Add(room); }
    public void addCorridor(Corridor corridor) { corridors.Add(corridor); }

    // Clear list
    public void clearObjects() { objects = new List<float[]>(); }
    public void clearRooms() { rooms = new List<Room>(); }
    public void clearCorridors() { corridors = new List<Corridor>(); }
}
