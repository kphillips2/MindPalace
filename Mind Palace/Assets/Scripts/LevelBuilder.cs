using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {
	public GameObject room;

	private GameObject currentRoom;
	//private RoomBuilder roomBuilder;

	// Use this for initialization
	void Start () {
		//roomBuilder = room.GetComponent<RoomBuilder>();

		Vector3 centre = new Vector3(0, 0, 0);
		int[] walls = {1, 2, 1, 2};
		room.GetComponent<RoomBuilder>().addWalls(
			centre,
			walls
		);

		centre = new Vector3(0, 0, 20);
		addRoom(centre, walls);

		centre = new Vector3(0, 0, -20);
		addRoom(centre, walls);
	}

	private void addRoom(Vector3 roomCentre, int[] input){
		currentRoom = Instantiate(
			room,
			roomCentre,
			Quaternion.Euler(0, 0, 0)
		) as GameObject;
		currentRoom.GetComponent<RoomBuilder>().addWalls(
			roomCentre,
			input
		);
	}

	// Update is called once per frame
	void Update () {

	}
}