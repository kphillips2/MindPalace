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
			cutDoor (input, new Vector3 (-ROOM_SIZE/4, -2.5f, 0.125f));
			break;
		case 2:
			cutDoor (input, new Vector3 (0, -2.5f, 0.125f));
			break;
		case 3:
			cutDoor (input, new Vector3 (ROOM_SIZE/4, -2.5f, 0.125f));
			break;
		default:
			break;
		}
	}
	private void cutDoor(GameObject input, Vector3 doorLoc){
		Destroy (input.GetComponent<BoxCollider> ());

		// get new vertices after scaling
		List<Vector3> vertices = compileVertices (input.transform.localPosition, doorLoc);
		// create an list for the new triangles
		List<int> triangles = new List<int> ();
		// set triangles and and new vertices
		compileTriangles(triangles, vertices);
		// resize the vertices to remove scaling
		Vector3[] vectors = resizeVectors (vertices, input.transform.localScale, input.transform.localPosition);

		for (int k = 0; k < vectors.Length; k += 2)
			print (vectors [k] + ", " + vectors [k + 1]);

		input.transform.GetComponent<MeshFilter> ().mesh.Clear ();
		input.transform.GetComponent<MeshFilter> ().mesh.vertices = vectors;
		input.transform.GetComponent<MeshFilter> ().mesh.triangles = triangles.ToArray();

		input.transform.GetComponent<MeshFilter> ().mesh.RecalculateBounds();
		input.AddComponent<MeshCollider> ();
	}
	private void compileTriangles(List<int> triangles, List<Vector3> vertices){
		// close face
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
		// end close face

		// far face
		for (int k = 0; k < 18; k+=3){
			triangles.Add (triangles [k + 2] - 1);
			triangles.Add (triangles [k + 1] - 1);
			triangles.Add (triangles [k] - 1);
		}
		// end far face


		// top face
		vertices.Add(vertices[0]);
		vertices.Add(vertices[1]);
		vertices.Add(vertices[2]);
		vertices.Add(vertices[3]);

		addFace (triangles, vertices.Count);
		// end top face

		// right face
		vertices.Add(vertices[0]);
		vertices.Add(vertices[14]);
		vertices.Add(vertices[1]);
		vertices.Add(vertices[15]);
		
		addFace (triangles, vertices.Count);
		// end right face

		// left face
		vertices.Add(vertices[3]);
		vertices.Add(vertices[5]);
		vertices.Add(vertices[2]);
		vertices.Add(vertices[4]);
		
		addFace (triangles, vertices.Count);
		// end left face

		// bottom faces
		vertices.Add(vertices[7]);
		vertices.Add(vertices[6]);
		vertices.Add(vertices[5]);
		vertices.Add(vertices[4]);

		addFace (triangles, vertices.Count);

		vertices.Add(vertices[15]);
		vertices.Add(vertices[14]);
		vertices.Add(vertices[13]);
		vertices.Add(vertices[12]);

		addFace (triangles, vertices.Count);
		// end bottom faces

		// door faces
		vertices.Add(vertices[11]);
		vertices.Add(vertices[10]);
		vertices.Add(vertices[9]);
		vertices.Add(vertices[8]);

		addFace (triangles, vertices.Count);

		vertices.Add(vertices[11]);
		vertices.Add(vertices[13]);
		vertices.Add(vertices[10]);
		vertices.Add(vertices[12]);

		addFace (triangles, vertices.Count);

		vertices.Add(vertices[8]);
		vertices.Add(vertices[6]);
		vertices.Add(vertices[9]);
		vertices.Add(vertices[7]);

		addFace (triangles, vertices.Count);
		// end door faces
	}
	private void addFace(List<int> triangles, int size){
		size -= 4;

		triangles.Add (size + 0);
		triangles.Add (size + 1);
		triangles.Add (size + 3);

		triangles.Add (size + 3);
		triangles.Add (size + 2);
		triangles.Add (size + 0);
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