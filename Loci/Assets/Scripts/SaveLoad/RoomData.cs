using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Holds all the relevant room information to be saved.
/// </summary>
[System.Serializable]
public class RoomData {
    private float width;
    private float length;
    private float[] centre = new float [3];
    private List<float[]>[] doorsAndWindows = new List<float[]> [4];
    private List<PlusData> plusSigns = new List<PlusData> ();
    private string[] materials = new string [3];
    private List<Picture> pictures;

    /// <summary>
    /// Contructs a save object for a room at the given centre.
    /// </summary>
    /// <param name="loc"> a float array for the centre vector of the room </param>
    public RoomData(float[] loc){
        if (loc.Length != 3)
            Debug.Log ("Error: centre array length must be 3");
        else
            Array.Copy (loc, centre, 3);
        pictures = new List<Picture> ();
    }
    /// <summary>
    /// Sets the stored length and width of the room.
    /// </summary>
    /// <param name="wid"> the width of the room along the X axis </param>
    /// <param name="len"> the length of the room along the Z axis </param>
    public void SetRoomSize(float wid, float len){
        width = wid;
        length = len;
    }
    /// <summary>
    /// Sets the stored material names for the room.
    /// </summary>
    /// <param name="floor"> the material name for the floor </param>
    /// <param name="roof"> the material name for the roof </param>
    /// <param name="wall"> the material name for the walls </param>
    public void SetMaterials(string floor, string roof, string wall){
        materials [0] = floor;
        materials [1] = roof;
        materials [2] = wall;
    }
    /// <summary>
    /// Sets the stored door and window locations.
    /// </summary>
    /// <param name="wallData"> a list per wall and a float array for each door or window vector </param>
    public void SetWallData(List<float[]>[] wallData){
        for (int k = 0; k < 4; k++) {
            doorsAndWindows [k] = new List<float[]> ();
            foreach (float[] loc in wallData [k])
                doorsAndWindows [k].Add (loc);
        }
    }
    /// <summary>
    /// Adds a given plus sign to the room.
    /// </summary>
    /// <param name="plusData"> the plus sign object being added </param>
    public void AddPlusSign(PlusData plusData) { plusSigns.Add (plusData); }
    /// <summary>
    /// Adds a given picture to the room.
    /// </summary>
    /// <param name="picture"> the picture object being added </param>
    public void AddPicture(Picture picture) { pictures.Add(picture); }
    /// <summary>
    /// Retrieves the stored centre of the room.
    /// </summary>
    /// <returns> a float array representing the room centre vector </returns>
    public float[] GetCentre() { return centre; }
    /// <summary>
    /// Retrieves the stored width of the room.
    /// </summary>
    /// <returns> the width of the room along the X axis </returns>
    public float GetWidth() { return width; }
    /// <summary>
    /// Retrieves the stored length of the room.
    /// </summary>
    /// <returns> the width of the room along the Z axis </returns>
    public float GetLength() { return length; }
    /// <summary>
    /// Retrieves the stored door and window locations.
    /// </summary>
    /// <returns> a string array of material names for the floor, roof and wall objects </returns>
    public string[] GetMaterials() { return materials; }
    /// <summary>
    /// Retrieves the stored door and window locations.
    /// </summary>
    /// <returns> a list per wall and a float array for each door or window vector </returns>
    public List<float[]>[] GetWallData() { return doorsAndWindows; }
    /// <summary>
    /// Retrieves the plus signs attached to the room.
    /// </summary>
    /// <returns> the stored plus sign information </returns>
    public List<PlusData> GetPlusData() { return plusSigns; }
    /// <summary>
    /// Retrieves the pictures attached to the room.
    /// </summary>
    /// <returns> the stored picture information </returns>
    public List<Picture> GetPictures() { return pictures; }
    /// <summary>
    /// Removes all doors and windows currently stored in the room.
    /// </summary>
    public void ClearWallData(){
        for (int k = 0; k < 4; k++)
            doorsAndWindows [k] = new List<float[]> ();
    }
    /// <summary>
    /// Removes all plus signs currently stored in the room.
    /// </summary>
    public void ClearPlusSigns() { plusSigns = new List<PlusData> (); }
    /// <summary>
    /// Removes all pictures currently stored in the room.
    /// </summary>
    public void ClearPictures() { pictures = new List<Picture> (); }
}
