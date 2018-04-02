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

    /// <summary>
    /// Constructs a save object for a plus sign at the given centre and angle.
    /// </summary>
    /// <param name="loc"> a float array for the centre vector of the room </param>
    /// <param name="dir"> an angle that determines which wall to display the plus sign over </param>
    public PlusData(float[] loc, float dir){
        if (loc.Length != 3)
            Debug.Log("Error: centre array length must be 3");
        else
            Array.Copy(loc, centre, 3);
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
    /// Determines if two plus signs are the same object.
    /// </summary>
    /// <param name="other"> the plus sign compared to this one </param>
    /// <returns> 0 if they are equal and negative if this precedes other </returns>
    public int CompareTo(PlusData other){
        float dir = other.GetAngle ();
        float[] chk = other.GetCentre ();
        float ans = (angle != dir) ? angle - dir :
            (centre[1] != chk[1]) ? centre[1] - chk[1] :
            (centre[0] != chk[0]) ? centre[0] - chk[1] : 0;
        return (int)ans;
    }
}
