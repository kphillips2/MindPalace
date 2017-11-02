using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour {
	public GameObject floor;
	public GameObject filledWall;
	public GameObject doorWall;

	private GameObject component;

	// Use this for initialization
	void Start (){
		doorWall.SetActive(false);
		filledWall.SetActive(false);
	}

	// input: Vector3 for the center of the room
	//        byte array for the state of each wall
	public void addWalls(Vector3 roomCentre, int[] input){
		// Wall Locations are the indices of input

		//       0
		//    --------
		//   |        |
		// 3 |        |
		//   |        | 1
		//   |        |
		//    --------
		//       2

		// wall with door is 1
		// wall without door is 2

		for(int k=0;k<4;k++)
			switch(input[k]){
				case 0:
					break;
				case 1:
					addDoor(roomCentre, k*90);
					break;
				default:
					addWall(roomCentre, (k-1)*90);
					break;
			}
	}

	// input: angle representing which side to put the wall on
	//        0 degrees is wall location 0
	private void addDoor(Vector3 roomCentre, int angle){
		component = Instantiate(
			doorWall,
			roomCentre,
			Quaternion.Euler(0, angle, 0)
		) as GameObject;
	}
	// input: angle representing which side to put the wall on
	//        -90 degrees is wall location 0
	private void addWall(Vector3 roomCentre, int angle){
		component = Instantiate(
			filledWall,
			roomCentre,
			Quaternion.Euler(0, angle, 0)
		) as GameObject;
	}

	// Update is called once per frame
	void Update () {

	}
}