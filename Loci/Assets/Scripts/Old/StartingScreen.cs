using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScreen : MonoBehaviour {

    public OldRoom room;
    private PictureCreator pictureCreator;
    private int[] roomDoors;

    // Use this for initialization
    void Start () {

        pictureCreator = new PictureCreator();
        roomDoors = new int[] { 0, 1, 1, 1 };
        room.setMaterials(
                "Wood Texture 15", // floor material
                "Wood Texture 05", // roof material
                "Wood texture 05"  // wall material
                 );
        room.addFloor(new Vector3(0, 0, 0));
        room.addRoof(new Vector3(0, 0, 0));
        room.addWalls(
            new Vector3(0, 0, 0),
            roomDoors
        );
    }

    // Update is called once per frame
    void Update () {
		
	}
}
