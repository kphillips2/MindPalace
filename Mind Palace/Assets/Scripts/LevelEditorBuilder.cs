using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorBuilder : MonoBehaviour {
	public GameObject room;
	public GameObject corridor;
	public GameObject space;

	private GameObject currentRoom;
	private OldRoom oldRoom;
	private OldCorridor oldCorridor;
	private RoomBuilder roomBuilder;
    private PictureCreator pictureCreator;
    private GameObject component;

    // Use this for initialization
    void Start () {
		oldRoom = room.GetComponent<OldRoom>();
		oldCorridor = corridor.GetComponent<OldCorridor>();
        pictureCreator = new PictureCreator();

<<<<<<< HEAD
		roomBuilder = space.GetComponent<RoomBuilder>();
		// input: index, loc (-6)------------(6)
		//roomBuilder.addDoor (0, -3);
		//roomBuilder.addDoor (1, 0);
		//roomBuilder.addDoor (2, -3);
		roomBuilder.addDoor (1, 0);
		roomBuilder.addDoor (1, 3);

		roomBuilder.setMaterials (
			"Wood Texture 06", // floor material
			"Wood Texture 15", // roof material
			"Wood texture 12"  // wall material
		);
        
        int[] roomDoors = {1, 1, 1, 1};
		Vector3 centre = new Vector3(0, 0, 0);

		oldRoom.setMaterials (
			"Wood Texture 06", // floor material
			"Wood Texture 15", // roof material
			"Wood texture 12"  // wall material
		);
		oldRoom.addFloor (centre);
		oldRoom.addRoof (centre);
        /*
		oldRoom.addWalls(
			centre,
			roomDoors
		);
=======
		roomCreator = space.GetComponent<RoomCreator>();
        /*
		// input: index, loc (-6)------------(6)
		roomCreator.addDoor (0, -3);
		roomCreator.addDoor (1, 0);
		//roomCreator.addDoor (2, -3);
		roomCreator.addDoor (3, 3);
>>>>>>> 2b6f34a07edbf85d57211b02364581991281b261
        */
        roomCreator.setMaterials(
           "Wood Texture 06", // floor material
            "Wood Texture 15", // roof material
            "Wood texture 12"  // wall material
        );
        addRoom(new Vector3(0,0,0));   
    }

	public GameObject addRoom(Vector3 roomCentre){
        component = Instantiate(
            space,
            roomCentre,
            Quaternion.Euler(0, 0, 0)
        ) as GameObject;
        component.SetActive(true);
        return component;

    }
	private void addCorridor(Vector3 roomCentre, int[] doorStates, int angle){
		oldCorridor.addFloor (
			roomCentre,
			angle
		);
		oldCorridor.addRoof (
			roomCentre,
			angle
		);
		oldCorridor.addWalls (
			roomCentre,
			doorStates,
			angle
		);
	}


	// Update is called once per frame
	void Update () {

	}
}