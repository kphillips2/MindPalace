using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A 10 X 10 square room
public class RoomBuilder : MonoBehaviour {
	public GameObject floor;
    public GameObject roof;
    public GameObject doorWall;
	public GameObject filledWall;

	private GameObject component;
	private Renderer[] materials;

	private Material floorMat;
	private Material roofMat;
	private Material wallMat;

	// Use this for initialization
	void Start (){
		floor.SetActive (false);
		roof.SetActive (false);
		doorWall.SetActive (false);
		filledWall.SetActive (false);
	}
	// input: three strings which represent the materials for the room
	public void setMaterials (string floorName, string roofName, string wallName){
		floorMat = Resources.Load ("Materials/"+floorName, typeof(Material)) as Material;
		roofMat = Resources.Load ("Materials/"+roofName, typeof(Material)) as Material;
		wallMat = Resources.Load ("Materials/"+wallName, typeof(Material)) as Material;
	}

	// returns the vectors for each inside corner
	public Vector3[,] getInsideCorners(Vector3 roomCentre, int[] doorStates){
		Vector3[,] corners = new Vector3[4,2];
		corners [0,0] = roomCentre + new Vector3 (4.75f, 0, 4.75f);
		corners [1,0] = roomCentre + new Vector3 (4.75f, 0, -4.75f);
		corners [2,0] = roomCentre + new Vector3 (-4.75f, 0, -4.75f);
		corners [3,0] = roomCentre + new Vector3 (-4.75f, 0, 4.75f);

		if(doorStates[0] == 1)
			corners[0, 1] = corners[0,0] + new Vector3 (-4.75f, 0, 0);
		if(doorStates[1] == 1)
			corners[0, 1] = corners[0,0] + new Vector3 (0, 0, 4.75f);
		if(doorStates[2] == 1)
			corners[0, 1] = corners[0,0] + new Vector3 (4.75f, 0, 0);
		if(doorStates[3] == 1)
			corners[0, 1] = corners[0,0] + new Vector3 (0, 0, -4.75f);

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
		component.GetComponent<Renderer> ().material = floorMat;
		component.SetActive (true);
	}
    // input: Vector3 for the center of the room
    public void addRoof(Vector3 roomCentre)
    {
        component = Instantiate(
            roof,
            roomCentre,
            Quaternion.Euler(0, 0, 0)
        ) as GameObject;
		component.GetComponentInChildren<Renderer> ().material = roofMat;
		component.SetActive (true);
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
		materials = component.GetComponentsInChildren<Renderer> ();
		foreach (Renderer renderer in materials)
			renderer.material = wallMat;
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
		component.GetComponentInChildren<Renderer> ().material = wallMat;
		component.SetActive (true);
	}
}