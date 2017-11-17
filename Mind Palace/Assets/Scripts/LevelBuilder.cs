using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {
	public GameObject room;
	public GameObject corridor;

	private GameObject currentRoom;
	private RoomBuilder roomBuilder;
	private CorridorBuilder corridorBuilder;

	// Use this for initialization
	void Start () {
		roomBuilder = room.GetComponent<RoomBuilder>();
		corridorBuilder = corridor.GetComponent<CorridorBuilder>();

		int[] roomDoors = {1, 1, 1, 0};
		Vector3 centre = new Vector3(0, 0, 0);
		roomBuilder.addWalls(
			centre,
			roomDoors
		);
        //adds roof to starting room
        roomBuilder.addRoof(centre);

        roomDoors = new int[]{0, 0, 1, 0};
		centre = new Vector3(0, 0, 10);
		addRoom(centre, roomDoors);
        //adds roof to starting room
        roomBuilder.addRoof(centre);

        roomDoors = new int[]{1, 0, 0, 0};
		centre = new Vector3(0, 0, -10);
		addRoom(centre, roomDoors);
        //adds roof to starting room
        roomBuilder.addRoof(centre);

        roomDoors = new int[]{1, 0, 0, 0};
		centre = new Vector3(20, 0, -7.5f);
		addRoom(centre, roomDoors);
        //adds roof to starting room
        roomBuilder.addRoof(centre);

        roomDoors = new int[]{0, 0, 1, 0};
		centre = new Vector3(30, 0, 7.5f);
		addRoom(centre, roomDoors);
        //adds roof to starting room
        roomBuilder.addRoof(centre);

        roomDoors = new int[] { 0, 1, 0, 0 };
        centre = new Vector3(12.5f, 0, 17.5f);
        addRoom(centre, roomDoors);
        //adds roof to starting room
        roomBuilder.addRoof(centre);

        roomDoors = new int[] { 0, 0, 1, 0 };
        centre = new Vector3(20, 0, 37.5f);
        addRoom(centre, roomDoors);
        //adds roof to starting room
        roomBuilder.addRoof(centre);

        int[] corridorDoors = {
			0, 1, 1,	//top wall
			0,			//right end wall
			0, 1, 0,	//bottom wall
			1			//left end wall
		};
		centre = new Vector3 (20, 0, 0);
		corridorBuilder.addWalls (
			centre, corridorDoors, 0
		);

		corridorDoors = new int[]{0, 0, 0, 1, 0, 1, 0, 1};
		centre = new Vector3 (20, 0, 17.5f);
		addCorridor (centre, corridorDoors, 90);
        corridorBuilder.addRoof(centre, 90);
	}

	private void addRoom(Vector3 roomCentre, int[] doorStates){
		roomBuilder.addFloor (roomCentre);
		roomBuilder.addWalls (
			roomCentre,
			doorStates
		);
	}
	private void addCorridor(Vector3 roomCentre, int[] doorStates, int angle){
		corridorBuilder.addFloor (
			roomCentre,
			angle
		);
		corridorBuilder.addWalls (
			roomCentre,
			doorStates,
			angle
		);
	}


	// Update is called once per frame
	void Update () {

	}
}