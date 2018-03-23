using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour {
	public GameObject floor;
	public GameObject roof;

	public GameObject posZWall;
	public GameObject posXWall;
	public GameObject negZWall;
	public GameObject negXWall;

	private float ROOM_WIDTH = 12;
	private float ROOM_LENGTH = 12;
	private WallCutter wallCutter;
	private List<Vector3>[] doors;

	// Use this for initialization
	void Awake() {
		doors = new List<Vector3>[4];
		for (int k = 0; k < 4; k++)
			doors [k] = new List<Vector3> ();
        wallCutter = floor.GetComponent<WallCutter> ();
	}
	// input: two floats that represent the size of the room
	public void setRoomSize(float width, float length){
        Vector3 dimensions = new Vector3(width, 0.25f, length);
        FloorResizer.resize (floor, dimensions);
        FloorResizer.resize (roof, dimensions);

		posZWall.transform.localPosition = new Vector3 (0, 2.5f, length/2-0.125f);
		posXWall.transform.localPosition = new Vector3 (0, 2.5f, width/2-0.125f);
		negZWall.transform.localPosition = new Vector3 (0, 2.5f, length/2-0.125f);
		negXWall.transform.localPosition = new Vector3 (0, 2.5f, width/2-0.125f);

		ROOM_WIDTH = width;
		ROOM_LENGTH = length;

        adjustWall (posZWall, 0);
        adjustWall (posXWall, 1);
        adjustWall (negZWall, 2);
        adjustWall (negXWall, 3);
	}
	// input: three strings which represent the materials for the room
	public void setMaterials(string floorName, string roofName, string wallName){
		Material floorMat = Resources.Load ("Materials/"+floorName, typeof(Material)) as Material;
		Material roofMat = Resources.Load ("Materials/"+roofName, typeof(Material)) as Material;
		Material wallMat = Resources.Load ("Materials/"+wallName, typeof(Material)) as Material;

		floorMat.mainTexture.wrapMode = TextureWrapMode.Repeat;
		roofMat.mainTexture.wrapMode = TextureWrapMode.Repeat;
		wallMat.mainTexture.wrapMode = TextureWrapMode.Repeat;

		floor.GetComponent<Renderer> ().material = floorMat;
		roof.GetComponent<Renderer> ().material = roofMat;

		posZWall.GetComponent<Renderer> ().material = wallMat;
		posXWall.GetComponent<Renderer> ().material = wallMat;
		negZWall.GetComponent<Renderer> ().material = wallMat;
		negXWall.GetComponent<Renderer> ().material = wallMat;
	}
	// input: index, loc (-6)------------(6)
	public void addDoor(int wallIndex, float doorLoc){
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
                    adjustForDoor (posXWall, 1, doorLoc);
                    break;
                case 2:
                    adjustForDoor (negZWall, 2, doorLoc);
                    break;
                case 3:
                    adjustForDoor (negXWall, 3, doorLoc);
                    break;
                default:
                    adjustForDoor (posZWall, 0, doorLoc);
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
            switch (wallIndex) {
                case 1:
                    adjustForWindow (posXWall, 1, windowLoc);
                    break;
                case 2:
                    adjustForWindow (negZWall, 2, windowLoc);
                    break;
                case 3:
                    adjustForWindow (negXWall, 3, windowLoc);
                    break;
                default:
                    adjustForWindow (posZWall, 0, windowLoc);
                    break;
            }
        else
            Debug.LogError ("A wall with index of {" + wallIndex + "} doesn't exist.");
    }
    // gets either the room width or the room length depending on the index
    private float getWallSize(int wallIndex){
        return (wallIndex % 2 == 0) ? ROOM_WIDTH : ROOM_LENGTH;
    }
    private void adjustForDoor(GameObject input, int wallIndex, float doorLoc){
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
    private void adjustForWindow(GameObject input, int wallIndex, float windowLoc){
        float wallLength = getWallSize (wallIndex);
        float windowLimit = wallLength / 2 - 2f;

        if (windowLoc >= -windowLimit && windowLoc <= windowLimit){
            Vector3 windowCentre = new Vector3(windowLoc, 1.5f, 0);
            doors [wallIndex].Add (windowCentre);
            doors [wallIndex].Sort ((a, b) => a.x.CompareTo(b.x));
            if (wallCutter.cutDoorsAndWindows (input, doors[wallIndex].ToArray(), wallLength))
                doors [wallIndex].Remove (windowCentre);
        } else
            Debug.LogError ("The Window at {" + windowLoc + "} is too close to end of the wall.");
    }
    private void adjustWall(GameObject input, int wallIndex){
        float wallLength = getWallSize (wallIndex);
        wallCutter.cutDoorsAndWindows (input, doors [wallIndex].ToArray (), wallLength);
	}

	// Update is called once per frame
	void Update () {

	}
}