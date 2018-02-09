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

		cut (frontWall, doorStates [0]);
		//cut (rightWall, doorStates [1]);
		//cut (rearWall, doorStates [2]);
		//cut (leftWall, doorStates [3]);
	}
	private void cut (GameObject input, int state){
		switch (state) {
		case 1:
			cutDoor (input, new Vector3 (-3, -2.5f, 0.125f));
			break;
		case 2:
			cutDoor (input, new Vector3 (0, -2.5f, 0.125f));
			break;
		case 3:
			cutDoor (input, new Vector3 (3, -2.5f, 0.125f));
			break;
		default:
			break;
		}
	}
	private Vector3[] getTriangleVertices (GameObject input){
		Mesh mesh = input.transform.GetComponent<MeshFilter> ().mesh;
		int[] triangles = mesh.triangles;
		Vector3[] vertices = mesh.vertices;
		Vector3[] answer = new Vector3[triangles.Length];

		Vector3 v;
		Vector3 scale = input.transform.localScale;
		Vector3 translation = input.transform.localPosition;
		print ("scale: " + scale+", translation: "+translation+", num triangles: "+triangles.Length/3);
		for (int k = 0; k < triangles.Length; k++) {
			v = vertices [triangles [k]];
			v.Scale (scale);
			v += translation;
			answer [k] = v;
		}
		return answer;
	}
	private Vector3[] getVertices (GameObject input){
		Mesh mesh = input.transform.GetComponent<MeshFilter> ().mesh;
		int[] triangles = mesh.triangles;
		Vector3[] vertices = mesh.vertices;
		Vector3[] answer = new Vector3[vertices.Length];

		Vector3 v;
		Vector3 scale = input.transform.localScale;
		Vector3 translation = input.transform.localPosition;
		print ("scale: " + scale+", translation: "+translation+", num vertices: "+vertices.Length);
		for (int k = 0; k < vertices.Length; k++) {
			v = vertices [k];
			v.Scale (scale);
			v += translation;
			answer [k] = v;
		}
		return answer;
	}
	private void cutDoor(GameObject input, Vector3 doorLoc){
		Destroy (input.GetComponent<MeshCollider> ());
		Mesh mesh = input.transform.GetComponent<MeshFilter> ().mesh;
		int[] triangles = mesh.triangles;
		//Vector3[] vertices = mesh.vertices;

		List<Vector3> vertices = compileVertices (input.transform.localPosition, doorLoc);

		Vector3[] current = getTriangleVertices (input);
		List<int> newTriangles = new List<int> ();
		
		//string test = "triangle: ";
		//Vector3 s1, s2, N;
		//for (int k = 0; k < triangles.Length; k += 3) {
		//	print (test + current [k] + ", " + current [k + 1] + ", " + current [k + 2]);
		//
		//	s1 = current [k + 1] - current [k];
		//	s2 = current [k + 2] - current [k];
		//	N = Vector3.Cross (s1, s2);
		//
		//	if (N.z != 0) {
		//	} else {
		//		newTriangles.Add (triangles [k]);
		//		newTriangles.Add (triangles [k + 1]);
		//		newTriangles.Add (triangles [k + 2]);
		//	}
		//		
		//}

		//Vector3[] vss = getVertices (input);
		//string test = "vertex: ";
		//for (int k = 0; k < vertices.Length; k++)
		//	print (test + vss [k] + ", index: " + k);
		//
		//int a, b, c;
		//test = "indices: ";
		//for (int k = 0; k < triangles.Length; k += 3) {
		//	a = triangles [k];
		//	b = triangles [k + 1];
		//	c = triangles [k + 2];
		//
		//	print(test+a+", "+b+", "+c);
		//}

		//Vector3 v;
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
		newTriangles.Add (1);
		newTriangles.Add (11);
		newTriangles.Add (3);

		//newTriangles.Add (9);
		//newTriangles.Add (5);
		//newTriangles.Add (3);
		//
		//newTriangles.Add (11);
		//newTriangles.Add (9);
		//newTriangles.Add (3);
		//
		//newTriangles.Add (1);
		//newTriangles.Add (11);
		//newTriangles.Add (3);
		//
		//newTriangles.Add (1);
		//newTriangles.Add (15);
		//newTriangles.Add (11);
		//
		//newTriangles.Add (15);
		//newTriangles.Add (13);
		//newTriangles.Add (11);

		for (int k = 0; k < 16; k += 2)
			print (vertices [k] + ", " + vertices [k + 1]);
		mesh.vertices = vertices.ToArray();
		mesh.triangles = newTriangles.ToArray ();
		input.AddComponent<MeshCollider> ();
	}
	private List<Vector3> compileVertices (Vector3 wallLoc, Vector3 doorLoc){
		List<Vector3> ans = new List<Vector3> ();

		ans.Add(wallLoc + new Vector3 (ROOM_SIZE / 2, wallLoc.y, 0.125f));
		ans.Add(ans[0] + new Vector3 (0, 0, -0.25f));
		ans.Add(ans[0] + new Vector3 (-(ROOM_SIZE - 0.25f), 0, 0));
		ans.Add(ans[1] + new Vector3 (-(ROOM_SIZE - 0.25f), 0, 0));

		ans.Add(ans[2] + new Vector3 (0, -2 * wallLoc.y, 0));
		ans.Add(ans[3] + new Vector3 (0, -2 * wallLoc.y, 0));
		ans.Add(wallLoc + doorLoc + new Vector3 (-1, 0, 0));
		ans.Add(ans[6] + new Vector3 (0, 0, -0.25f));

		ans.Add(ans[6] + new Vector3 (0, 4, 0));
		ans.Add(ans[7] + new Vector3 (0, 4, 0));
		ans.Add(ans[8] + new Vector3 (2, 0, 0));
		ans.Add(ans[9] + new Vector3 (2, 0, 0));

		ans.Add(ans[6] + new Vector3 (2, 0, 0));
		ans.Add(ans[7] + new Vector3 (2, 0, 0));
		ans.Add(ans[0] + new Vector3 (0, -2 * wallLoc.y, 0));
		ans.Add(ans[1] + new Vector3 (0, -2 * wallLoc.y, 0));

		return ans;
	}

	// Update is called once per frame
	void Update () {

	}
}
