using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
	public GameObject room;

    private List<GameObject> rooms;

	// Use this for initialization
	void Start (){
        rooms = new List<GameObject>();
        if (SaveFile.name == null)
            AddRoom(new Vector3(0, 0, 0));
        else
            LoadRooms (SaveFile.currentLoci.getRooms ());
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
        float[] chk, dims = GetDimensions (type);
        RoomData current;

        foreach (GameObject room in rooms) {
            current = room.GetComponent<RoomHandler> ().GetData ();
            chk = current.GetCentre ();

            distX = current.GetWidth () / 2 + dims [0] / 2;
            distZ = current.GetLength () / 2 + dims [1] / 2;

            inX = centre.x > chk [0] - distX && centre.x < chk [0] + distX;
            inZ = centre.z > chk [2] - distZ && centre.z < chk [2] + distZ;

            if (inX && inZ)
                return false;
        }
        return true;
    }
    /// <summary>
    /// Checks whether a potential new door or window goes into an existing room.
    /// </summary>
    /// <param name="centre"> the vector for the centre of the new room </param>
    /// <param name="loc"> the vector for the centre of the new door or window </param>
    /// <param name="type"> determines which room dimensions to use </param>
    /// <returns> the room object that is adjacent to the new door or window</returns>
    public GameObject CheckDoorWindowPlacement (Vector3 centre, Vector3 loc, string type){
        bool inX, inZ;
        float[] chk, dims = GetDimensions (type);
        float distX = dims[0] / 2, distZ = dims [1] / 2;

        Vector3 newLoc =
            // loc lies on positive X wall
            (loc.x > centre.x + distX - 0.5f) ? new Vector3 (centre.x + distX + 0.125f, loc.y, loc.z) :
            // loc lies on negative Z wall
            (loc.z > centre.z + distZ - 0.5f) ? new Vector3 (loc.x, loc.y, centre.z + distZ + 0.125f) :
            // loc lies on negative X wall
            (loc.x < centre.x - distX + 0.5f) ? new Vector3 (centre.x - distX - 0.125f, loc.y, loc.z) :
            // loc must lie on positive Z wall
            new Vector3 (loc.x, loc.y, centre.z - distZ - 0.125f);

        RoomData current;
        foreach (GameObject room in rooms) {
            current = room.GetComponent<RoomHandler> ().GetData ();
            chk = current.GetCentre();
            if(centre.x == chk [0] && centre.y == chk[1] && centre.z == chk[2])
                continue; 

            distX = current.GetWidth() / 2;
            distZ = current.GetLength() / 2;

            inX = newLoc.x >= chk [0] - distX && newLoc.x <= chk [0] + distX;
            inZ = newLoc.z >= chk [2] - distZ && newLoc.z <= chk [2] + distZ;

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
    /// <returns> the new room object that has been created</returns>
    private GameObject InstantiateRoom(Vector3 roomCentre, string type){
        GameObject component = Instantiate (
            room,
            roomCentre,
            Quaternion.Euler (0, 0, 0)
        ) as GameObject;
        component.SetActive (true);

        float[] dims = GetDimensions (type);
        component.GetComponent<RoomHandler> ().InitData (dims [0], dims [1]);
        rooms.Add (component);
        return component;
    }
    /// <summary>
    /// Contains all the possible dimensions of different room sizes.
    /// </summary>
    /// <param name="type"> determines which room dimensions to use </param>
    /// <returns> a float array of width and length </returns>
    private float[] GetDimensions(string type){
        switch (type) {
            case "room":
                return new float[] { 12, 12 };
            case "xCorridor":
                return new float[] { 24, 4 };
            case "zCorridor":
                return new float[] { 4, 24 };
            default:
                return new float[] { -1, -1 };
        }
    }
    private GameObject LoadRoom(Vector3 centre, float width, float length){
        if (width == 12 && length == 12)
            return AddRoom (centre);
        else if (width == 24 && length == 4)
            return AddXCorridor (centre);
        else if (width == 4 && length == 24)
            return AddZCorridor (centre);
        return null;
    }
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

            mats = data.GetMaterials ();
            component.GetComponent<RoomHandler> ().SetMaterials (mats [0], mats [1], mats [2]);
            wallData = data.GetWallData();
            component.GetComponent<RoomHandler> ().SetWallData (wallData);
        }
    }
}