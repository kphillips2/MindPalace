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

		int[] walls = {1, 1, 1, 0};
		Vector3 centre = new Vector3(0, 0, 0);
		roomBuilder.addWalls (
			centre,
			walls
		);

		walls = new int[]{0, 1, 1, 0};
		centre = new Vector3(0, 0, 10);
		addRoom(centre, walls);

		walls = new int[]{1, 1, 0, 0};
		centre = new Vector3(0, 0, -10);
		addRoom(centre, walls);

		int[] other = {0, 0, 0, 1, 0, 0, 0, 1};
		centre = new Vector3(20, 0, 0);
		corridorBuilder.addWalls (
			centre,
			other
		);
	}

	private void addRoom(Vector3 roomCentre, int[] input){
		currentRoom = Instantiate(
			room,
			roomCentre,
			Quaternion.Euler(0, 0, 0)
		) as GameObject;
		roomBuilder.addWalls(
			roomCentre,
			input
		);
	}
	private void addCorridor(Vector3 roomCentre, int[] input){
		currentRoom = Instantiate(
			corridor,
			roomCentre,
			Quaternion.Euler(0, 0, 0)
		) as GameObject;
		corridorBuilder.addWalls(
			roomCentre,
			input
		);
	}

	// Update is called once per frame
	void Update () {

	}
}