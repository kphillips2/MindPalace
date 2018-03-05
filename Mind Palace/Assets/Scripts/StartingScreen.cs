using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScreen : MonoBehaviour {

    public GameObject room;
    private PictureCreator pictureCreator;
    private Building building;
    private GameObject component;
    private RoomBuilder roomTextureSetter;

    // Use this for initialization
    void Start () {

        pictureCreator = new PictureCreator();
        building = GetComponentInParent<Building>();
        building.addRoom(new Vector3(0, 0, 0));
        roomTextureSetter = room.GetComponent<RoomBuilder>();
        roomTextureSetter.setMaterials(
                "Wood Texture 15", // floor material
                "Wood Texture 12", // roof material
                "Wood texture 12"  // wall material
                 );
    }

    public GameObject addRoom(Vector3 roomCentre)
    {
        component = Instantiate(
            room,
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
