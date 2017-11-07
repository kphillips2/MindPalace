using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorridorBuilder : MonoBehaviour {
	public GameObject floor;
	public GameObject doorWall;
	public GameObject filledWall;
	public GameObject wallEnd;
	public GameObject doorEnd;

	private GameObject component;
	private WallBuilder wallBuilder;

	// Use this for initialization
	void Start (){
		//wallBuilder = doorWall.GetComponent<WallBuilder>();
		//doorWall.SetActive (false);
		//filledWall.SetActive(false);
		wallEnd.SetActive(false);
		doorEnd.SetActive(false);
	}

	// input: Vector3 for the center of the corridor
	//        int array for the state of each wall
	public void addWalls(Vector3 corridorCentre, int[] doorStates){
		// door numbers correspond with indices of doorStates
		//   -----0-----1-----2-----
		//  7                       3
		//   -----6-----5-----4-----

		// states correspond with values of doorStates
		// 0 = door inactive
		// 1 = door active

		if (doorStates [3] == 1)
			addDoorEnd (corridorCentre, 0);
		else
			addWallEnd (corridorCentre, 180);
		
		if (doorStates [7] == 1)
			addDoorEnd (corridorCentre, 180);
		else
			addWallEnd (corridorCentre, 0);

		int[] topWall = {
			doorStates [0],
			doorStates [1],
			doorStates [2]
		};
		int[] bottomWall = {
			doorStates[4],
			doorStates[5],
			doorStates[6]
		};
	}
	private void addDoorEnd(Vector3 centre, int angle){
		component = Instantiate (
			doorEnd,
			centre,
			Quaternion.Euler (0, angle, 0)
		) as GameObject;
	}
	private void addWallEnd(Vector3 centre, int angle){
		component = Instantiate (
			wallEnd,
			centre,
			Quaternion.Euler (0, angle, 0)
		) as GameObject;
	}

	// Update is called once per frame
	void Update () {

	}
}