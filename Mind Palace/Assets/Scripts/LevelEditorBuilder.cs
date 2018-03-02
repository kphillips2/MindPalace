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
		//oldCorridor = corridor.GetComponent<OldCorridor> ();
        pictureCreator = new PictureCreator();
		building = GetComponentInParent<Building> (); 
		//roomCreator = room.GetComponent<RoomCreator> ();
        /*
		// input: index, loc (-6)------------(6)
		roomCreator.addDoor (0, -3);
		roomCreator.addDoor (1, 0);
		//roomCreator.addDoor (2, -3);
		roomCreator.addDoor (3, 3);
        */

		//roomCreator.setMaterials(
		//	"Wood Texture 06", // floor material
		//	"Wood Texture 15", // roof material
		//	"Wood texture 12"  // wall material
		//);
        ////roomBuilder.addDoor(1, -3);
        ////roomBuilder.addDoor(1, 3);
        //GameObject FirstRoom =addRoom(new Vector3(0,0,0));

		building.addRoom (new Vector3 (0, 0, 0));
        
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


	// Update is called once per frame
	void Update () {

	}
}