using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Resonsible for loading any room objects. Also tracks all rooms.
/// </summary>
public class Building : MonoBehaviour {
	public GameObject room;
    public GameObject plusSign;

    private List<GameObject> rooms;

	// Use this for initialization
	void Start (){
        rooms = new List<GameObject> ();
        if (SaveFile.isNewLoci)
            AddRoom (new Vector3 (0, 0, 0));
        else { 
            LoadRooms (SaveFile.currentLoci.getRooms ());
            ObjectPlacer op = new ObjectPlacer();
            foreach (float[] o in SaveFile.currentLoci.getObjects())
                op.createPrefab((int) o[0], new Vector3(o[1], o[2], o[3]), new Vector3(o[4], o[5], o[6]), true);
        }
    }
    /// <summary>
    /// Saves all the rooms and corridors to the currently open Loci.
    /// </summary>
    public void Save(){
        SaveFile.currentLoci.clearRooms ();
        foreach (GameObject room in rooms)
            room.GetComponent<RoomHandler> ().Save ();
    }
    /// <summary>
    /// Checks whether a potential new room overlaps with any existing rooms.
    /// </summary>
    /// <param name="centre"> the vector for the centre of the new room </param>
    /// <param name="type"> determines which room dimensions to use </param>
    /// <returns> false if an existing room overlaps and true otherwise </returns>
    public bool CheckRoomPlacement (Vector3 centre, string type){
        float distX, distZ;
        bool inX, inZ;
        float[] chk, dims = RoomTypes.GetDimensions (type);
        RoomData current;

        foreach (GameObject room in rooms) {
            current = room.GetComponent<RoomHandler> ().GetData ();
            chk = current.GetCentre ();

            float width = current.GetWidth ();
            float length = current.GetLength ();
            float margin = 0.0001f;

            distX = width / 2 + dims [0] / 2 - margin;
            distZ = length / 2 + dims [1] / 2 - margin;

            inX = centre.x > chk [0] - distX && centre.x < chk [0] + distX;
            inZ = centre.z > chk [2] - distZ && centre.z < chk [2] + distZ;

            if (inX && inZ) {
                //string collideCentre = "<" + chk[0] + ", " + chk[1] + ", " + chk[2] + ">";
                //Debug.Log (
                //    "New room type : " + type + ", room collided with : " +
                //    "[width : " + width + ", length : " + length + ", centre : " + collideCentre + "]."
                //);
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// Checks whether a potential new door or window goes into an existing room.
    /// </summary>
    /// <param name="centre"> the vector for the centre of the room </param>
    /// <param name="loc"> the vector for the centre of the plus sign </param>
    /// <param name="width"> determines the size of the room along the X axis </param>
    /// <param name="length"> determines the size of the room along the Z axis </param>
    /// <returns> the room object that is adjacent to the new door or window </returns>
    public GameObject CheckDoorWindowPlacement (float[] centre, float[] loc, float width, float length){
        bool inX, inZ;
        float[] chk;
        float distX = width / 2, distZ = length / 2;
        float margin, diffX, diffY, diffZ;

        float[] newLoc =
            // loc lies on positive X wall
            (loc [0] > centre [0] + distX - 0.5f) ? new float[] { centre [0] + distX + 0.3f, loc [1], loc [2] } :
            // loc lies on negative Z wall
            (loc [2] < centre [2] - distZ + 0.5f) ? new float[] { loc [0], loc [1], centre [2] - distZ - 0.3f } :
            // loc lies on negative X wall
            (loc [0] < centre [0] - distX + 0.5f) ? new float[] { centre [0] - distX - 0.3f, loc [1], loc [2] } :
            // loc must lie on positive Z wall
            new float[] { loc [0], loc [1], centre [2] + distZ + 0.3f };

        RoomData current;
        foreach (GameObject room in rooms) {
            current = room.GetComponent<RoomHandler> ().GetData ();
            chk = current.GetCentre ();

            margin = 0.0001f;
            diffX = centre [0] - chk [0];
            diffY = centre [1] - chk [1];
            diffZ = centre [2] - chk [2];

            if (Math.Abs (diffX) < margin && Math.Abs (diffY) < margin && Math.Abs (diffZ) < margin)
                continue; 

            distX = current.GetWidth () / 2;
            distZ = current.GetLength () / 2;

            inX = newLoc [0] >= chk [0] - distX && newLoc [0] <= chk [0] + distX;
            inZ = newLoc [2] >= chk [2] - distZ && newLoc [2] <= chk [2] + distZ;

            if (inX && inZ)
                return room;
        }
        return null;
    }
    /// <summary>
    /// Adds a new room to the scene.
    /// Since we have one room size this will create a 12 wide and 12 long room.
    /// </summary>
    /// <param name="roomCentre"> the vector for the centre of the new room </param>
    /// <returns> the new room object that has been created </returns>
    public GameObject AddRoom(Vector3 roomCentre){
        return InstantiateRoom (roomCentre, "room");
	}
    /// <summary>
    /// Adds a new corridor to the scene.
    /// Since the corridor is a rectangle, it can align to the X or Z axis.
    /// This will create a 24 wide and 4 long corridor.
    /// </summary>
    /// <param name="roomCentre"> the vector for the centre of the new corridor </param>
    /// <returns> the new corridor object that has been created </returns>
    public GameObject AddXCorridor(Vector3 roomCentre){
        return InstantiateRoom (roomCentre, "xCorridor");
    }
    /// <summary>
    /// Adds a new corridor to the scene.
    /// Since the corridor is a rectangle, it can align to the X or Z axis.
    /// This will create a 4 wide and 24 long corridor.
    /// </summary>
    /// <param name="roomCentre"> the vector for the centre of the new corridor </param>
    /// <returns> the new corridor object that has been created </returns>
    public GameObject AddZCorridor(Vector3 roomCentre){
        return InstantiateRoom (roomCentre, "zCorridor");
    }
    /// <summary>
    /// Creates a clone of the room object inside the scene.
    /// </summary>
    /// <param name="roomCentre"> the vector for the centre of the new room </param>
    /// <param name="type"> determines which room dimensions to use </param>
    /// <returns> the new room object that has been created </returns>
    private GameObject InstantiateRoom(Vector3 roomCentre, string type){
        GameObject component = Instantiate (
            room,
            roomCentre,
            Quaternion.Euler (0, 0, 0)
        ) as GameObject;
        component.SetActive (true);

        float[] dims = RoomTypes.GetDimensions (type);
        RoomHandler roomScript = component.GetComponent<RoomHandler> ();
        roomScript.InitData (dims [0], dims [1]);
        roomScript.AddPlusSigns ();

        rooms.Add (component);
        return component;
    }
    /// <summary>
    /// Loads all the rooms and corridors from the list provided.
    /// </summary>
    /// <param name="savedRooms"> a list of room information </param>
    private void LoadRooms (List<RoomData> savedRooms){
        float width, length;
        float[] centre;
        string[] mats;
        List<float[]>[] wallData;
        GameObject component;

        foreach (RoomData data in savedRooms) {
            centre = data.GetCentre ();
            width = data.GetWidth ();
            length = data.GetLength ();
            component = LoadRoom (new Vector3 (centre [0], centre [1], centre [2]), width, length);
            LoadPlusSigns (data.GetPlusData ());

            mats = data.GetMaterials ();
            component.GetComponent<RoomHandler> ().SetMaterials (mats [0], mats [1], mats [2]);
            wallData = data.GetWallData ();
            component.GetComponent<RoomHandler> ().SetWallData (wallData);
        }
    }
    /// <summary>
    /// Creates a clone of the room object inside the scene.
    /// </summary>
    /// <param name="roomCentre"> the vector for the centre of the new room </param>
    /// <param name="width"> determines the size of the room along the X axis </param>
    /// <param name="length"> determines the size of the room along the Z axis </param>
    /// <returns> the new room object that has been created </returns>
    private GameObject LoadRoom(Vector3 roomCentre, float width, float length){
        GameObject component = Instantiate (
            room,
            roomCentre,
            Quaternion.Euler (0, 0, 0)
        ) as GameObject;
        component.SetActive (true);
        component.GetComponent<RoomHandler> ().InitData (width, length);

        rooms.Add (component);
        return component;
    }
    /// <summary>
    /// Loads all the plus signs from the list provided.
    /// </summary>
    /// <param name="data"> a list of plus sign information </param>
    private void LoadPlusSigns(List<PlusData> data){
        float[] centre;
        GameObject component;

        foreach (PlusData plus in data) {
            component = Instantiate (
                plusSign,
                Vector3.zero,
                Quaternion.Euler (0, 0, 0)
            ) as GameObject;
            centre = plus.GetCentre ();
            component.transform.Translate (new Vector3 (centre [0], centre [1], centre [2]));
            component.transform.rotation = Quaternion.Euler (0, plus.GetAngle (), 0);
            component.SetActive (true);
            component.GetComponent<SubMenuHandler> ().InitData (plus);
        }
    }
}