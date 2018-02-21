using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCutter : MonoBehaviour {
	private float wallSize;
	private int doorCount;

	// Use this for initialization
	void Start () {
		wallSize = 4;
		doorCount = 0;
	}
	public void cutDoor(GameObject input, Vector3[] doorLocs, float size){
		Destroy (input.GetComponent<BoxCollider> ());
		Destroy (input.GetComponent<MeshCollider> ());
		wallSize = size;
		// get new vertices after scaling
		List<Vector3> vertices = compileVertices (input.transform.localPosition, doorLocs);
		// set triangles and and new vertices
		List<int> triangles = new List<int>();
		compileTriangles (triangles, vertices);
		// resize the vertices to remove scaling
		Vector3[] vectors = resizeVectors (vertices, input.transform.localScale, input.transform.localPosition);
		// create an list for the new UV map
		List<Vector2> uvs = new List<Vector2> ();
		for (int k = 0; k < vectors.Length; k++)
			uvs.Add (new Vector2 (vectors [k].x, vectors [k].y));

		input.transform.GetComponent<MeshFilter> ().mesh.Clear ();
		input.transform.GetComponent<MeshFilter> ().mesh.vertices = vectors;
		input.transform.GetComponent<MeshFilter> ().mesh.triangles = triangles.ToArray ();
		input.transform.GetComponent<MeshFilter> ().mesh.uv = uvs.ToArray ();

		input.transform.GetComponent<MeshFilter> ().mesh.RecalculateBounds ();
		input.transform.GetComponent<MeshFilter> ().mesh.RecalculateNormals ();
		input.transform.GetComponent<MeshFilter> ().mesh.RecalculateTangents ();
		input.AddComponent<MeshCollider> ();
	}
	private void compileTriangles(List<int> triangles, List<Vector3> vertices){
		// close face
		triangles.Add (11);
		triangles.Add (9);
		triangles.Add (5);

		triangles.Add (5);
		triangles.Add (3);
		triangles.Add (11);

		triangles.Add (11);
		triangles.Add (3);
		triangles.Add (1);

		for (int k = 1; k < doorCount; k++)
			addDoorFace (triangles, k);
		int mark = 8 + 8 * (doorCount - 1);

		triangles.Add (1);
		triangles.Add (mark + 5);
		triangles.Add (11);

		triangles.Add (mark + 5);
		triangles.Add (1);
		triangles.Add (7);

		triangles.Add (7);
		triangles.Add (mark + 7);
		triangles.Add (mark + 5);
		// end close face

		// far face
		for (int k = triangles.Count-1; k > 0; k-=3){
			triangles.Add (triangles [k] - 1);
			triangles.Add (triangles [k - 1] - 1);
			triangles.Add (triangles [k - 2] - 1);
		}
		// end far face

		// right faces
		vertices.Add (vertices [0]);
		vertices.Add (vertices [6]);
		vertices.Add (vertices [1]);
		vertices.Add (vertices [7]);

		addSquareFace (triangles, vertices.Count);

		// door face(s)
		for (int k = 0; k < doorCount; k++) {
			mark = 8 + 8 * k;

			vertices.Add (vertices [mark + 2]);
			vertices.Add (vertices [mark]);
			vertices.Add (vertices [mark + 3]);
			vertices.Add (vertices [mark + 1]);

			addSquareFace (triangles, vertices.Count);
		}
		// end door face(s)
		// end right faces

		// left faces
		vertices.Add(vertices[3]);
		vertices.Add(vertices[5]);
		vertices.Add(vertices[2]);
		vertices.Add(vertices[4]);

		addSquareFace (triangles, vertices.Count);

		// door face(s)
		for (int k = 0; k < doorCount; k++) {
			mark = 8 + 8 * k;

			vertices.Add (vertices [mark + 5]);
			vertices.Add (vertices [mark + 7]);
			vertices.Add (vertices [mark + 4]);
			vertices.Add (vertices [mark + 6]);

			addSquareFace (triangles, vertices.Count);
		}
		// end door face(s)
		// end left faces

		// top face
		vertices.Add(vertices[0]);
		vertices.Add(vertices[1]);
		vertices.Add(vertices[2]);
		vertices.Add(vertices[3]);

		addSquareFace (triangles, vertices.Count);
		// end top face

		// bottom faces
		// door faces
		for (int k = 0; k < doorCount; k++) {
			mark = 8 + 8 * k;

			vertices.Add (vertices [mark + 5]);
			vertices.Add (vertices [mark + 4]);
			vertices.Add (vertices [mark + 3]);
			vertices.Add (vertices [mark + 2]);

			addSquareFace (triangles, vertices.Count);

			vertices.Add (vertices [mark + 1]);
			vertices.Add (vertices [mark]);
			if (k == 0)
				mark = 6;
			vertices.Add (vertices [mark - 1]);
			vertices.Add (vertices [mark - 2]);

			addSquareFace (triangles, vertices.Count);
		}
		// end door faces

		mark = 8 + 8 * (doorCount - 1);

		vertices.Add (vertices [7]);
		vertices.Add (vertices [6]);
		vertices.Add (vertices [mark + 7]);
		vertices.Add (vertices [mark + 6]);

		addSquareFace (triangles, vertices.Count);
		// end bottom faces
	}
	private void addDoorFace(List<int> triangles, int doorIndex){
		int mark = 8 + 8 * doorIndex;

		triangles.Add (mark + 3);
		triangles.Add (mark + 1);
		triangles.Add (mark - 1);

		triangles.Add (mark - 1);
		triangles.Add (mark - 3);
		triangles.Add (mark + 3);
	}
	private void addSquareFace(List<int> triangles, int mark){
		mark -= 4;

		triangles.Add (mark);
		triangles.Add (mark + 1);
		triangles.Add (mark + 3);

		triangles.Add (mark + 3);
		triangles.Add (mark + 2);
		triangles.Add (mark);
	}
	private List<Vector3> compileVertices (Vector3 wallLoc, Vector3[] doorLocs){
		List<Vector3> ans = new List<Vector3> ();
		List<Vector3> overlapLocs = new List<Vector3> ();
		doorCount = 0;

		ans.Add (wallLoc + new Vector3 (wallSize / 2, wallLoc.y, 0.125f));// index: 0
		ans.Add(ans[0] + new Vector3 (0, 0, -0.25f));// index: 1
		ans.Add(ans[0] + new Vector3 (-(wallSize - 0.25f), 0, 0));// index: 2
		ans.Add(ans[1] + new Vector3 (-(wallSize - 0.25f), 0, 0));// index: 3

		ans.Add (ans [2] + new Vector3 (0, -2 * wallLoc.y, 0));// index: 4
		ans.Add (ans [3] + new Vector3 (0, -2 * wallLoc.y, 0));// index: 5
		ans.Add (ans [0] + new Vector3 (0, -2 * wallLoc.y, 0));// index: 6
		ans.Add (ans [1] + new Vector3 (0, -2 * wallLoc.y, 0));// index: 7

		int mark = ans.Count;
		Vector3 doorCentre;
		foreach (Vector3 doorLoc in doorLocs) {
			doorCentre = doorLoc + wallLoc + new Vector3 (0, -wallLoc.y, 0.125f);
			if(checkDoorPlacement(doorCentre, overlapLocs)){
				ans.Add (doorCentre + new Vector3 (-1, 0, 0));// index: mark
				ans.Add (ans [mark] + new Vector3 (0, 0, -0.25f));// index: mark + 1
				ans.Add (ans [mark] + new Vector3 (0, 4, 0));// index: mark + 2
				ans.Add (ans [mark + 1] + new Vector3 (0, 4, 0));// index: mark + 3

				ans.Add (ans [mark + 2] + new Vector3 (2, 0, 0));// index: mark + 4
				ans.Add (ans [mark + 3] + new Vector3 (2, 0, 0));// index: mark + 5
				ans.Add (ans [mark] + new Vector3 (2, 0, 0));// index: mark + 6
				ans.Add (ans [mark + 1] + new Vector3 (2, 0, 0));// index: mark + 7

				doorCount++;
				overlapLocs.Add (doorCentre);
				mark = ans.Count;
			}
		}

		return ans;
	}
	private bool checkDoorPlacement(Vector3 doorLoc, List<Vector3> overlapLocs){
		foreach (Vector3 existingLoc in overlapLocs) {
			float dist = Mathf.Abs(Vector3.Distance (existingLoc, doorLoc));
			if (dist < 2.25f) {
				Debug.LogError ("The door at {"+doorLoc.x+"} is too close to an existing door at {"+existingLoc.x+"}.");
				return false;
			}
		}
		return true;
	}
	private Vector3[] resizeVectors(List<Vector3> vertices, Vector3 scale, Vector3 translation){
		Vector3[] ans = new Vector3[vertices.Count];
		Vector3 s = new Vector3(1/scale.x,1/scale.y,1/scale.z);
		Vector3 v;
		for (int k = 0; k < ans.Length; k++){
			v = vertices [k] - translation;
			v.Scale (s);
			ans [k] = v;
		}
		return ans;
	}
	// Update is called once per frame
	void Update () {

	}
}