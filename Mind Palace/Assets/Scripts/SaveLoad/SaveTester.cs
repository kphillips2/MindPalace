using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Temporary script for testing the save feature

public class SaveTester : MonoBehaviour {

    void Start() {
        //performSaveTest();
    }

    // Creates a test Loci and saves it, called "saveTest". Commenting-out the loadTest() line in the Start function of
    // LevelBuilder.cs and running this function will reset the save file so that it only contains this Loci.
    void performSaveTest () {
        SaveFile.currentLoci = new Loci("saveTest"); //Make a new Loci

        // Place objects in the scene which will be saved when the save() function is called
        ObjectPlacer op = new ObjectPlacer();
        op.createPrefab(0, new Vector3(-3.468538f, 0f, -12.47951f), new Vector3(0f, 0f, 0f), false); //Bed
        op.createPrefab(1, new Vector3(4.15f, 0f, 8.72f), new Vector3(0f, -90f, 0f), false); //Book cases
        op.createPrefab(1, new Vector3(4.15f, 0f, 10.43f), new Vector3(0f, -90f, 0f), false);
        op.createPrefab(1, new Vector3(-4.16f, 0f, -6.81f), new Vector3(0f, 90f, 0f), false);
        op.createPrefab(1, new Vector3(-4.44f, 0f, 7.66f), new Vector3(0f, 90f, 0f), false);
        op.createPrefab(2, new Vector3(2.7956f, 0f, -3.76479f), new Vector3(0f, 0f, 0f), false); //Chair
        op.createPrefab(3, new Vector3(19.07f, 0f, -7.08f), new Vector3(0f, 90f, 0f), false); //Chair2
        op.createPrefab(3, new Vector3(21.93f, 0f, -8.543f), new Vector3(0f, 270f, 0f), false);
        op.createPrefab(3, new Vector3(19.07f, 0f, -8.543f), new Vector3(0f, 90f, 0f), false);
        op.createPrefab(3, new Vector3(21.92f, 0f, -7.08f), new Vector3(0f, 270f, 0f), false);
        op.createPrefab(4, new Vector3(-1.18f, 0f, 12.39f), new Vector3(0f, 90f, 0f), false); //Coffee table
        op.createPrefab(5, new Vector3(-3.47f, 0f, 12.44f), new Vector3(0f, 90f, 0f), false); //Couches
        op.createPrefab(5, new Vector3(1f, 0f, 12.24f), new Vector3(0f, -90f, 0f), false);
        op.createPrefab(5, new Vector3(1.31f, 0f, -9.26f), new Vector3(0f, 90f, 0f), false);
        op.createPrefab(6, new Vector3(16f, 0f, -7.22f), new Vector3(0f, 90f, 0f), false); //Counters
        op.createPrefab(6, new Vector3(16f, 0f, -8.52f), new Vector3(0f, 90f, 0f), false);
        op.createPrefab(7, new Vector3(-4.09f, 0f, 0.05f), new Vector3(0f, 90f, 0f), false); //Fireplace
        op.createPrefab(8, new Vector3(4.0925f, 0f, -3.876518f), new Vector3(0f, 0f, 0f), false); //Flower tables
        op.createPrefab(8, new Vector3(-3.865167f, 0f, 3.955207f), new Vector3(0f, 0f, 0f), false);
        op.createPrefab(8, new Vector3(-3.643468f, 0f, -3.745927f), new Vector3(0f, 0f, 0f), false);
        op.createPrefab(8, new Vector3(4.133508f, 0f, 3.98855f), new Vector3(0f, 0f, 0f), false);
        op.createPrefab(8, new Vector3(34.03f, 0f, 10.99f), new Vector3(0f, 0f, 0f), false);
        op.createPrefab(8, new Vector3(33.83f, 0f, 3.35468f), new Vector3(0f, 0f, 0f), false);
        op.createPrefab(9, new Vector3(16.12f, 0f, -11.23f), new Vector3(0f, 90f, 0f), false); //Fridge
        op.createPrefab(10, new Vector3(0.3411262f, 0f, -14.19815f), new Vector3(0f, 0f, 0f), false); //Guitar
        op.createPrefab(11, new Vector3(3.07f, 0f, -14.07f), new Vector3(0f, 0f, 0f), false); //Headboard
        op.createPrefab(12, new Vector3(33.44f, 0f, -0.1f), new Vector3(0f, -90f, 0f), false); //Lion statue
        op.createPrefab(13, new Vector3(16.13f, 0f, -9.81f), new Vector3(0f, 90f, 0f), false); //Oven
        op.createPrefab(14, new Vector3(23.8f, 0f, -3.97f), new Vector3(0f, 0f, 0f), false); //Plants
        op.createPrefab(14, new Vector3(3.810411f, 0f, 13.44f), new Vector3(0f, 0f, 0f), false);
        op.createPrefab(14, new Vector3(3.810411f, 0f, 6.19f), new Vector3(0f, 0f, 0f), false);
        op.createPrefab(15, new Vector3(34.25f, 0f, 5.94f), new Vector3(0f, -90f, 0f), false); //Sink
        op.createPrefab(16, new Vector3(1.01692f, 0f, -12.85651f), new Vector3(0f, 0f, 0f), false); //Soccer ball
        op.createPrefab(17, new Vector3(20.4941f, 0f, -7.77f), new Vector3(0f, 90f, 0f), false); //Table
        op.createPrefab(18, new Vector3(34.13f, 0f, 7.88f), new Vector3(0f, -90f, 0f), false); //Toilet
        op.createPrefab(19, new Vector3(3.937f, 0f, -9.35f), new Vector3(0f, -90f, 0f), false); //TV

        // Add rooms and corridors to the current Loci
        /*int[] doors = new int[] { 1, 1, 1, 0 };
        float[] centre = new float[] { 0, 0, 0 };
        string[] materials = new string[] { "Wood Texture 06", "Wood Texture 15", "Wood texture 12" };
        RoomData r = new RoomData(doors, new Vector3(0, 0, 0), materials);
        SaveFile.currentLoci.addRoom(r);
        doors = new int[] { 0, 0, 1, 0 };
        centre = new float[] { 0, 0, 10 };
        r = new Room(doors, centre, materials);
        SaveFile.currentLoci.addRoom(r);
        doors = new int[] { 1, 0, 0, 0 };
        centre = new float[] { 0, 0, -10 };
        r = new Room(doors, centre, materials);
        SaveFile.currentLoci.addRoom(r);
        centre = new float[] { 20, 0, -7.5f };
        r = new Room(doors, centre, materials);
        SaveFile.currentLoci.addRoom(r);

        doors = new int[] { 0, 1, 1, 0, 0, 1, 0, 1 };
        centre = new float[] { 20, 0, 0 };
        Corridor c = new Corridor(doors, new Vector3(20, 0, 0), materials, 0);
        SaveFile.currentLoci.addCorridor(c);

        materials = new string[] { "Bricks1", "Wood Texture 05", "Wood texture 06" };
        doors = new int[] { 0, 0, 1, 0 };
        centre = new float[] { 30, 0, 7.5f };
        r = new Room(doors, centre, materials);
        SaveFile.currentLoci.addRoom(r);
        doors = new int[] { 0, 1, 0, 0 };
        centre = new float[] { 12.5f, 0, 17.5f };
        r = new Room(doors, centre, materials);
        SaveFile.currentLoci.addRoom(r);
        doors = new int[] { 0, 0, 1, 0 };
        centre = new float[] { 20, 0, 37.5f };
        r = new Room(doors, centre, materials);
        SaveFile.currentLoci.addRoom(r);

        doors = new int[] { 0, 0, 0, 1, 0, 1, 0, 1 };
        centre = new float[] { 20, 0, 17.5f };
        c = new Corridor(doors, centre, materials, 90);
        SaveFile.currentLoci.addCorridor(c);*/

        SaveFile.save(); // SaveFile the current Loci to file
    }
}
