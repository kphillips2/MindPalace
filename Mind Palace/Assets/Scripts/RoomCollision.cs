using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Class containing functions for checking whether or not a room, corridor, or door can be placed at a certain
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
            if (centre.x < rCentre[0] + 12 && centre.x > rCentre[0] - 12 &&
                centre.z < rCentre[2] + 12 && centre.z > rCentre[2] - 12) return false;
        }
        // Check that room does not collide with any existing corridors
        foreach (Corridor c in SaveLoad.currentLoci.getCorridors())
        {
            float[] cCentre = c.getCentre();
            float cAngle = c.getAngle();

            // Case that corridor is long in the x-direction, short in z-direction
            if(cAngle % 180 == 0)
            {
                if (centre.x < cCentre[0] + 18 && centre.x > cCentre[0] - 18 &&
                centre.z < cCentre[2] + 8 && centre.z > cCentre[2] - 8) return false;
            }
            // Case that corridor is long in the z-direction, short in x-direction
            else
            {
                if (centre.x < cCentre[0] + 8 && centre.x > cCentre[0] - 8 &&
                centre.z < cCentre[2] + 18 && centre.z > cCentre[2] - 18) return false;
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
                if (centre.x < rCentre[0] + 18 && centre.x > rCentre[0] - 18 &&
                centre.z < rCentre[2] + 8 && centre.z > rCentre[2] - 8) return false;
            }
            // Corridor is long in z-direction
            else
            {
                if (centre.x < rCentre[0] + 8 && centre.x > rCentre[0] - 8 &&
                centre.z < rCentre[2] + 18 && centre.z > rCentre[2] - 18) return false;
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
                if (centre.x < cCentre[0] + 24 && centre.x > cCentre[0] - 24 &&
                centre.z < cCentre[2] + 4 && centre.z > cCentre[2] - 4) return false;
            }
            //Both corridors long in z-direction
            else if(angle % 180 != 0 && cAngle % 180 != 0)
            {
                if (centre.x < cCentre[0] + 4 && centre.x > cCentre[0] - 4 &&
                centre.z < cCentre[2] + 24 && centre.z > cCentre[2] - 24) return false;
            }
            //One corridor long in x-direction, other long in z-direction
            else
            {
                if (centre.x < cCentre[0] + 14 && centre.x > cCentre[0] - 14 &&
                centre.z < cCentre[2] + 14 && centre.z > cCentre[2] - 14) return false;
            }
        }

        return true;
    }

    //Checks if a door can be built on room with the given centre and where the door would be at
    //the given doorIndex. Currently returns null if door can't be built, float array containing
    //the centre of the corridor or room that it would be a door to if it can be built
    public static float[] canRoomDoorBePlaced(Vector3 centre, int doorIndex)
    {
        if (doorIndex < 0 || doorIndex > 11) return null; //Invalid index

        Vector3 door = getRoomDoorCoordinates(centre, doorIndex);

        //Look for rooms that touch the door
        foreach (Room r in SaveLoad.currentLoci.getRooms())
        {
            float[] rCentre = r.getCentre();
            //Skip if you reach the room whose door is being looked at
            if (door.x == rCentre[0] && door.z == rCentre[2]) continue;

            if ((Math.Abs(door.x - rCentre[0]) <= 4 && Math.Abs(door.z - rCentre[2]) <= 6) ||
                (Math.Abs(door.x - rCentre[0]) <= 6 && Math.Abs(door.z - rCentre[2]) <= 4))
                return rCentre;
        }

        //If no rooms were found, look for corridors that touch the door
        foreach(Corridor c in SaveLoad.currentLoci.getCorridors())
        {
            float[] cCentre = c.getCentre();
            float cAngle = c.getAngle();

            //Corridor is long in x-direction
            if (cAngle % 180 == 0)
            {
                if ((Math.Abs(door.x - cCentre[0]) <= 10 && Math.Abs(door.z - cCentre[2]) <= 2) ||
                    (door.z == cCentre[2] && Math.Abs(door.x - cCentre[0]) <= 12))
                    return cCentre;
            }
            //Corridor is long in z-direction
            else
            {
                if ((Math.Abs(door.x - cCentre[0]) <= 2 && Math.Abs(door.z - cCentre[2]) <= 10) ||
                    (door.x == cCentre[0] && Math.Abs(door.z - cCentre[2]) <= 12))
                    return cCentre;
            }
        }

        //Nothing touches the door, door can't be made
        return null;
    }

    //Takes the center of a room and the index of a door. Returns a vector containing
    //the door's coordinates. doorIndex should be between 0 - 11
    private static Vector3 getRoomDoorCoordinates(Vector3 centre, int doorIndex)
    {
        switch (doorIndex)
        {
            case 0: return new Vector3(centre.x, centre.y, centre.z + 6); //x = 0, pos z
            case 1: return new Vector3(centre.x + 4, centre.y, centre.z + 6);
            case 2: return new Vector3(centre.x + 6, centre.y, centre.z + 4);
            case 3: return new Vector3(centre.x + 6, centre.y, centre.z);
            case 4: return new Vector3(centre.x + 6, centre.y, centre.z - 4);
            case 5: return new Vector3(centre.x + 4, centre.y, centre.z - 6);
            case 6: return new Vector3(centre.x, centre.y, centre.z - 6);
            case 7: return new Vector3(centre.x - 4, centre.y, centre.z - 6);
            case 8: return new Vector3(centre.x - 6, centre.y, centre.z - 4);
            case 9: return new Vector3(centre.x - 6, centre.y, centre.z);
            case 10: return new Vector3(centre.x - 6, centre.y, centre.z + 4);
            default: return new Vector3(centre.x - 4, centre.y, centre.z + 6);
        }
    }
}
