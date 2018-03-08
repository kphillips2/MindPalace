using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorBuilder : MonoBehaviour {
	public GameObject room;

    private PictureCreator pictureCreator;
	private Building building;
    private GameObject component;

    // Use this for initialization
    void Start () {
        pictureCreator = new PictureCreator();
		building = GetComponentInParent<Building> (); 

		building.addRoom (new Vector3 (0, 0, 0));
        int[] DummyDoors = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        string[] DummyMat = { "", "", "" };
        SaveLoad.currentLoci.addRoom(new Room(DummyDoors, new Vector3(0,0,0), DummyMat));

    }

	public GameObject addRoom(Vector3 roomCentre){
        component = Instantiate(
			room,
            roomCentre,
            Quaternion.Euler(0, 0, 0)
        ) as GameObject;
        component.SetActive(true);
        return component;
    }

    public GameObject addCorridorAlongZ(Vector3 roomCentre)
    {
        print(roomCentre);
        GameObject component = Instantiate(
            room,
            roomCentre,
            Quaternion.Euler(0, 0, 0)
        ) as GameObject;
        component.SetActive(true);
        component.GetComponent<RoomBuilder>().setRoomSize(4, 24);
        return component;

    }

    public GameObject addCorridorAlongX(Vector3 roomCentre)
    {
        print(roomCentre);
        GameObject component = Instantiate(
            room,
            roomCentre,
            Quaternion.Euler(0, 0, 0)
        ) as GameObject;
        component.SetActive(true);
        component.GetComponent<RoomBuilder>().setRoomSize(24, 4);
        return component;

    }


    // Update is called once per frame
    void Update () {

	}
}