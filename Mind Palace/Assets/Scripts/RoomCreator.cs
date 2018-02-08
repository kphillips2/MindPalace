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

	private const int ROOM_SIZE = 12;

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
		print("Door states : "+doorStates[0]+", "+doorStates[1]+", "+doorStates[2]+", "+doorStates[3]);
		if (doorStates [0] != 0)
			cut (frontWall, doorStates [0]);
		//if (doorStates [1] != 0)
		//	cut (rightWall, -1*doorStates [1]);
		//if (doorStates [2] != 0)
		//	cut (rearWall, doorStates [2]);
		//if (doorStates [3] != 0)
		//	cut (leftWall, -1*doorStates [3]);
		//rebuild(frontWall.transform.position, doorStates[0]);
	}
	private void rebuild (Vector3 input, int state){
		Vector3 p0 = input + new Vector3 (ROOM_SIZE / 2, input.y, 0.125f);
		Vector3 p1 = p0 + new Vector3 (0, 0, -0.25f);
		Vector3 p2 = p0 + new Vector3 (-(ROOM_SIZE - 0.25f), 0, 0);
		Vector3 p3 = p1 + new Vector3 (-(ROOM_SIZE - 0.25f), 0, 0);

		Vector3 p4 = p2 + new Vector3 (0, -2 * input.y, 0);
		Vector3 p5 = p3 + new Vector3 (0, -2 * input.y, 0);
		Vector3 p6 = input + new Vector3 (-1, -input.y, 0.125f);
		Vector3 p7 = p6 + new Vector3 (0, 0, -0.25f);

		Vector3 p8 = p6 + new Vector3 (0, 4, 0);
		Vector3 p9 = p7 + new Vector3 (0, 4, 0);
		Vector3 pA = p8 + new Vector3 (2, 0, 0);
		Vector3 pB = p9 + new Vector3 (2, 0, 0);

		Vector3 pC = p6 + new Vector3 (2, 0, 0);
		Vector3 pD = p7 + new Vector3 (2, 0, 0);
		Vector3 pE = p0 + new Vector3 (0, -2 * input.y, 0);
		Vector3 pF = p1 + new Vector3 (0, -2 * input.y, 0);
	}
	private void cut(GameObject input, int state){
		Destroy (input.GetComponent<MeshCollider> ());
		Mesh mesh = input.transform.GetComponent<MeshFilter> ().mesh;
		int[] triangles = mesh.triangles;
		Vector3[] vertices = mesh.vertices;

		string test = "triangle: ";
		//for (int k = 0; k < triangles.Length; k += 3)
		//	print (test + vertices [triangles [k]] + ", " + vertices [triangles [k + 1]] + ", " + vertices [triangles [k + 2]]);

		Vector3 a, b, c;
		Vector3 scale = input.transform.localScale;
		Vector3 translation = input.transform.position;
		print ("scale: " + scale+", translation: "+translation);
		for (int k = 0; k < triangles.Length; k += 3) {
			a = vertices [triangles [k]];
			b = vertices [triangles [k + 1]];
			c = vertices [triangles [k + 2]];
			a.Scale (scale);
			b.Scale (scale);
			c.Scale (scale);
			a += translation;
			b += translation;
			c += translation;
		
			print(test+a+", "+b+", "+c);
		}

		//Vector3 v;
		//
		//double dist = 0;
		//for (int k = 0; k < triangles.Length / 3; k++) {
		//	for (int x = 0; x < 3; x++) {
		//		v = vertices [triangles [k * 3 + x]];
		//		if ( v.y < 5) {
		//			// wall is parallel to z axis
		//			if (state > 0)
		//				dist = Mathf.Abs(v.z - centre.z);
		//			// wall is parallel to x axis
		//			else
		//				dist = Mathf.Abs(v.x - centre.x);
		//			if (dist >= 2.5)
		//				nxt [k] = cur [k];	//<---this is the problem
		//		} else {
		//			// add any vertex above the door
		//			nxt [k] = cur [k];
		//		}
		//	}
		//}
		//
		//
		//
		//
		//Vector3[] nxt = new Vector3[mesh.vertices.Length];
		//Vector3 centre = floor.transform.position;
		//
		//double dist = 0;
		//for (int k = 0; k < mesh.vertices.Length; k++)
		//	if (cur [k].y < 5) {
		//		// wall is parallel to z axis
		//		if (state > 0)
		//			dist = Mathf.Abs(cur [k].z - centre.z);
		//		// wall is parallel to x axis
		//		else
		//			dist = Mathf.Abs(cur [k].x - centre.x);
		//		if (dist >= 2.5)
		//			nxt [k] = cur [k];
		//	} else {
		//		// add any vertex above the door
		//		nxt [k] = cur [k];
		//	}
		//
		////int i = 0, j = 0;
		////while (j < mesh.triangles.Length) {
		////	if (j > 20 || j < 10) {
		////		newTriangles [i++] = oldTriangles [j++];
		////		newTriangles [i++] = oldTriangles [j++];
		////		newTriangles [i++] = oldTriangles [j++];
		////	} else {
		////		j += 3;
		////	}
		////}
		//
		//input.transform.GetComponent<MeshFilter> ().mesh.vertices = nxt;
		input.AddComponent<MeshCollider> ();
	}

	// Update is called once per frame
	void Update () {

	}
}
