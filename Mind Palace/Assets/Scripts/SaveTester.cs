using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTester : MonoBehaviour {

    void Start() {
        //performSaveTest();
    }

    // Create a save file to test
    void performSaveTest () {
        Loci.current = new Loci("saveTest");

        ObjectPlacer op = new ObjectPlacer();
        op.createPrefab(0, -3.468538f, -12.47951f, 0f); //Bed
        op.createPrefab(1, 4.15f, 8.72f, -90f); //Book cases
        op.createPrefab(1, 4.15f, 10.43f, -90f);
        op.createPrefab(1, -4.16f, -6.81f, 90f);
        op.createPrefab(1, -4.44f, 7.66f, 90f);
        op.createPrefab(2, 2.7956f, -3.76479f, 0f); //Chair
        op.createPrefab(3, 19.07f, -7.08f, 90f); //Chair2
        op.createPrefab(3, 21.93f, -8.543f, 270f);
        op.createPrefab(3, 19.07f, -8.543f, 90f);
        op.createPrefab(3, 21.92f, -7.08f, 270f);
        op.createPrefab(4, -1.18f, 12.39f, 90f); //Coffee table
        op.createPrefab(5, -3.47f, 12.44f, 90f); //Couches
        op.createPrefab(5, 1f, 12.24f, -90f);
        op.createPrefab(5, 1.31f, -9.26f, 90f);
        op.createPrefab(6, 16f, -7.22f, 90f); //Counters
        op.createPrefab(6, 16f, -8.52f, 90f);
        op.createPrefab(7, -4.09f, 0.05f, 90f); //Fireplace
        op.createPrefab(8, 4.0925f, -3.876518f, 0f); //Flower tables
        op.createPrefab(8, -3.865167f, 3.955207f, 0f);
        op.createPrefab(8, -3.643468f, -3.745927f, 0f);
        op.createPrefab(8, 4.133508f, 3.98855f, 0f);
        op.createPrefab(8, 34.03f, 10.99f, 0f);
        op.createPrefab(8, 33.83f, 3.35468f, 0f);
        op.createPrefab(9, 16.12f, -11.23f, 90f); //Fridge
        op.createPrefab(10, 0.3411262f, -14.19815f, 0f); //Guitar
        op.createPrefab(11, 3.07f, -14.07f, 0f); //Headboard
        op.createPrefab(12, 33.44f, -0.1f, -90f); //Lion statue
        op.createPrefab(13, 16.13f, -9.81f, 90f); //Oven
        op.createPrefab(14, 23.8f, -3.97f, 0f); //Plants
        op.createPrefab(14, 3.810411f, 13.44f, 0f);
        op.createPrefab(14, 3.810411f, 6.19f, 0f);
        op.createPrefab(15, 34.25f, 5.94f, -90f); //Sink
        op.createPrefab(16, 1.01692f, -12.85651f, 0f); //Soccer ball
        op.createPrefab(17, 20.4941f, -7.77f, 90f); //Table
        op.createPrefab(18, 34.13f, 7.88f, -90f); //Toilet
        op.createPrefab(19, 3.937f, -9.35f, -90f); //TV

        int[] doors = new int[] { 1, 1, 1, 0 };
        float[] centre = new float[] { 0, 0, 0 };
        string[] materials = new string[] { "Wood Texture 06", "Wood Texture 15", "Wood texture 12" };
        Room r = new Room(doors, centre, materials);
        Loci.current.rooms.Add(r);
        doors = new int[] { 0, 0, 1, 0 };
        centre = new float[] { 0, 0, 10 };
        r = new Room(doors, centre, materials);
        Loci.current.rooms.Add(r);
        doors = new int[] { 1, 0, 0, 0 };
        centre = new float[] { 0, 0, -10 };
        r = new Room(doors, centre, materials);
        Loci.current.rooms.Add(r);
        centre = new float[] { 20, 0, -7.5f };
        r = new Room(doors, centre, materials);
        Loci.current.rooms.Add(r);

        doors = new int[] { 0, 1, 1, 0, 0, 1, 0, 1 };
        centre = new float[] { 20, 0, 0 };
        Corridor c = new Corridor(doors, centre, materials, 0);
        Loci.current.corridors.Add(c);

        materials = new string[] { "Bricks1", "Wood Texture 05", "Wood texture 06" };
        doors = new int[] { 0, 0, 1, 0 };
        centre = new float[] { 30, 0, 7.5f };
        r = new Room(doors, centre, materials);
        Loci.current.rooms.Add(r);
        doors = new int[] { 0, 1, 0, 0 };
        centre = new float[] { 12.5f, 0, 17.5f };
        r = new Room(doors, centre, materials);
        Loci.current.rooms.Add(r);
        doors = new int[] { 0, 0, 1, 0 };
        centre = new float[] { 20, 0, 37.5f };
        r = new Room(doors, centre, materials);
        Loci.current.rooms.Add(r);

        doors = new int[] { 0, 0, 0, 1, 0, 1, 0, 1 };
        centre = new float[] { 20, 0, 17.5f };
        c = new Corridor(doors, centre, materials, 90);
        Loci.current.corridors.Add(c);

        SaveLoad.save();
    }
}
