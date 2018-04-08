using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all the relevant room type information.
/// </summary>
public static class RoomTypes {
    /// <summary>
    /// Contains all the possible dimensions of different room sizes.
    /// </summary>
    /// <param name="type"> determines which room dimensions to use </param>
    /// <returns> a float array of width and length </returns>
    public static float[] GetDimensions(string type){
        switch (type) {
            case "room":
                return new float[] { 12, 12 };
            case "xCorridor":
                return new float[] { 24, 4 };
            case "zCorridor":
                return new float[] { 4, 24 };
            default:
                return null;
        }
    }
    /// <summary>
    /// Finds the centre of a potential new room based on its type.
    /// </summary>
    /// <param name="type"> determines which room dimensions to use for the new room </param>
    /// <param name="centre"> the vector for the centre of the room holding the plus sign </param>
    /// <param name="loc"> the vector for the centre of the plus sign </param>
    /// <param name="newType"> determines which room dimensions to use for the existing room </param>
    /// <returns> the vector for the centre of the new room </returns>
    public static float[] GetNewRoomCentre(float width, float length, Vector3 centre, Vector3 loc, string type) {
        float[] dims = GetDimensions(type);
        float distX = width / 2, distZ = length / 2;
        float moveX = dims [0] / 2, moveZ = dims [1] / 2;

        float[] newLoc =
            // loc lies on positive X wall
            (loc.x > centre.x + distX - 0.5f) ? new float[] { centre.x + distX + moveX, centre.y, loc.z } :
            // loc lies on negative Z wall
            (loc.z < centre.z - distZ + 0.5f) ? new float[] { loc.x, centre.y, centre.z - distZ - moveZ } :
            // loc lies on negative X wall
            (loc.x < centre.x - distX + 0.5f) ? new float[] { centre.x - distX - moveX, centre.y, loc.z } :
            // loc must lie on positive Z wall
            new float[] { loc.x, centre.y, centre.z + distZ + moveZ };
        return newLoc;
    }
    /// <summary>
    /// Finds the correct centre of a potential new room based on its type.
    /// </summary>
    /// <param name="plus"> the plus sign adding the new room </param>
    /// <param name="type"> determines which room dimensions to use for the new room </param>
    /// <returns></returns>
    public static float[] GetNewRoomCentre(PlusData plus, string type){
        switch (type) {
            case "room":
                return plus.GetNewRoom ();
            case "xCorridor":
                return plus.GetNewCorridor ();
            case "zCorridor":
                return plus.GetNewCorridor ();
            default:
                return null;
        }
    }
    /// <summary>
    /// Finds the corridor type based on a given angle.
    /// </summary>
    /// <param name="angle"> the angle of the corridor </param>
    /// <returns> the corridor type </returns>
    public static string GetCorridorType(float angle){
        return (angle % 180 == 0) ? "zCorridor" : "xCorridor";
    }
}
