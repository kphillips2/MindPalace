﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorBuilder : MonoBehaviour {
	public GameObject room;
	public GameObject corridor;
	public GameObject space;

	private GameObject currentRoom;
	private RoomCreator roomBuilder;
    private PictureCreator pictureCreator;
    private GameObject component;

    // Use this for initialization
    void Start () {
        pictureCreator = new PictureCreator();
		roomBuilder = space.GetComponent<RoomCreator>();
        /*
		// input: index, loc (-6)------------(6)
		roomCreator.addDoor (0, -3);
		roomCreator.addDoor (1, 0);
		//roomCreator.addDoor (2, -3);
		roomCreator.addDoor (3, 3);
        */
		roomBuilder.setMaterials(
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


	// Update is called once per frame
	void Update () {

	}
}