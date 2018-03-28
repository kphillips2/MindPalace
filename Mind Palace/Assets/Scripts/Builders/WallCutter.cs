using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallCutter {
	private static int doorWindowCount;
	private static bool doesNewestOverlap;

    /// <summary>
    /// Cuts all doors and windows into a given wall object.
    /// </summary>
    /// <param name="input"> the wall object being changed </param>
    /// <param name="cutLocs"> the locations of all doors and windows </param>
    /// <param name="size"> the length of the wall </param>
    /// <returns></returns>
	public static bool cutDoorsAndWindows(GameObject input, Vector3[] cutLocs, float size){
		doesNewestOverlap = false;
		foreach (Collider collider in input.GetComponentsInChildren<Collider>())
			Object.Destroy (collider);
		// get new vertices after scaling
		List<Vector3> vertices = compileVertices (input.transform.localPosition, cutLocs, size);
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

		return doesNewestOverlap;
	}
    /// <summary>
    /// Compiles the triangles needed for each surface on the wall.
    /// </summary>
    /// <param name="triangles"> all the tiangles of the wall </param>
    /// <param name="vertices"> all the vertices of the wall </param>
	private static void compileTriangles(List<int> triangles, List<Vector3> vertices){
		if (doorWindowCount > 0)
			addFrontFace (triangles, vertices);
		else {
			triangles.Add (1);
			triangles.Add (7);
			triangles.Add (5);

			triangles.Add (5);
			triangles.Add (3);
			triangles.Add (1);
		}

		// far face
		for (int k = triangles.Count-1; k > 0; k-=3) {
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

		// door and window face(s)
		int mark;
		for (int k = 0; k < doorWindowCount; k++) {
			mark = 8 + 8 * k;

			vertices.Add (vertices [mark + 2]);
			vertices.Add (vertices [mark]);
			vertices.Add (vertices [mark + 3]);
			vertices.Add (vertices [mark + 1]);

			addSquareFace (triangles, vertices.Count);
		}
        // end door and window face(s)
        // end right faces

        // left faces
        vertices.Add (vertices[3]);
		vertices.Add (vertices[5]);
		vertices.Add (vertices[2]);
		vertices.Add (vertices[4]);

		addSquareFace (triangles, vertices.Count);

        // door and window face(s)
        for (int k = 0; k < doorWindowCount; k++) {
			mark = 8 + 8 * k;

			vertices.Add (vertices [mark + 5]);
			vertices.Add (vertices [mark + 7]);
			vertices.Add (vertices [mark + 4]);
			vertices.Add (vertices [mark + 6]);

			addSquareFace (triangles, vertices.Count);
		}
        // end door and window face(s)
        // end left faces

        // top face
        vertices.Add (vertices[0]);
		vertices.Add (vertices[1]);
		vertices.Add (vertices[2]);
		vertices.Add (vertices[3]);

		addSquareFace (triangles, vertices.Count);

        // window faces
        for (int k = 0; k < doorWindowCount; k++) {
            mark = 8 + 8 * k;
            if (vertices[mark].y > 0) {
                vertices.Add (vertices[mark + 6]);
                vertices.Add (vertices[mark + 7]);
                vertices.Add (vertices[mark]);
                vertices.Add (vertices[mark + 1]);

                addSquareFace(triangles, vertices.Count);
            }
        }
        // end window faces
        // end top face

        // bottom faces
        // door and window faces
        Vector3[] previous = { vertices [5], vertices [4] };
        for (int k = 0; k < doorWindowCount; k++) {
			mark = 8 + 8 * k;

			vertices.Add (vertices [mark + 5]);
			vertices.Add (vertices [mark + 4]);
			vertices.Add (vertices [mark + 3]);
			vertices.Add (vertices [mark + 2]);

			addSquareFace (triangles, vertices.Count);

            if (vertices[mark].y == 0){
                vertices.Add (vertices [mark + 1]);
                vertices.Add (vertices [mark]);
                vertices.Add (previous [0]);
                vertices.Add (previous [1]);

                addSquareFace(triangles, vertices.Count);

                previous[0] = vertices [mark + 7];
                previous[1] = vertices [mark + 6];
            }
		}
        // end door and window faces

        if (doorWindowCount > 0) {
			mark = 8 + 8 * (doorWindowCount - 1);

			vertices.Add (vertices [7]);
			vertices.Add (vertices [6]);
            vertices.Add (previous [0]);
            vertices.Add (previous [1]);

            addSquareFace (triangles, vertices.Count);
		} else {
			triangles.Add (7);
			triangles.Add (6);
			triangles.Add (4);

			triangles.Add (4);
			triangles.Add (5);
			triangles.Add (7);
		}
		// end bottom faces
	}
    /// <summary>
    /// Adds the triangles needed for the face toward the room's centre.
    /// </summary>
    /// <param name="triangles"> all the triangles of the wall </param>
    /// <param name="vertices"> all the vertices of the wall </param>
    private static void addFrontFace(List<int> triangles, List<Vector3> vertices)
    {
        // close face
        int[] previous = { 3, 5 };
        int mark;
        for (int k = 0; k < doorWindowCount; k++){
            mark = 8 + 8 * k;
            if (vertices [mark].y == 0)
                addDoorFace(triangles, mark, previous);
            else
                addWindowFace(triangles, vertices, mark, previous);
        }

        triangles.Add (11);
        triangles.Add (3);
        triangles.Add (1);

        triangles.Add (1);
        triangles.Add (previous [0]);
        triangles.Add (11);

        triangles.Add (previous [0]);
        triangles.Add (1);
        triangles.Add (7);

        triangles.Add (7);
        triangles.Add (previous [1]);
        triangles.Add (previous [0]);
        // end close face
    }
    /// <summary>
    /// Adds the triangles for the face to the left of a door.
    /// </summary>
    /// <param name="triangles"> all the triangles of the wall </param>
    /// <param name="mark"> the index within vertices that the door starts at </param>
    /// <param name="previous"> the indices that connect to this door </param>
	private static void addDoorFace(List<int> triangles, int mark, int[] previous){
		triangles.Add (mark + 3);
		triangles.Add (mark + 1);
		triangles.Add (previous [1]);

		triangles.Add (previous [1]);
		triangles.Add (previous [0]);
		triangles.Add (mark + 3);

        previous [0] = mark + 5;
        previous [1] = mark + 7;
    }
    /// <summary>
    /// Adds the triangles for the faces to the left and below a window.
    /// </summary>
    /// <param name="triangles"> all the triangles of the wall </param>
    /// <param name="vertices"> all the vertices of the wall </param>
    /// <param name="mark"> the index within vertices that the window starts at </param>
    /// <param name="previous"> the indices that connect to this window </param>
    private static void addWindowFace(List<int> triangles, List<Vector3> vertices, int mark, int[] previous){
        triangles.Add (mark + 3);
        triangles.Add (mark + 1);
        triangles.Add (previous [1]);

        triangles.Add (previous [1]);
        triangles.Add (previous [0]);
        triangles.Add (mark + 3);

        int toGround = vertices.Count;
        vertices.Add (new Vector3 (vertices [mark + 6].x, 0, vertices [mark + 6].z));
        vertices.Add (new Vector3 (vertices [mark + 7].x, 0, vertices [mark + 7].z));

        triangles.Add (mark + 7);
        triangles.Add (toGround + 1);
        triangles.Add (previous [1]);

        triangles.Add (previous [1]);
        triangles.Add (mark + 1);
        triangles.Add (mark + 7);

        previous[0] = mark + 5;
        previous[1] = toGround + 1;
    }
    /// <summary>
    /// Adds the triangles for a face using the four indices in front a given index.
    /// </summary>
    /// <param name="triangles"> all the triangles of the wall </param>
    /// <param name="mark"> the index at the end of the four indices to be used </param>
    private static void addSquareFace(List<int> triangles, int mark){
		mark -= 4;

		triangles.Add (mark);
		triangles.Add (mark + 1);
		triangles.Add (mark + 3);

		triangles.Add (mark + 3);
		triangles.Add (mark + 2);
		triangles.Add (mark);
	}
    /// <summary>
    /// Compiles the vertices needed for the wall including any doors and windows.
    /// </summary>
    /// <param name="wallLoc"> the vector for the centre of the wall </param>
    /// <param name="cutLocs"> the locations of all doors and windows </param>
    /// <param name="wallSize"> the length of the wall </param>
    /// <returns> the list of compiled vertices </returns>
    private static List<Vector3> compileVertices(Vector3 wallLoc, Vector3[] cutLocs, float wallSize){
		List<Vector3> ans = new List<Vector3> ();
		List<Vector3> existingLocs = new List<Vector3> ();
        doorWindowCount = 0;

        ans.Add (wallLoc + new Vector3 (wallSize / 2, wallLoc.y, 0.125f));// index: 0
		ans.Add (ans[0] + new Vector3 (0, 0, -0.25f));// index: 1
		ans.Add (ans[0] + new Vector3 (-(wallSize - 0.25f), 0, 0));// index: 2
		ans.Add (ans[1] + new Vector3 (-(wallSize - 0.25f), 0, 0));// index: 3

		ans.Add (ans [2] + new Vector3 (0, -2 * wallLoc.y, 0));// index: 4
		ans.Add (ans [3] + new Vector3 (0, -2 * wallLoc.y, 0));// index: 5
		ans.Add (ans [0] + new Vector3 (0, -2 * wallLoc.y, 0));// index: 6
		ans.Add (ans [1] + new Vector3 (0, -2 * wallLoc.y, 0));// index: 7

		Vector3 cutCentre;
        for (int k = 0; k < cutLocs.Length; k++) {
            cutCentre = cutLocs[k] + new Vector3(wallLoc.x, 0, wallLoc.z + 0.125f);
            if (cutLocs[k].y > 0) {
                if (checkPlacement (cutCentre, existingLocs, 1.5f)) {
                    doorWindowCount++;
                    addCutVertices (ans, cutCentre, 3);
                    existingLocs.Add (cutCentre);
                } else {
                    if (k == cutLocs.Length - 1)
                        doesNewestOverlap = true;
                    Debug.LogError(
                        "The window at {" + cutCentre.x + "} is too close to an existing door or window."
                    );
                }
            } else if (checkPlacement (cutCentre, existingLocs, 1)) {
                doorWindowCount++;
                addCutVertices (ans, cutCentre, 2);
                existingLocs.Add (cutCentre);
            } else {
                if (k == cutLocs.Length - 1)
                    doesNewestOverlap = true;
                Debug.LogError(
                    "The door at {" + cutCentre.x + "} is too close to an existing door or window."
                );
            }
		}

		return ans;
	}
    /// <summary>
    /// Adds the vertices for a given door or window.
    /// </summary>
    /// <param name="verts"> all the veritices of the wall </param>
    /// <param name="centre"> the vector for the centre of the door or window </param>
    /// <param name="size"> the width of the door or window </param>
    private static void addCutVertices(List<Vector3> verts, Vector3 centre, float size){
        int mark = verts.Count;

        verts.Add (centre + new Vector3(-size / 2, 0, 0));// index: mark
        verts.Add (verts [mark] + new Vector3(0, 0, -0.25f));// index: mark + 1
        verts.Add (new Vector3(verts [mark].x, 4, verts [mark].z));// index: mark + 2
        verts.Add (new Vector3(verts [mark + 1].x, 4, verts [mark + 1].z));// index: mark + 3

        verts.Add (verts [mark + 2] + new Vector3(size, 0, 0));// index: mark + 4
        verts.Add (verts [mark + 3] + new Vector3(size, 0, 0));// index: mark + 5
        verts.Add (verts [mark] + new Vector3(size, 0, 0));// index: mark + 6
        verts.Add (verts [mark + 1] + new Vector3(size, 0, 0));// index: mark + 7
    }
    /// <summary>
    /// Checks whether a window or door location overlaps with any other doors or windows.
    /// </summary>
    /// <param name="newLoc"> the vector for the centre of the door or window </param>
    /// <param name="existingLocs"> the locations of all doors and windows already added to the wall </param>
    /// <param name="size"> half the width of the door or window </param>
    /// <returns></returns>
    private static bool checkPlacement(Vector3 newLoc, List<Vector3> existingLocs, float size){
        float dist, minDist;
        foreach (Vector3 existingLoc in existingLocs) {
			dist = Mathf.Abs (Vector3.Distance (
                new Vector3(existingLoc.x, 0, existingLoc.z), new Vector3(newLoc.x, 0, newLoc.z)
            ));
            minDist = size + 0.25f + 1;
            if (existingLoc.y > 0)
                minDist += 0.5f;
            if (dist < minDist)
                return false;
		}
		return true;
	}
    /// <summary>
    /// Removes the scale and translation from the given list and returns the resulting vectors as an array.
    /// </summary>
    /// <param name="vertices"> all the vertices of the wall </param>
    /// <param name="scale"> the scale to be removed </param>
    /// <param name="translation"> the translation to be removed </param>
    /// <returns> an array of the resulting vertices </returns>
    private static Vector3[] resizeVectors(List<Vector3> vertices, Vector3 scale, Vector3 translation){
		Vector3[] ans = new Vector3[vertices.Count];
		Vector3 s = new Vector3(1 / scale.x, 1 / scale.y, 1 / scale.z);
		Vector3 v;
		for (int k = 0; k < ans.Length; k++){
			v = vertices [k] - translation;
			v.Scale (s);
			ans [k] = v;
		}
		return ans;
	}
}