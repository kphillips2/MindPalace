using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class containing functions for checking whether or not a room or corridor can be placed at a certain
//location

public static class RoomCollision  {

    // Checks whether a room with the given centre can be built without colliding with another
    // room or corridor. If the room is okay to be placed, the function will return true. If
    // a room cannot be placed there, the function will return false.
    public static bool canRoomBePlaced (Vector3 centre)
    {
        // Check that room does not collide with any existing rooms
        foreach (Room r in SaveLoad.currentLoci.getRooms())
        {
            float[] rCentre = r.getCentre();
            if (centre.x < rCentre[0] + 10 && centre.x > rCentre[0] - 10 &&
                centre.z < rCentre[2] + 10 && centre.z > rCentre[2] - 10) return false;
        }
        // Check that room does not collide with any existing corridors
        foreach (Corridor c in SaveLoad.currentLoci.getCorridors())
        {
            float[] cCentre = c.getCentre();
            float cAngle = c.getAngle();

            // Case that corridor is long in the x-direction, short in z-direction
            if(cAngle % 180 == 0)
            {
                if (centre.x < cCentre[0] + 20 && centre.x > cCentre[0] - 20 &&
                centre.z < cCentre[2] + 7.5 && centre.z > cCentre[2] - 7.5) return false;
            }
            // Case that corridor is long in the z-direction, short in x-direction
            else
            {
                if (centre.x < cCentre[0] + 7.5 && centre.x > cCentre[0] - 7.5 &&
                centre.z < cCentre[2] + 20 && centre.z > cCentre[2] - 20) return false;
            }
        }

        return true;
    }

    // Checks whether a corridor with the given centre and angle can be built without colliding with another
    // room or corridor. If the corridor is okay to be placed, the function will return true. If
    // a corridor cannot be placed there, the function will return false.
    public static bool canCorridorBePlaced (Vector3 centre, float angle)
    {
        //Check that corridor does not collide with any existing rooms
        foreach (Room r in SaveLoad.currentLoci.getRooms())
        {
            float[] rCentre = r.getCentre();

            // Corridor is long in x-direction
            if (angle % 180 == 0)
            {
                if (centre.x < rCentre[0] + 20 && centre.x > rCentre[0] - 20 &&
                centre.z < rCentre[2] + 7.5 && centre.z > rCentre[2] - 7.5) return false;
            }
            // Corridor is long in z-direction
            else
            {
                if (centre.x < rCentre[0] + 7.5 && centre.x > rCentre[0] - 7.5 &&
                centre.z < rCentre[2] + 20 && centre.z > rCentre[2] - 20) return false;
            }
        }

        //Check that corridor does not collide with any existing corridors
        foreach (Corridor c in SaveLoad.currentLoci.getCorridors())
        {
            float[] cCentre = c.getCentre();
            float cAngle = c.getAngle();

            //Both corridors long in x-direction
            if (angle % 180 == 0 && cAngle % 180 == 0)
            {
                if (centre.x < cCentre[0] + 30 && centre.x > cCentre[0] - 30 &&
                centre.z < cCentre[2] + 5 && centre.z > cCentre[2] - 5) return false;
            }
            //Both corridors long in z-direction
            else if(angle % 180 != 0 && cAngle % 180 != 0)
            {
                if (centre.x < cCentre[0] + 5 && centre.x > cCentre[0] - 5 &&
                centre.z < cCentre[2] + 30 && centre.z > cCentre[2] - 30) return false;
            }
            //One corridor long in x-direction, other long in z-direction
            else
            {
                if (centre.x < cCentre[0] + 17.5 && centre.x > cCentre[0] - 17.5 &&
                centre.z < cCentre[2] + 17.5 && centre.z > cCentre[2] - 17.5) return false;
            }
        }

        return true;
    }
}
