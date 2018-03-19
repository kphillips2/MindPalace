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

    //Checks if a door can be built on room with the given centre and where the door corresponds
    //to the given index. Currently returns null if door can't be built, float array containing
    //the centre of the corridor or room that it would be a door to if it can be built
    public static float[] canRoomDoorBePlaced(Vector3 centre, int doorIndex)
    {
        if (doorIndex < 0 || doorIndex > 11) return null; //Invalid index
        Vector3 door = getRoomDoorCoordinates(centre, doorIndex);
        return canDoorBePlaced(centre, door);
    }

    //Checks if a door can be built on corridor with the given centre/angle and where the door corresponds
    //to the given index. Currently returns null if door can't be built, float array containing
    //the centre of the corridor or room that it would be a door to if it can be built
    public static float[] canCorridorDoorBePlaced(Vector3 centre, float angle, int doorIndex)
    {
        if (doorIndex < 0 || doorIndex > 13) return null; //Invalid index
        Vector3 door = getCorridorDoorCoordinates(centre, angle, doorIndex);
        return canDoorBePlaced(centre, door);
    }

    //Checks if a door can be built on room/corridor with the given centre and where the door would be at
    //the given location. Currently returns null if door can't be built.
    //If can be built returns a float array containing:
    //the centre of the corridor or room that it would be a door to, the index of the door on this room/corridor,
    //and whether it is a room or corridor (0 = room, 1 = corridor)
    public static float[] canDoorBePlaced(Vector3 centre, Vector3 door)
    {
        //Look for rooms that touch the door
        foreach (Room r in SaveLoad.currentLoci.getRooms())
        {
            float[] rCentre = r.getCentre();
            //Skip if you reach the room whose door is being looked at
            if (centre.x == rCentre[0] && centre.z == rCentre[2]) continue;

            //If this is true, there is a wall next to the door space
            if ((Math.Abs(door.x - rCentre[0]) <= 4 && Math.Abs(door.z - rCentre[2]) <= 6) ||
                (Math.Abs(door.x - rCentre[0]) <= 6 && Math.Abs(door.z - rCentre[2]) <= 4))
            {
                //If a picture is placed on the other side of the wall, don't place door
                foreach(Picture p in SaveLoad.currentLoci.getPictures())
                {
                    float[] pLoc = p.getLocation();
                    if (Math.Abs(door.x - pLoc[0]) <= 0.5 && Math.Abs(door.z - pLoc[2]) <= 0.5) return null;
                }
                return new float[] {rCentre[0], rCentre[1], rCentre[2],
                    getRoomDoorIndex(new Vector3(rCentre[0], rCentre[1], rCentre[2]), door), 0 };
            }
        }

        //If no rooms were found, look for corridors that touch the door
        foreach(Corridor c in SaveLoad.currentLoci.getCorridors())
        {
            float[] cCentre = c.getCentre();
            float cAngle = c.getAngle();

            //Skip if you reach the room whose door is being looked at
            if (centre.x == cCentre[0] && centre.z == cCentre[2]) continue;

            //Corridor is long in x-direction
            if (cAngle % 180 == 0)
            {
                if ((Math.Abs(door.x - cCentre[0]) <= 10 && Math.Abs(door.z - cCentre[2]) <= 2) ||
                    (door.z == cCentre[2] && Math.Abs(door.x - cCentre[0]) <= 12))
                {
                    //If a picture is placed on the other side of the wall, don't place door
                    foreach (Picture p in SaveLoad.currentLoci.getPictures())
                    {
                        float[] pLoc = p.getLocation();
                        if (Math.Abs(door.x - pLoc[0]) <= 0.5 && Math.Abs(door.z - pLoc[2]) <= 0.5) return null;
                    }
                    return new float[] {cCentre[0], cCentre[1], cCentre[2],
                    getCorridorDoorIndex(new Vector3(cCentre[0], cCentre[1], cCentre[2]), door, cAngle), 1 };
                }
            }
            //Corridor is long in z-direction
            else
            {
                if ((Math.Abs(door.x - cCentre[0]) <= 2 && Math.Abs(door.z - cCentre[2]) <= 10) ||
                    (door.x == cCentre[0] && Math.Abs(door.z - cCentre[2]) <= 12))
                {
                    //If a picture is placed on the other side of the wall, don't place door
                    foreach (Picture p in SaveLoad.currentLoci.getPictures())
                    {
                        float[] pLoc = p.getLocation();
                        if (Math.Abs(door.x - pLoc[0]) <= 0.5 && Math.Abs(door.z - pLoc[2]) <= 0.5) return null;
                    }
                    return new float[] {cCentre[0], cCentre[1], cCentre[2],
                    getCorridorDoorIndex(new Vector3(cCentre[0], cCentre[1], cCentre[2]), door, cAngle), 1 };
                }
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

    //Takes the center/angle of a corridor and the index of a door. Returns a vector containing
    //the door's coordinates. doorIndex should be between 0 - 11
    private static Vector3 getCorridorDoorCoordinates(Vector3 centre, float angle, int doorIndex)
    {
        //Long on x-axis
        if(angle % 180 == 0)
        {
            switch (doorIndex)
            {
                case 0: return new Vector3(centre.x + 2, centre.y, centre.z + 2);
                case 1: return new Vector3(centre.x + 6, centre.y, centre.z + 2);
                case 2: return new Vector3(centre.x + 10, centre.y, centre.z + 2);
                case 3: return new Vector3(centre.x + 12, centre.y, centre.z);
                case 4: return new Vector3(centre.x + 10, centre.y, centre.z - 2);
                case 5: return new Vector3(centre.x + 6, centre.y, centre.z - 2);
                case 6: return new Vector3(centre.x + 2, centre.y, centre.z - 2);
                case 7: return new Vector3(centre.x - 2, centre.y, centre.z - 2);
                case 8: return new Vector3(centre.x - 6, centre.y, centre.z - 2);
                case 9: return new Vector3(centre.x - 10, centre.y, centre.z - 2);
                case 10: return new Vector3(centre.x - 12, centre.y, centre.z);
                case 11: return new Vector3(centre.x - 10, centre.y, centre.z + 2);
                case 12: return new Vector3(centre.x - 6, centre.y, centre.z + 2);
                default: return new Vector3(centre.x - 2, centre.y, centre.z + 2);
            }
        }
        //Long on z-axis
        switch (doorIndex)
        {
            case 0: return new Vector3(centre.x, centre.y, centre.z + 12); //x = 0, pos z
            case 1: return new Vector3(centre.x + 2, centre.y, centre.z + 10);
            case 2: return new Vector3(centre.x + 2, centre.y, centre.z + 6);
            case 3: return new Vector3(centre.x + 2, centre.y, centre.z + 2);
            case 4: return new Vector3(centre.x + 2, centre.y, centre.z - 2);
            case 5: return new Vector3(centre.x + 2, centre.y, centre.z - 6);
            case 6: return new Vector3(centre.x + 2, centre.y, centre.z - 10);
            case 7: return new Vector3(centre.x, centre.y, centre.z - 12);
            case 8: return new Vector3(centre.x - 2, centre.y, centre.z - 10);
            case 9: return new Vector3(centre.x - 2, centre.y, centre.z - 6);
            case 10: return new Vector3(centre.x - 2, centre.y, centre.z - 2);
            case 11: return new Vector3(centre.x - 2, centre.y, centre.z + 2);
            case 12: return new Vector3(centre.x - 2, centre.y, centre.z + 6);
            default: return new Vector3(centre.x - 2, centre.y, centre.z + 10);
        }
    }

    //Takes the center of a room and the location of the door. Returns the
    //index of the door for the room
    private static int getRoomDoorIndex(Vector3 centre, Vector3 door)
    { 
        if (door.x == centre.x)
        {
            if (door.z == centre.z + 6) return 0;
            else return 6;
        }
        else if (door.x == centre.x + 4)
        {
            if (door.z == centre.z + 6) return 1;
            else return 5;
        }
        else if (door.x == centre.x + 6)
        {
            if (door.z == centre.z + 4) return 2;
            else if (door.z == centre.z) return 3;
            else return 4;
        }
        else if(door.x == centre.x - 4)
        {
            if (door.z == centre.z - 6) return 7;
            else return 11;
        }
        else
        {
            if (door.z == centre.z - 4) return 8;
            else if (door.z == centre.z) return 9;
            else return 10;
        }
    }

    //Takes the center/angle of a corridor and the location of the door. Returns the
    //index of the door for the corridor
    private static int getCorridorDoorIndex(Vector3 centre, Vector3 door, float angle)
    {
        //Long on x-axis
        if (angle % 180 == 0)
        {
            if (door.z == centre.z)
            {
                if (door.x == centre.x + 12) return 3;
                else return 10;
            }
            else if (door.z == centre.z + 2)
            {
                if (door.x == centre.x + 2) return 0;
                else if (door.x == centre.x + 6) return 1;
                else if (door.x == centre.x + 10) return 2;
                else if (door.x == centre.x - 10) return 11;
                else if (door.x == centre.x - 6) return 12;
                else return 13;
            }
            else
            {
                if (door.x == centre.x + 10) return 4;
                else if (door.x == centre.x + 6) return 5;
                else if (door.x == centre.x + 2) return 6;
                else if (door.x == centre.x - 2) return 7;
                else if (door.x == centre.x - 6) return 8;
                else return 9;
            }
        }
        //Long on z-axis
        else
        {
            if (door.x == centre.x)
            {
                if (door.z == centre.z + 12) return 0;
                else return 7;
            }
            else if (door.x == centre.x + 2)
            {
                if (door.z == centre.z + 2) return 3;
                else if (door.z == centre.z + 6) return 2;
                else if (door.z == centre.z + 10) return 1;
                else if (door.z == centre.z - 10) return 6;
                else if (door.z == centre.z - 6) return 5;
                else return 4;
            }
            else
            {
                if (door.z == centre.z + 10) return 13;
                else if (door.z == centre.z + 6) return 12;
                else if (door.z == centre.z + 2) return 11;
                else if (door.z == centre.z - 2) return 10;
                else if (door.z == centre.z - 6) return 9;
                else return 8;
            }
        }
    }
}
