using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomResizer : MonoBehaviour {
	// Use this for initialization
	void Awake () {
	}
	// input: width is the distance in the x, length is the distance in the z
	public void resize(GameObject input, float width, float length){
		foreach (Collider collider in input.GetComponentsInChildren<Collider>())
			Destroy (collider);
		// get new vertices after scaling
		List<Vector3> vertices = compileVertices (input.transform.localPosition, width, length);
		// set triangles and and new vertices
		List<int> triangles = new List<int>();
		compileTriangles (triangles, vertices);
		// resize the vertices to remove scaling
		Vector3[] vectors = resizeVectors (vertices, input.transform.localScale, input.transform.localPosition);
		// create an list for the new UV map
		List<Vector2> uvs = new List<Vector2> ();
		for (int k = 0; k < vectors.Length; k++)
			uvs.Add (new Vector2 (vectors [k].x, vectors [k].z));

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
		// top face
		triangles.Add (0);
		triangles.Add (1);
		triangles.Add (3);

		triangles.Add (3);
		triangles.Add (2);
		triangles.Add (0);
		// end top face

		// bottom face
		triangles.Add (5);
		triangles.Add (4);
		triangles.Add (6);

		triangles.Add (6);
		triangles.Add (7);
		triangles.Add (5);
		// end bottom face

		// right face
		triangles.Add (0);
		triangles.Add (4);
		triangles.Add (5);

		triangles.Add (5);
		triangles.Add (1);
		triangles.Add (0);
		// end right face

		// left face
		triangles.Add (3);
		triangles.Add (7);
		triangles.Add (6);

		triangles.Add (6);
		triangles.Add (2);
		triangles.Add (3);
		// end left face

		// close face
		triangles.Add (1);
		triangles.Add (5);
		triangles.Add (7);

		triangles.Add (7);
		triangles.Add (3);
		triangles.Add (1);
		// end close face

		// far face
		triangles.Add (2);
		triangles.Add (6);
		triangles.Add (4);

		triangles.Add (4);
		triangles.Add (0);
		triangles.Add (2);
		// end far face
	}
	// returns a list of vertices with the new size
	private List<Vector3> compileVertices (Vector3 wallLoc, float width, float length){
		List<Vector3> ans = new List<Vector3> ();

		ans.Add (wallLoc + new Vector3 (width/2, 0.125f, length/2));// index: 0
		ans.Add (ans[0] + new Vector3 (0, 0, -length));// index: 1
		ans.Add (ans[0] + new Vector3 (-width, 0, 0));// index: 2
		ans.Add (ans[1] + new Vector3 (-width, 0, 0));// index: 3

		ans.Add (ans [0] + new Vector3 (0, -0.25f, 0));// index: 4
		ans.Add (ans [1] + new Vector3 (0, -0.25f, 0));// index: 5
		ans.Add (ans [2] + new Vector3 (0, -0.25f, 0));// index: 6
		ans.Add (ans [3] + new Vector3 (0, -0.25f, 0));// index: 7

		return ans;
	}
	// removes the scale and translation from the given list and returns the resulting vectors as an array
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