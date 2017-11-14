using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuilder : MonoBehaviour {
	public GameObject doorTop;
	public GameObject sideToEnd;
	public GameObject sideToMiddle;
	public GameObject sideToSide;
	public GameObject middleToEnd;
	public GameObject sideToOpposite;

	private GameObject component;
	private int rotation;
	private int xrot;
	private int zrot;

	// Use this for initialization
	void Start (){
		doorTop.SetActive (false);
		sideToEnd.SetActive (false);
		sideToMiddle.SetActive (false);
		sideToSide.SetActive (false);
		middleToEnd.SetActive (false);
		sideToOpposite.SetActive (false);
	}

	// input: Vector3 for the center of the wall
	//        int array for the state of each wall
	public void addWalls(Vector3 wallCentre, int[] doorStates, int angle, int x, int z){
		// door numbers correspond with indices of doorStates
		// left -----0-----1-----2----- Right

		// states correspond with values of doorStates
		// 0 = door inactive
		// 1 = door active
		rotation = angle;
		xrot = x;
		zrot = z;

		if( checkEqual (doorStates, new int[]{ 1, 0, 0 }))
			addOneSide (wallCentre, -1);
		else if (checkEqual (doorStates, new int[]{ 0, 0, 1 }))
			addOneSide (wallCentre, 1);
		else if (checkEqual (doorStates, new int[]{ 1, 1, 0 }))
			addMiddleSide (wallCentre, -1);
		else if (checkEqual (doorStates, new int[]{ 0, 1, 1 }))
			addMiddleSide (wallCentre, 1);
		else if (checkEqual (doorStates, new int[]{ 1, 0, 1 }))
			addBothSides (wallCentre);
		else if (checkEqual (doorStates, new int[]{ 0, 1, 0 }))
			addOneMiddle (wallCentre);
		else
			addAllDoors(wallCentre);
	}
	// input: two arrays
	// output: true if the arrays share the same values
	private bool checkEqual(int[] first, int[] second){
		for (int k = 0; k < first.Length; k++)
			if (first [k] != second [k])
				return false;
		return true;
	}

	// input: wallSide represents which side to put the side door on
	//        1 is a door on the right side of the wall
	private void addDoorEnd(Vector3 wallCentre, int wallSide){
		Vector3 centre = wallCentre + new Vector3 (
			12.875f * wallSide * xrot, 2.5f, 12.875f * wallSide * zrot
		);
		component = Instantiate (
			sideToEnd,
			centre,
			Quaternion.Euler (0, rotation, 0)
		) as GameObject;
		component.SetActive (true);

		centre += new Vector3 (
			-2.875f * wallSide * xrot, 2, -2.875f * wallSide * xrot
		);
		component = Instantiate (
			doorTop,
			centre,
			Quaternion.Euler (0, rotation, 0)
		) as GameObject;
		component.SetActive (true);
	}
	// input: wallSide represents which side to put the side door on
	//        1 is a door on the right side of the wall
	private void addOneSide(Vector3 wallCentre, int wallSide){
		addDoorEnd (wallCentre, wallSide);
		Vector3 centre = wallCentre + new Vector3 (
			-2.875f * wallSide * xrot, 2.5f, -2.875f * wallSide * zrot
		);
		component = Instantiate (
			sideToOpposite,
			centre,
			Quaternion.Euler (0, rotation, 0)
		) as GameObject;
		component.SetActive (true);
	}
	// input: wallSide represents which side to put the side door on
	//        1 is doors on the right side of the wall
	private void addMiddleSide(Vector3 wallCentre, int wallSide){
		addDoorEnd (wallCentre, wallSide);
		Vector3 centre = wallCentre + new Vector3 (
			-7.875f * wallSide * xrot, 2.5f, -7.875f * wallSide * zrot
		);
		component = Instantiate (
			middleToEnd,
			centre,
			Quaternion.Euler (0, rotation, 0)
		) as GameObject;
		component.SetActive (true);

		centre += new Vector3 (
			12.875f * wallSide * xrot, 0, 12.875f * wallSide * zrot
		);
		component = Instantiate (
			sideToMiddle,
			centre,
			Quaternion.Euler(0, rotation, 0)
		) as GameObject;
		component.SetActive (true);

		centre += new Vector3 (
			-5f * wallSide * xrot, 2, -5f * wallSide * zrot
		);
		component = Instantiate (
			doorTop,
			centre,
			Quaternion.Euler(0, rotation, 0)
		) as GameObject;
		component.SetActive (true);
	}
	// puts a door on left and right side but not middle
	private void addBothSides(Vector3 wallCentre){
		addDoorEnd (wallCentre, 1);
		addDoorEnd (wallCentre, -1);
		Vector3 centre = wallCentre + new Vector3 (0, 2.5f, 0);
		component = Instantiate (
			sideToSide,
			centre,
			Quaternion.Euler(0, rotation, 0)
		) as GameObject;
		component.SetActive (true);
	}
	// input: wallSide represents which side to put the side door on
	//        1 is doors on the right side of the wall
	private void addOneMiddle(Vector3 wallCentre){
		Vector3 centre = wallCentre + new Vector3 (
			7.875f * xrot, 2.5f, 7.875f * zrot
		);
		component = Instantiate (
			middleToEnd,
			centre,
			Quaternion.Euler (0, rotation, 0)
		) as GameObject;
		component.SetActive (true);

		centre += new Vector3 (
			-15.75f * xrot, 0, -15.75f * zrot
		);
		component = Instantiate (
			middleToEnd,
			centre,
			Quaternion.Euler(0, rotation, 0)
		) as GameObject;
		component.SetActive (true);

		centre += new Vector3 (
			7.875f * xrot, 2, 7.875f * zrot
		);
		component = Instantiate (
			doorTop,
			centre,
			Quaternion.Euler(0, rotation, 0)
		) as GameObject;
		component.SetActive (true);
	}
	// puts a door in all three locations
	private void addAllDoors(Vector3 wallCentre){
		addDoorEnd (wallCentre, 1);
		addDoorEnd (wallCentre, -1);
		Vector3 centre = wallCentre + new Vector3 (
			5 * xrot, 2.5f, 5 * zrot
		);
		component = Instantiate (
			sideToMiddle,
			centre,
			Quaternion.Euler(0, rotation, 0)
		) as GameObject;
		component.SetActive (true);

		centre += new Vector3 (
			-10 * xrot, 0, -10 * zrot
		);
		component = Instantiate (
			sideToMiddle,
			centre,
			Quaternion.Euler(0, rotation, 0)
		) as GameObject;
		component.SetActive (true);

		centre += new Vector3 (
			5 * xrot, 2, 5 * zrot
		);
		component = Instantiate (
			doorTop,
			centre,
			Quaternion.Euler (0, rotation, 0)
		) as GameObject;
		component.SetActive (true);
	}

	// Update is called once per frame
	void Update () {

	}
}