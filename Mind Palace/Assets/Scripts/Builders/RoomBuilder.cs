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
	private WallCutter wallCutter;
	private List<Vector3>[] doors;
	private FloorResizer floorResizer;

	// Use this for initialization
	void Awake () {
		doors = new List<Vector3>[4];
		for (int k = 0; k < 4; k++)
			doors [k] = new List<Vector3> ();
        wallCutter = floor.GetComponent<WallCutter> ();
        floorResizer = floor.GetComponent<FloorResizer> ();
	}
	// input: two floats that represent the size of the room
	public void setRoomSize(float width, float length){
        Vector3 dimensions = new Vector3(width, 0.25f, length);
        floorResizer.resize (floor, dimensions);
        floorResizer.resize (roof, dimensions);

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
                    adjustForDoor (rightWall, 1, doorLoc);
                    break;
                case 2:
                    adjustForDoor (rearWall, 2, doorLoc);
                    break;
                case 3:
                    adjustForDoor (leftWall, 3, doorLoc);
                    break;
                default:
                    adjustForDoor (frontWall, 0, doorLoc);
                    break;
            }
		else
			Debug.LogError ("A wall with index of {" + wallIndex + "} doesn't exist.");
	}
    // input: index, loc (-6)------------(6)
    public void addWindow(int wallIndex, float windowLoc)
    {
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
            switch (wallIndex)
            {
                case 1:
                    adjustForWindow (rightWall, 1, windowLoc);
                    break;
                case 2:
                    adjustForWindow (rearWall, 2, windowLoc);
                    break;
                case 3:
                    adjustForWindow (leftWall, 3, windowLoc);
                    break;
                default:
                    adjustForWindow (frontWall, 0, windowLoc);
                    break;
            }
        else
            Debug.LogError ("A wall with index of {" + wallIndex + "} doesn't exist.");
    }
    // gets either the room width or the room length depending on the index
    private float getWallSize(int wallIndex){
        switch (wallIndex % 2) {
            case 0:
                return ROOM_WIDTH;
            default:
                return ROOM_LENGTH;
        }
    }
    private void adjustForDoor (GameObject input, int wallIndex, float doorLoc){
        float wallLength = getWallSize (wallIndex);
		float doorLimit = wallLength / 2 - 1.5f;

		if (doorLoc >= -doorLimit && doorLoc <= doorLimit) {
			Vector3 doorCentre = new Vector3 (doorLoc, 0, 0);
			doors [wallIndex].Add (doorCentre);
			doors [wallIndex].Sort ((a, b) => a.x.CompareTo (b.x));
			if (wallCutter.cutDoorsAndWindows (input, doors [wallIndex].ToArray (), wallLength))
				doors [wallIndex].Remove (doorCentre);
		} else
			Debug.LogError ("The door at {" + doorLoc + "} is too close to end of the wall.");
	}
    private void adjustForWindow(GameObject input, int wallIndex, float doorLoc){
        float wallLength = getWallSize (wallIndex);
        float windowLimit = wallLength / 2 - 2f;

        if (doorLoc >= -windowLimit && doorLoc <= windowLimit){
            Vector3 windowCentre = new Vector3(doorLoc, 1.5f, 0);
            doors [wallIndex].Add (windowCentre);
            doors [wallIndex].Sort ((a, b) => a.x.CompareTo(b.x));
            if (wallCutter.cutDoorsAndWindows (input, doors[wallIndex].ToArray(), wallLength))
                doors [wallIndex].Remove (windowCentre);
        } else
            Debug.LogError ("The Window at {" + doorLoc + "} is too close to end of the wall.");
    }
    private void adjustWall (GameObject input, int wallIndex){
        float wallLength = getWallSize (wallIndex);
        wallCutter.cutDoorsAndWindows (input, doors [wallIndex].ToArray (), wallLength);
	}

	// Update is called once per frame
	void Update () {

	}
}