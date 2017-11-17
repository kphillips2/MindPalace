﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A 10 X 10 square room
public class RoomBuilder : MonoBehaviour {
	public GameObject floor;
	public GameObject doorWall;
	public GameObject filledWall;

	private GameObject component;

	// Use this for initialization
	void Start (){
		doorWall.SetActive (false);
		filledWall.SetActive (false);
	}
	// returns the vectors for each inside corner
	public Vector3[,] getInsideCorners(Vector3 roomCentre){
		Vector3[,] corners = new Vector3[4,4];
		corners [0,0] = roomCentre + new Vector3 (4.75f, 0, 4.75f);
		corners [1,0] = roomCentre + new Vector3 (4.75f, 0, -4.75f);
		corners [2,0] = roomCentre + new Vector3 (-4.75f, 0, -4.75f);
		corners [3,0] = roomCentre + new Vector3 (-4.75f, 0, 4.75f);

		return corners;
	}

	// input: Vector3 for the center of the room
	public void addFloor(Vector3 roomCentre){
		Vector3 centre = roomCentre + new Vector3 (0, -0.125f, 0);
		component = Instantiate (
			floor,
			centre,
			Quaternion.Euler (0, 0, 0)
		) as GameObject;
	}
	// input: Vector3 for the center of the room
	//        int array for the state of each wall
	public void addWalls(Vector3 roomCentre, int[] doorStates){
		// door numbers correspond with indices of doorStates
		// (->) is the start direction
		//   ----0----
		//  |         |
		//  |         |
		//  3   ->    1
		//  |         |
		//  |         |
		//   ----2----

		// states correspond with values of doorStates
		// 0 = door inactive
		// 1 = door active

		for(int k=0;k<4;k++) switch(doorStates[k]){
		case 0:
			addWall(roomCentre, (k-1)*90);
			break;
		case 1:
			addDoor(roomCentre, k*90);
			break;
		default:
			break;
		}
	}

	// input: angle representing which side to put the wall on
	//        0 degrees is door location 0
	private void addDoor(Vector3 roomCentre, int angle){
		component = Instantiate(
			doorWall,
			roomCentre,
			Quaternion.Euler (0, angle, 0)
		) as GameObject;
		component.SetActive (true);
	}
	// input: angle representing which side to put the wall on
	//        -90 degrees is door location 0
	private void addWall(Vector3 roomCentre, int angle){
		component = Instantiate(
			filledWall,
			roomCentre,
			Quaternion.Euler (0, angle, 0)
		) as GameObject;
		component.SetActive (true);
	}
}