using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Resonsible for loading any room objects. Also tracks all rooms.
/// </summary>
public class Building : MonoBehaviour {
	public GameObject room;
    public GameObject plusSign;

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
        float[] chk, dims = RoomTypes.GetDimensions (type);
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
    /// <param name="centre"> the vector for the centre of the room </param>
    /// <param name="loc"> the vector for the centre of the new door or window </param>
    /// <param name="type"> determines which room dimensions to use </param>
    /// <returns> the room object that is adjacent to the new door or window </returns>
    public GameObject CheckDoorWindowPlacement (Vector3 centre, Vector3 loc, string type){
        bool inX, inZ;
        float[] chk, dims = RoomTypes.GetDimensions (type);
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

        RoomHandler roomScript = component.GetComponent<RoomHandler> ();
        roomScript.InitData (width, length);

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
                Quaternion.Euler(0, 0, 0)
            ) as GameObject;
            centre = plus.GetCentre ();
            component.transform.Translate (new Vector3 (centre [0], centre [1], centre [2]));
            component.transform.rotation = Quaternion.Euler (0, plus.GetAngle(), 0);
            component.SetActive (true);
        }
    }
}