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

	private float ROOM_WIDTH = 12;
	private float ROOM_LENGTH = 12;
	private DoorCutter doorCutter;
	private List<Vector3>[] doors;

	// Use this for initialization
	void Awake () {
		doors = new List<Vector3>[4];
		for (int k = 0; k < 4; k++)
			doors [k] = new List<Vector3> ();
		doorCutter = floor.GetComponent<DoorCutter> ();
	}
	// input: two floats that represent the size of the room
	public void setRoomSize(float width, float length){
		floor.transform.localScale = new Vector3(width, 0.25f, length);
		roof.transform.localScale = new Vector3(width, 0.25f, length);

		frontWall.transform.localPosition = new Vector3 (0, 2.5f, length/2-0.125f);
		rightWall.transform.localPosition = new Vector3 (0, 2.5f, width/2-0.125f);
		rearWall.transform.localPosition = new Vector3 (0, 2.5f, length/2-0.125f);
		leftWall.transform.localPosition = new Vector3 (0, 2.5f, width/2-0.125f);

		ROOM_WIDTH = width;
		ROOM_LENGTH = length;

		adjustWall (frontWall, 0);
		adjustWall (rightWall, 1);
		adjustWall (rearWall, 2);
		adjustWall (leftWall, 3);
	}
	// input: three strings which represent the materials for the room
	public void setMaterials (string floorName, string roofName, string wallName){
		Material floorMat = Resources.Load ("Materials/"+floorName, typeof(Material)) as Material;
		Material roofMat = Resources.Load ("Materials/"+roofName, typeof(Material)) as Material;
		Material wallMat = Resources.Load ("Materials/"+wallName, typeof(Material)) as Material;

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

		if (wallIndex >= 0 && wallIndex <= 3)
			switch (wallIndex) {
			case 1:
				adjustWall (rightWall, 1, doorLoc);
				break;
			case 2:
				adjustWall (rearWall, 2, doorLoc);
				break;
			case 3:
				adjustWall (leftWall, 3, doorLoc);
				break;
			default:
				adjustWall (frontWall, 0, doorLoc);
				break;
			}
		else
			Debug.LogError ("A wall with index of {" + wallIndex + "} doesn't exist.");
	}
	private void adjustWall (GameObject input, int wallIndex, float doorLoc){
		float wallLength; switch (wallIndex%2) {
		case 0:
			wallLength = ROOM_WIDTH;
			break;
		default:
			wallLength = ROOM_LENGTH;
			break;
		}
		float doorLimit = wallLength / 2 - 1.5f;

		if (doorLoc >= -doorLimit && doorLoc <= doorLimit) {
			Vector3 doorCentre = new Vector3 (doorLoc, 0, 0);
			doors [wallIndex].Add (doorCentre);
			doors [wallIndex].Sort ((a, b) => a.x.CompareTo (b.x));
			if (doorCutter.cutDoor (input, doors [wallIndex].ToArray (), wallLength))
				doors [wallIndex].Remove (doorCentre);
		} else
			Debug.LogError ("The door at {" + doorLoc + "} is too close to end of the wall.");
	}
	private void adjustWall (GameObject input, int wallIndex){
		float wallLength; switch (wallIndex%2) {
		case 0:
			wallLength = ROOM_WIDTH;
			break;
		default:
			wallLength = ROOM_LENGTH;
			break;
		}
		doorCutter.cutDoor (input, doors [wallIndex].ToArray (), wallLength);
	}

	// Update is called once per frame
	void Update () {

	}
}