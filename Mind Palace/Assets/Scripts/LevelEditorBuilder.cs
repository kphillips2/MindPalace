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
	private RoomCreator roomCreator;
    private PictureCreator pictureCreator;
    private GameObject component;

    // Use this for initialization
    void Start () {
		oldRoom = room.GetComponent<OldRoom>();
		oldCorridor = corridor.GetComponent<OldCorridor>();
        pictureCreator = new PictureCreator();
<<<<<<< HEAD
		roomBuilder = space.GetComponent<RoomCreator>();
=======

		roomCreator = space.GetComponent<RoomCreator>();
>>>>>>> parent of afe92dcb... fixedPlusSignIBroke
        /*
		// input: index, loc (-6)------------(6)
		roomCreator.addDoor (0, -3);
		roomCreator.addDoor (1, 0);
		//roomCreator.addDoor (2, -3);
		roomCreator.addDoor (3, 3);
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