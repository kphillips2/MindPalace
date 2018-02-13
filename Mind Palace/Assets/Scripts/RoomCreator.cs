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

		string test = "triangle: ";
		for (int k = 0; k < triangles.Length; k += 3)
			print (test + answer [k] + ", " + answer [k + 1] + ", " + answer [k + 2]);
		return answer;
	}
	private void cutDoor(GameObject input, Vector3 doorLoc){
		Destroy (input.GetComponent<MeshCollider> ());

		// get new vertices and their vectors
		List<Vector3> vertices = compileVertices (input.transform.localPosition, doorLoc);
		Vector3[] vectors = resizeVectors (vertices, input.transform.localScale, input.transform.localPosition);

		// create an list for the new triangles
		int[] triangles = compileTriangles();

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

		for (int k = 0; k < vectors.Length; k += 2)
			print (vectors [k] + ", " + vectors [k + 1]);

		input.transform.GetComponent<MeshFilter> ().mesh.Clear ();
		input.transform.GetComponent<MeshFilter> ().mesh.vertices = vectors;
		input.transform.GetComponent<MeshFilter> ().mesh.triangles = triangles;

		input.transform.GetComponent<MeshFilter> ().mesh.RecalculateBounds();
		input.AddComponent<MeshCollider> ();

		Vector3[] current = getTriangleVertices (input);
	}
	private int[] compileTriangles(){
		List<int> triangles = new List<int> ();

		triangles.Add (7);
		triangles.Add (5);
		triangles.Add (9);

		triangles.Add (9);
		triangles.Add (5);
		triangles.Add (3);

		triangles.Add (3);
		triangles.Add (1);
		triangles.Add (9);

		triangles.Add (9);
		triangles.Add (1);
		triangles.Add (11);

		triangles.Add (11);
		triangles.Add (1);
		triangles.Add (15);

		triangles.Add (15);
		triangles.Add (13);
		triangles.Add (11);

		return triangles.ToArray ();
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
	private Vector3[] resizeVectors(List<Vector3> vertices, Vector3 scale, Vector3 translation){
		Vector3[] answer = vertices.ToArray();
		Vector3 s = new Vector3(1/scale.x,1/scale.y,1/scale.z);
		Vector3 v;
		for (int k = 0; k < vertices.Count; k++){
			v = vertices [k] - translation;
			v.Scale (s);
			answer [k] = v;
		}
		return answer;
	}

	// Update is called once per frame
	void Update () {

	}
}