﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A 5 X 30 rectangular corridor
public class CorridorBuilder : MonoBehaviour {
	public GameObject floor;
    public GameObject roof;
    public GameObject doorWall;
	public GameObject filledWall;
	public GameObject wallEnd;
	public GameObject doorEnd;

	private GameObject component;
	private bool built;
	private int rotation;

	private Material wallTexture;

	// Use this for initialization
	void Start (){
		built = false;
		doorWall.SetActive (false);
		filledWall.SetActive (false);
		wallEnd.SetActive (false);
		doorEnd.SetActive (false);
		wallTexture = Resources.Load ("Materials/leather", typeof(Material)) as Material;
	}

	// input: Vector3 for the center of the corridor
	//		  0 degrees creates the floor horizontally
	public void addFloor(Vector3 corridorCentre, int angle){
		Vector3 centre = corridorCentre + new Vector3 (0, -0.125f, 0);
		component = Instantiate (
			floor,
			centre,
			Quaternion.Euler (0, angle, 0)
		) as GameObject;
		component.GetComponent<Renderer> ().material = wallTexture;
	}
    // input: Vector3 for the center of the corridor
    //		  0 degrees creates the roof horizontally
    public void addRoof(Vector3 corridorCentre, int angle)
    {
        component = Instantiate(
            roof,
			corridorCentre,
            Quaternion.Euler(0, angle, 0)
        ) as GameObject;
    }
    // input: Vector3 for the center of the corridor
    //        int array for the state of each wall
    //		  0 degrees creates the floor horizontally
    public void addWalls(Vector3 corridorCentre, int[] doorStates, int angle){
		// door numbers correspond with indices of doorStates
		// (->) is the start direction without rotation
		//   -----0-----1-----2-----
		//  7       ->              3
		//   -----6-----5-----4-----

		// states correspond with values of doorStates
		// 0 = door inactive
		// 1 = door active
		rotation = angle;

		if(doorStates [3] == 1)
			addDoorEnd(corridorCentre, 180);
		else
			addWallEnd(corridorCentre, 0);

		if (doorStates [7] == 1)
			addDoorEnd(corridorCentre, 0);
		else
			addWallEnd(corridorCentre, 180);

		int[] zeros = {0, 0, 0};

		int[] topWall = {
			doorStates[2],
			doorStates[1],
			doorStates[0]
		};
		if(checkEqual(topWall, zeros))
			addFilledWall(corridorCentre, 0);
		else
			addDoorWall(corridorCentre, 180, topWall);

		int[] bottomWall = {
			doorStates[6],
			doorStates[5],
			doorStates[4]
		};
		if (checkEqual(bottomWall, zeros))
			addFilledWall (corridorCentre, 180);
		else
			addDoorWall (corridorCentre, 0, bottomWall);
	}
	// input: two arrays
	// output: true if the arrays share the same values
	private bool checkEqual(int[] first, int[] second){
		for (int k = 0; k < first.Length; k++)
			if (first [k] != second [k])
				return false;
		return true;
	}

	// input: angle representing which side to put the wall on
	//        0 degrees is door location 3
	private void addDoorEnd(Vector3 centre, int angle){
		component = Instantiate (
			doorEnd,
			centre,
			Quaternion.Euler (0, angle+rotation, 0)
		) as GameObject;
		component.SetActive (true);
	}
	// input: angle representing which side to put the wall on
	//        180 degrees is door location 3
	private void addWallEnd(Vector3 centre, int angle){
		component = Instantiate (
			wallEnd,
			centre,
			Quaternion.Euler (0, angle+rotation, 0)
		) as GameObject;
		component.SetActive (true);
	}
	// input: angle representing which side to put the wall on
	//        0 degrees represents door locations 0, 1 and 2 
	private void addFilledWall(Vector3 centre, int angle){
		component = Instantiate (
			filledWall,
			centre,
			Quaternion.Euler (0, angle+rotation, 0)
		) as GameObject;
		component.SetActive (true);
	}
	// input: angle representing which side to put the wall on
	//        180 degrees represents door locations 0, 1 and 2 
	private void addDoorWall(Vector3 centre, int angle, int[] doorStates){
		if (built == false) {
			component = Instantiate (
				doorWall,
				centre,
				Quaternion.Euler (0, angle+rotation, 0)
			) as GameObject;
			component.SetActive (true);
			built = true;
		}
		doorWall.GetComponent<WallBuilder>().addWalls(centre, doorStates, rotation+angle);
	}
}