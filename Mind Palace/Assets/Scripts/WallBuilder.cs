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
	private Material wallMat;

	// Use this for initialization
	void Start (){
		doorTop.SetActive (false);
		sideToEnd.SetActive (false);
		sideToMiddle.SetActive (false);
		sideToSide.SetActive (false);
		middleToEnd.SetActive (false);
		sideToOpposite.SetActive (false);
	}
	// input: the material for the wall
	public void setMaterial(Material material){
		wallMat = material;
	}

	// input: Vector3 for the center of the wall
	//        int array for the state of each wall
	public void addWalls(Vector3 roomCentre, int[] doorStates, int angle){
		// door numbers correspond with indices of doorStates
		// left -----0-----1-----2----- Right

		// states correspond with values of doorStates
		// 0 = door inactive
		// 1 = door active
		rotation = angle;

		if( checkEqual (doorStates, new int[]{ 1, 0, 0 }))
			addOneSideDoor (roomCentre, -1);
		else if (checkEqual (doorStates, new int[]{ 0, 0, 1 }))
			addOneSideDoor (roomCentre, 1);
		else if (checkEqual (doorStates, new int[]{ 1, 1, 0 }))
			addMiddleAndOneSideDoor (roomCentre, -1);
		else if (checkEqual (doorStates, new int[]{ 0, 1, 1 }))
			addMiddleAndOneSideDoor (roomCentre, 1);
		else if (checkEqual (doorStates, new int[]{ 1, 0, 1 }))
			addBothSideDoors (roomCentre);
		else if (checkEqual (doorStates, new int[]{ 0, 1, 0 }))
			addOnlyMiddleDoor (roomCentre);
		else
			addAllDoors (roomCentre);
	}
	// input: two arrays
	// output: true if the arrays share the same values
	private bool checkEqual(int[] first, int[] second){
		for (int k = 0; k < first.Length; k++)
			if (first [k] != second [k])
				return false;
		return true;
	}

	// input: wallSide represents 1 for right, -1 for left
	private void addEndDoor(Vector3 roomCentre, int wallSide){
		Vector3 wallCentre = roomCentre + new Vector3 (12.875f * wallSide, 2.5f, -2.375f);
		component = Instantiate (
			sideToEnd,
			wallCentre,
			Quaternion.Euler (0, 0, 0)
		) as GameObject;
		component.transform.RotateAround (roomCentre, Vector3.up, rotation);
		component.GetComponentInChildren<Renderer> ().material = wallMat;
		component.SetActive (true);

		wallCentre += new Vector3 (-2.875f * wallSide, 2, 0);
		component = Instantiate (
			doorTop,
			wallCentre,
			Quaternion.Euler (0, 0, 0)
		) as GameObject;
		component.transform.RotateAround (roomCentre, Vector3.up, rotation);
		component.GetComponentInChildren<Renderer> ().material = wallMat;
		component.SetActive (true);
	}
	// input: wallSide represents 1 for right, -1 for left
	private void addWallBetweenSideDoorAndMiddle(Vector3 roomCentre, int wallSide){
		Vector3 wallCentre = roomCentre + new Vector3 (5 * wallSide, 2.5f, -2.375f);
		component = Instantiate (
			sideToMiddle,
			wallCentre,
			Quaternion.Euler (0, 0, 0)
		) as GameObject;
		component.transform.RotateAround (roomCentre, Vector3.up, rotation);
		component.GetComponentInChildren<Renderer> ().material = wallMat;
		component.SetActive (true);
	}
	// input: wallSide represents 1 for right, -1 for left
	private void addWallBetweenEndAndMiddle(Vector3 roomCentre, int wallSide){
		Vector3 wallCentre = roomCentre + new Vector3 (7.875f * wallSide, 2.5f, -2.375f);
		component = Instantiate (
			middleToEnd,
			wallCentre,
			Quaternion.Euler (0, 0, 0)
		) as GameObject;
		component.transform.RotateAround (roomCentre, Vector3.up, rotation);
		component.GetComponentInChildren<Renderer> ().material = wallMat;
		component.SetActive (true);
	}
	// input: wallSide represents 1 for right, -1 for left
	private void addOneSideDoor(Vector3 roomCentre, int wallSide){
		addEndDoor (roomCentre, wallSide);

		Vector3 wallCentre = roomCentre + new Vector3 (-2.875f * wallSide, 2.5f, -2.375f);
		component = Instantiate (
			sideToOpposite,
			wallCentre,
			Quaternion.Euler (0, 0, 0)
		) as GameObject;
		component.transform.RotateAround (roomCentre, Vector3.up, rotation);
		component.GetComponentInChildren<Renderer> ().material = wallMat;
		component.SetActive (true);
	}
	// input: wallSide represents 1 for right, -1 for left
	private void addMiddleAndOneSideDoor(Vector3 roomCentre, int wallSide){
		addEndDoor (roomCentre, wallSide);
		addWallBetweenEndAndMiddle (roomCentre, -wallSide);
		addWallBetweenSideDoorAndMiddle (roomCentre, wallSide);

		Vector3 wallCentre = roomCentre + new Vector3 (0, 4.5f, -2.375f);
		component = Instantiate (
			doorTop,
			wallCentre,
			Quaternion.Euler(0, 0, 0)
		) as GameObject;
		component.transform.RotateAround (roomCentre, Vector3.up, rotation);
		component.GetComponentInChildren<Renderer> ().material = wallMat;
		component.SetActive (true);
	}
	// puts a door on left and right side but not middle
	private void addBothSideDoors(Vector3 roomCentre){
		addEndDoor (roomCentre, 1);
		addEndDoor (roomCentre, -1);

		Vector3 wallCentre = roomCentre + new Vector3 (0, 2.5f, -2.375f);
		component = Instantiate (
			sideToSide,
			wallCentre,
			Quaternion.Euler(0, 0, 0)
		) as GameObject;
		component.transform.RotateAround (roomCentre, Vector3.up, rotation);
		component.GetComponentInChildren<Renderer> ().material = wallMat;
		component.SetActive (true);
	}
	// puts a door only in the middle
	private void addOnlyMiddleDoor(Vector3 roomCentre){
		addWallBetweenEndAndMiddle (roomCentre, 1);
		addWallBetweenEndAndMiddle (roomCentre, -1);

		Vector3 wallCentre = roomCentre + new Vector3 (0, 4.5f, -2.375f);
		component = Instantiate (
			doorTop,
			wallCentre,
			Quaternion.Euler(0, 0, 0)
		) as GameObject;
		component.transform.RotateAround (roomCentre, Vector3.up, rotation);
		component.GetComponentInChildren<Renderer> ().material = wallMat;
		component.SetActive (true);
	}
	// puts a door in all three locations
	private void addAllDoors(Vector3 roomCentre){
		addEndDoor (roomCentre, 1);
		addEndDoor (roomCentre, -1);
		addWallBetweenSideDoorAndMiddle (roomCentre, 1);
		addWallBetweenSideDoorAndMiddle (roomCentre, -1);

		Vector3 wallCentre = roomCentre + new Vector3 (0, 4.5f, -2.375f);
		component = Instantiate (
			doorTop,
			wallCentre,
			Quaternion.Euler (0, 0, 0)
		) as GameObject;
		component.transform.RotateAround (roomCentre, Vector3.up, rotation);
		component.GetComponentInChildren<Renderer> ().material = wallMat;
		component.SetActive (true);
	}
}