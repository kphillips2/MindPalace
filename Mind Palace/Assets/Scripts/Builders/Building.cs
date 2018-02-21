using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
	public GameObject room;
	private List<GameObject> rooms;

	private GameObject currentRoom;
	private RoomBuilder roomScript;
	private PictureCreator pictureCreator;
	private GameObject component;

	// Use this for initialization
	void Start () {
		pictureCreator = new PictureCreator();
		rooms = new List<GameObject> ();
		roomScript = room.GetComponent<RoomBuilder> ();

		roomScript.setMaterials(
			"Wood Texture 06", // floor material
			"Wood Texture 15", // roof material
			"Wood texture 12"  // wall material
		);
		addRoom (new Vector3 (0, 0, 12));
		addRoom (new Vector3 (0, 0, -12));
		roomScript = rooms [0].GetComponent<RoomBuilder> ();
		roomScript.addDoor (0, -4f);
		roomScript.addDoor (0, 0f);
		roomScript.addDoor (0, 4f);

		roomScript.addDoor (1, -4f);
		roomScript.addDoor (1, 4f);

		roomScript.addDoor (2, -4f);

		roomScript.addDoor (3, 4f);

		roomScript = rooms [1].GetComponent<RoomBuilder> ();
		roomScript.addDoor (0, -4f);

		//roomScript.addDoor (0, 2.25f);
		//roomScript.addDoor (0, -2.5f);
		//roomScript.addDoor (0, 5f);
		//roomScript.addDoor (5, 2.5f);
	}

	public void addRoom(Vector3 roomCentre){
		component = Instantiate (
			room,
			roomCentre,
			Quaternion.Euler (0, 0, 0)
		) as GameObject;
		component.SetActive (true);
		rooms.Add(component);
	}
	public void setMaterials(int index){
	}

	// Update is called once per frame
	void Update () {

	}
}