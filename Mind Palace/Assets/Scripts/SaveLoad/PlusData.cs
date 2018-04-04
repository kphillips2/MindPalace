using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Holds all the relevant plus sign information to be saved.
/// </summary>
[System.Serializable]
public class PlusData : IComparable<PlusData> {
    private float angle;
    private float[] centre = new float[3];
    private float[] newRoom = new float[3];
    private float[] newCorridor = new float[3];

    /// <summary>
    /// Constructs a save object for a plus sign at the given centre and angle.
    /// </summary>
    /// <param name="loc"> a float array for the centre vector of the plus sign </param>
    /// <param name="dir"> an angle that determines which wall to display the plus sign on </param>
    public PlusData(float[] loc, float[] roomLoc, float[] corridorLoc, float dir){
        if (loc.Length != 3)
            Debug.Log("Error: plus sign centre array length must be 3");
        else
            Array.Copy(loc, centre, 3);

        if (loc.Length != 3)
            Debug.Log("Error: new room centre array length must be 3");
        else
            Array.Copy(roomLoc, newRoom, 3);

        if (loc.Length != 3)
            Debug.Log("Error: new corridor centre array length must be 3");
        else
            Array.Copy(corridorLoc, newCorridor, 3);

        angle = dir;
    }
    /// <summary>
    /// Retrieves the stored angle of the plus sign.
    /// </summary>
    /// <returns> the angle of the plus sign </returns>
    public float GetAngle() { return angle; }
    /// <summary>
    /// Retrieves the stored centre of the plus sign.
    /// </summary>
    /// <returns> a float array representing the plus sign centre vector </returns>
    public float[] GetCentre() { return centre; }
    /// <summary>
    /// Retrieves the stored centre of a potential new room.
    /// </summary>
    /// <returns> a float array representing a new room centre vector </returns>
    public float[] GetNewRoom() { return newRoom; }
    /// <summary>
    /// Retrieves the stored centre of a potential new corridor.
    /// </summary>
    /// <returns> a float array representing a new corridor centre vector </returns>
    public float[] GetNewCorridor() { return newCorridor; }
    /// <summary>
    /// Determines if two plus signs are the same object.
    /// </summary>
    /// <param name="other"> the plus sign compared to this one </param>
    /// <returns> 0 if they are equal and negative if this precedes other </returns>
    public int CompareTo(PlusData other){
        float dir = other.GetAngle ();
        float[] chk = other.GetCentre ();
        float ans = (centre[1] != chk[1]) ? centre[1] - chk[1] :
            (angle != dir) ? angle - dir :
            (centre[0] != chk[0]) ? centre[0] - chk[1] : 0;
        return (int)ans;
    }

    public int GetWallIndex()
    {
        switch ((int) angle)
        {
            case 0: return 0;
            case 90: return 1;
            case 180: return 2;
            default: return 3;
        }
    }
}
