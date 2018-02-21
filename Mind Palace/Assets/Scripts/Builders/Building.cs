using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
	public GameObject space;

	private GameObject currentRoom;
	private RoomBuilder roomScript;
	private PictureCreator pictureCreator;
	private GameObject component;

	// Use this for initialization
	void Start () {
		pictureCreator = new PictureCreator();
		roomScript = space.GetComponent<RoomBuilder> ();

		roomScript.setMaterials(
			"Wood Texture 06", // floor material
			"Wood Texture 15", // roof material
			"Wood texture 12"  // wall material
		);
		//addRoom(new Vector3(0,0,0));
		roomScript.addDoor (1, -4f);
		roomScript.addDoor (1, 4f);
	}

	public GameObject addRoom(Vector3 roomCentre){
		component = Instantiate(
			space,
			roomCentre,
			Quaternion.Euler(0, 0, 0)
		) as GameObject;
		component.SetActive(true);
		return component;
	}


	// Update is called once per frame
	void Update () {

	}
}