using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A 12 X 12 square room
public class RoomCreator : MonoBehaviour {
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

	// Use this for initialization
	void Start () {
		
	}

	public void addDoors(int[] doorStates){
		// wall numbers correspond with indices of doorStates
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
		// 1 = door left justify
		// 2 = door centre
		// 3 = door right justify
		if (doorStates [0] != 0)
			cut (frontWall, doorStates [0]);
		if (doorStates [1] != 0)
			cut (rightWall, doorStates [1]);
		if (doorStates [2] != 0)
			cut (rearWall, doorStates [2]);
		if (doorStates [3] != 0)
			cut (leftWall, doorStates [3]);
	}

	private void cut(GameObject input, int state){
		Destroy (input.GetComponent<MeshCollider> ());
		Mesh mesh = input.transform.GetComponent<MeshFilter> ().mesh;
		int[] oldTriangles = mesh.triangles;
		int[] newTriangles = new int[mesh.triangles.Length];

		int i = 0, j = 0;
		while (j < mesh.triangles.Length) {
			if (j > 20 || j < 10) {
				newTriangles [i++] = oldTriangles [j++];
				newTriangles [i++] = oldTriangles [j++];
				newTriangles [i++] = oldTriangles [j++];
			} else {
				j += 3;
			}
		}

		input.transform.GetComponent<MeshFilter> ().mesh.triangles = newTriangles;
		input.AddComponent<MeshCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
