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

    // Update

    // Records that the door at doorIndex of the room with the given centre is now open
    public void openRoomDoorAt(Vector3 centre, int doorIndex)
    {
        foreach (Room r in rooms)
        {
            float[] rCentre = r.getCentre();
            if (rCentre[0] == centre.x && rCentre[1] == centre.y && rCentre[2] == centre.z)
            {
                r.setRoomDoor(doorIndex, 1);
                break;
            }
        }
    }

    // Records that the door at doorIndex of the corridor with the given centre is now open
    public void openCorridorDoorAt(Vector3 centre, int doorIndex)
    {
        foreach (Corridor c in corridors)
        {
            float[] cCentre = c.getCentre();
            if (cCentre[0] == centre.x && cCentre[1] == centre.y && cCentre[2] == centre.z)
            {
                c.setCorridorDoor(doorIndex, 1);
                break;
            }
        }
    }

    // Changes the materials used in the room at the given centre
    public void updateRoomMaterials(Vector3 centre, string[] materials)
    {
        foreach (Room r in rooms)
        {
            float[] rCentre = r.getCentre();
            if (rCentre[0] == centre.x && rCentre[1] == centre.y && rCentre[2] == centre.z)
            {
                r.setMaterials(materials);
                break;
            }
        }
    }

    // Changes the materials used in the corridor at the given centre
    public void updateCorridorMaterials(Vector3 centre, string[] materials)
    {
        foreach (Corridor c in corridors)
        {
            float[] cCentre = c.getCentre();
            if (cCentre[0] == centre.x && cCentre[1] == centre.y && cCentre[2] == centre.z)
            {
                c.setMaterials(materials);
                break;
            }
        }
    }
}
