﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour {
	public GameObject floor;
	public GameObject roof;
	// positive z
	public GameObject frontWall;
	// positive x
	public GameObject rightWall;
	// negative z
	public GameObject rearWall;
	// negative x
	public GameObject leftWall;

	private Material floorMat;
	private Material roofMat;
	private Material wallMat;

	private int ROOM_SIZE = 12;
	private DoorCutter doorCutter;
	private List<Vector3>[] doors;

	// Use this for initialization
	void Start () {
		doors = new List<Vector3>[4];
		for (int k = 0; k < 4; k++)
			doors[k] = new List<Vector3>();
		doorCutter = floor.GetComponent<DoorCutter>();
	}
	// input: three strings which represent the materials for the room
	public void setMaterials (string floorName, string roofName, string wallName){
		floorMat = Resources.Load ("Materials/"+floorName, typeof(Material)) as Material;
		roofMat = Resources.Load ("Materials/"+roofName, typeof(Material)) as Material;
		wallMat = Resources.Load ("Materials/"+wallName, typeof(Material)) as Material;

		floorMat.mainTexture.wrapMode = TextureWrapMode.Repeat;
		roofMat.mainTexture.wrapMode = TextureWrapMode.Repeat;
		wallMat.mainTexture.wrapMode = TextureWrapMode.Repeat;

		floor.GetComponent<Renderer> ().material = floorMat;
		roof.GetComponent<Renderer> ().material = roofMat;

		frontWall.GetComponent<Renderer> ().material = wallMat;
		rightWall.GetComponent<Renderer> ().material = wallMat;
		rearWall.GetComponent<Renderer> ().material = wallMat;
		leftWall.GetComponent<Renderer> ().material = wallMat;
	}
	// input: index, loc (-6)------------(6)
	public void addDoor (int wall, float doorLoc){
		// wall numbers correspond with indices of doorStates
		// (->) is the start direction
		//   ----0----
		//  |         |
		//  |         |
		//  3   ->    1
		//  |         |
		//  |         |
		//   ----2----

		if(wall >=0 && wall <= 3)
			if(doorLoc >= -6 && doorLoc <= 6)
				switch (wall) {
		case 1:
			doors [1].Add (new Vector3 (doorLoc, 0, 0));
			doorCutter.cutDoor (rightWall, doors[1].ToArray(), ROOM_SIZE);
			break;
		case 2:
			doors [2].Add (new Vector3 (doorLoc, 0, 0));
			doorCutter.cutDoor (rightWall, doors[2].ToArray(), ROOM_SIZE);
			break;
		case 3:
			doors [3].Add (new Vector3 (doorLoc, 0, 0));
			doorCutter.cutDoor (rightWall, doors[3].ToArray(), ROOM_SIZE);
			break;
		default:
			doors [0].Add (new Vector3 (doorLoc, 0, 0));
			doorCutter.cutDoor (rightWall, doors[0].ToArray(), ROOM_SIZE);
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
