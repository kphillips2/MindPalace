using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
	public GameObject room;
	private List<GameObject> rooms;
	private List<GameObject> corridors;

	private RoomBuilder roomScript;

	// Use this for initialization
	void Awake () {
		roomScript = room.GetComponent<RoomBuilder> ();
		rooms = new List<GameObject> ();
		corridors = new List<GameObject> ();

		roomScript.setMaterials(
			"Wood Texture 06", // floor material
			"Wood Texture 15", // roof material
			"Wood texture 12"  // wall material
		);

		//addRoom (new Vector3 (0, 0, 12));
		//roomScript = rooms [0].GetComponent<RoomBuilder> ();
		//roomScript.addDoor (0, -4f);
		//roomScript.addDoor (0, 0f);
		//roomScript.addDoor (0, 4f);
		//
		//roomScript.addDoor (1, -4f);
		//roomScript.addDoor (1, 4f);
		//
		//roomScript.addDoor (2, 4f);
		//
		//roomScript.addDoor (3, -4f);
		//
		//addRoom (new Vector3 (0, 0, 0));
		//roomScript = rooms [1].GetComponent<RoomBuilder> ();
		//roomScript.addDoor (0, -4f);
		//
		//roomScript.addDoor (2, 0f);
		//
		//addRoom (new Vector3 (0, 0, -12));
		//roomScript = rooms [2].GetComponent<RoomBuilder> ();
		//roomScript.addDoor (0, 0f);
		//
		//roomScript.addDoor (3, -4.5f);
		//roomScript.addDoor (3, -2.25f);
		//roomScript.addDoor (3, 4.5f);
		//roomScript.addDoor (3, 2.25f);
		//roomScript.addDoor (3, 0f);
		//
		//addCorridor (new Vector3 (0, 0, 30));
		//roomScript = corridors [0].GetComponent<RoomBuilder> ();
		//roomScript.addDoor (2, 0f);
	}

	public void addRoom(Vector3 roomCentre){
		GameObject component = Instantiate (
			room,
			roomCentre,
			Quaternion.Euler (0, 0, 0)
		) as GameObject;
		component.SetActive (true);
		component.GetComponent<RoomBuilder> ().setRoomSize (12, 12);
		rooms.Add(component);
	}
    public void addCorridorAlongZ(Vector3 roomCentre){
        GameObject component = Instantiate(
            room,
            roomCentre,
            Quaternion.Euler(0, 0, 0)
        ) as GameObject;
        component.SetActive (true);
        component.GetComponent<RoomBuilder>().setRoomSize(4, 24);
        corridors.Add (component);
    }
    public void addCorridorAlongX(Vector3 roomCentre){
		GameObject component = Instantiate (
			room,
			roomCentre,
			Quaternion.Euler (0, 0, 0)
		) as GameObject;
		component.SetActive (true);
		component.GetComponent<RoomBuilder> ().setRoomSize (4, 24);
		corridors.Add (component);
	}
    public void setMaterials(int index){
	}

	// Update is called once per frame
	void Update () {

	}
}