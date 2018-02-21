using System.Collections;
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

	private float ROOM_SIZE = 12;
	private DoorCutter doorCutter;
	private List<Vector3>[] doors;

	// Use this for initialization
	void Awake () {
		doors = new List<Vector3>[4];
		for (int k = 0; k < 4; k++)
			doors [k] = new List<Vector3> ();
		doorCutter = floor.GetComponent<DoorCutter> ();
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
	public void addDoor (int wallIndex, float doorLoc){
		// wall numbers correspond with indices of doorStates
		// (->) is the start direction
		//   ----0----
		//  |         |
		//  |         |
		//  3   ->    1
		//  |         |
		//  |         |
		//   ----2----

		float doorLimit = ROOM_SIZE / 2 - 1.5f;

		if (wallIndex >= 0 && wallIndex <= 3) {
			if (doorLoc >= -doorLimit && doorLoc <= doorLimit)
				switch (wallIndex) {
				case 1:
					cutDoor (rightWall, 1, doorLoc);
					break;
				case 2:
					cutDoor (rearWall, 2, doorLoc);
					break;
				case 3:
					cutDoor (leftWall, 3, doorLoc);
					break;
				default:
					cutDoor (frontWall, 0, doorLoc);
					break;
				}
			else
				Debug.LogError ("The door at {" + doorLoc + "} is too close to end of the wall.");
		} else
			Debug.LogError ("A wall with index of {" + wallIndex + "} doesn't exist.");
	}
	private void cutDoor (GameObject input, int wallIndex, float doorLoc){
		Vector3 doorCentre = new Vector3 (doorLoc, 0, 0);
		doors [wallIndex].Add (doorCentre);
		doors [wallIndex].Sort ((a, b) => a.x.CompareTo (b.x));
		if (doorCutter.cutDoor (input, doors [wallIndex].ToArray (), ROOM_SIZE))
			doors [wallIndex].Remove (doorCentre);
	}

	// Update is called once per frame
	void Update () {

	}
}