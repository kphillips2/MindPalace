using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for storing information about a single palace

[System.Serializable]
public class Loci
{

    private string name; // Name that the user has given the Loci
    private List<float[]> objects; // Each float array must be length 4 (obj number, xcor, zcor, y rotation)
    private List<Room> rooms;
    private List<Corridor> corridors;
    private List<Picture> pictures;

    private struct RoomData
    {
        float width;
        float length;
        List<float[]>[] doors;
    }

    // Constructor, all lists initialized to be empty
    public Loci(string name)
    {
        this.name = name;
        objects = new List<float[]>();
        rooms = new List<Room>();
        corridors = new List<Corridor>();
        pictures = new List<Picture>();
    }

    // Getters
    public string getName() { return name; }
    public List<float[]> getObjects() { return new List<float[]>(objects); }
    public List<Room> getRooms() { return new List<Room>(rooms); }
    public List<Corridor> getCorridors() { return new List<Corridor>(corridors); }
    public List<Picture> getPictures() { return new List<Picture>(pictures); }

    // Set name
    public void setName(string name) { this.name = name; }

    // Add to Lists
    public void addObject(float[] obj)
    {
        if (obj.Length != 7) Debug.Log("Error: Added objects must be represented by an array of length 7");
        else objects.Add(obj);
    }
    public void addRoom(Room room) { rooms.Add(room); }
    public void addCorridor(Corridor corridor) { corridors.Add(corridor); }
    public void addPicture(Picture picture) { pictures.Add(picture); }

    // Clear list
    public void clearObjects() { objects = new List<float[]>(); }
    public void clearRooms() { rooms = new List<Room>(); }
    public void clearCorridors() { corridors = new List<Corridor>(); }
    public void clearPictures() { pictures = new List<Picture>(); }

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