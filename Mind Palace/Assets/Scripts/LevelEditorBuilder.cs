using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorBuilder : MonoBehaviour {
	public GameObject room;
	public GameObject corridor;
	public GameObject space;

	private GameObject currentRoom;
	private OldRoom oldRoom;
	private OldCorridor oldCorridor;
	private RoomBuilder roomBuilder;
    private PictureCreator pictureCreator;

    // Use this for initialization
    void Start () {
		oldRoom = room.GetComponent<OldRoom>();
		oldCorridor = corridor.GetComponent<OldCorridor>();
        pictureCreator = new PictureCreator();

		roomBuilder = space.GetComponent<RoomBuilder>();
		// input: index, loc (-6)------------(6)
		//roomBuilder.addDoor (0, -3);
		//roomBuilder.addDoor (1, 0);
		//roomBuilder.addDoor (2, -3);
		roomBuilder.addDoor (1, 0);
		roomBuilder.addDoor (1, 3);

		roomBuilder.setMaterials (
			"Wood Texture 06", // floor material
			"Wood Texture 15", // roof material
			"Wood texture 12"  // wall material
		);
        
        int[] roomDoors = {1, 1, 1, 1};
		Vector3 centre = new Vector3(0, 0, 0);

		oldRoom.setMaterials (
			"Wood Texture 06", // floor material
			"Wood Texture 15", // roof material
			"Wood texture 12"  // wall material
		);
		oldRoom.addFloor (centre);
		oldRoom.addRoof (centre);
        /*
		oldRoom.addWalls(
			centre,
			roomDoors
		);
        */

        /*
        //add pictures
        hangPictures(centre, roomDoors);

		addRooms ();
        
        int[] corridorDoors = {
			0, 1, 1,	//top wall
			0,			//right end wall
			0, 1, 0,	//bottom wall
			1			//left end wall
		};
		centre = new Vector3 (20, 0, 0);
		oldCorridor.setMaterials (
			"Wood Texture 06", // floor material
			"Wood Texture 15", // roof material
			"Wood texture 12"  // wall material
		);
		oldCorridor.addFloor (
			centre, 0
		);
		oldCorridor.addRoof (
			centre, 0
		);
		oldCorridor.addWalls (
			centre, corridorDoors, 0
		);

		corridorDoors = new int[]{0, 0, 0, 1, 0, 1, 0, 1};
		centre = new Vector3 (20, 0, 17.5f);
		oldCorridor.setMaterials (
			"Bricks1", // floor material
			"Wood Texture 05", // roof material
			"Wood texture 06"  // wall material
		);
		addCorridor (centre, corridorDoors, 90);*/
    }

	private void addRooms(){
		int[] roomDoors = new int[]{0, 0, 1, 0};
		Vector3 centre = new Vector3(0, 0, 10);
		addRoom(centre, roomDoors);
		//add pictures
		hangPictures(centre, roomDoors);

		roomDoors = new int[]{1, 0, 0, 0};
		centre = new Vector3(0, 0, -10);
		addRoom(centre, roomDoors);
		//add pictures
		hangPictures(centre, roomDoors);

		roomDoors = new int[]{1, 0, 0, 0};
		centre = new Vector3(20, 0, -7.5f);
		addRoom(centre, roomDoors);
		//add pictures
		hangPictures(centre, roomDoors);

		oldRoom.setMaterials (
			"Bricks1", // floor material
			"Wood Texture 05", // roof material
			"Granite_01"  // wall material
		);

		roomDoors = new int[]{0, 0, 1, 0};
		centre = new Vector3(30, 0, 7.5f);
		addRoom(centre, roomDoors);
		//add pictures
		hangPictures(centre, roomDoors);

		roomDoors = new int[] { 0, 1, 0, 0 };
		centre = new Vector3(12.5f, 0, 17.5f);
		addRoom(centre, roomDoors);
		//add pictures
		hangPictures(centre, roomDoors);

		roomDoors = new int[] { 0, 0, 1, 0 };
		centre = new Vector3(20, 0, 37.5f);
		addRoom(centre, roomDoors);
		//add pictures
		hangPictures(centre, roomDoors);
	}
	private void addRoom(Vector3 roomCentre, int[] doorStates){
		oldRoom.addFloor (roomCentre);
		oldRoom.addRoof (roomCentre);
		oldRoom.addWalls (
			roomCentre,
			doorStates
		);
	}
	private void addCorridor(Vector3 roomCentre, int[] doorStates, int angle){
		oldCorridor.addFloor (
			roomCentre,
			angle
		);
		oldCorridor.addRoof (
			roomCentre,
			angle
		);
		oldCorridor.addWalls (
			roomCentre,
			doorStates,
			angle
		);
	}

    /**
     * Will later use wallSpaceFinder and PictureSpacePlacer 
     */
    private void hangPictures(Vector3 centre, int[] roomDoors)
    {
        string[] imageRes = { "Assets/Resources/treepic.jpg" , "Assets/Resources/tamarin.jpg", "Assets/Resources/seadragon.jpg",
                                "Assets/Resources/owl.jpg", "Assets/Resources/camel.JPG", "Assets/Resources/mountain.jpeg",
                                "Assets/Resources/shoreline.jpeg", "Assets/Resources/chalk.jpeg", "Assets/Resources/christmas.jpeg",
                                "Assets/Resources/glowsticks.jpeg", "Assets/Resources/donuts.jpeg", "Assets/Resources/sparkler.jpeg",
                                "Assets/Resources/valley.jpeg", "Assets/Resources/iceberg.jpeg", "Assets/Resources/space.jpeg",
                                "Assets/Resources/lamb.jpeg"};

        int randPic = Random.RandomRange(0, imageRes.Length);

        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 0:
                    if (roomDoors[0] == 1)
                    {
                        pictureCreator.placePicture(imageRes[randPic], 90f, centre + new Vector3(-(4.7f / 1.5f), 2.6f, 4.7f));
                        pictureCreator.placePicture(imageRes[(randPic+1)%imageRes.Length], 90f, centre + new Vector3((4.7f / 1.5f), 2.6f, 4.7f));
                    }
                    else
                    {
                        pictureCreator.placePicture(imageRes[(randPic + 0) % imageRes.Length], 90f, centre + new Vector3(-(4.7f / 1.5f), 2.6f, 4.7f));
                        pictureCreator.placePicture(imageRes[(randPic + 1) % imageRes.Length], 90f, centre + new Vector3(0, 2.6f, 4.7f));
                        pictureCreator.placePicture(imageRes[(randPic + 2) % imageRes.Length], 90f, centre + new Vector3((4.7f / 1.5f), 2.6f, 4.7f));
                    }
                    break;
                case 1:
                    if (roomDoors[1] == 1)
                    {
                        pictureCreator.placePicture(imageRes[(randPic + 3) % imageRes.Length], 180f, centre + new Vector3(4.7f, 2.6f, -(4.7f / 1.5f)));
                        pictureCreator.placePicture(imageRes[(randPic + 4) % imageRes.Length], 180f, centre + new Vector3(4.7f, 2.6f, (4.7f / 1.5f)));
                    }
                    else
                    {
                        pictureCreator.placePicture(imageRes[(randPic + 3) % imageRes.Length], 180f, centre + new Vector3(4.7f, 2.6f, -(4.7f / 1.5f)));
                        pictureCreator.placePicture(imageRes[(randPic + 4) % imageRes.Length], 180f, centre + new Vector3(4.7f, 2.6f, 0));
                        pictureCreator.placePicture(imageRes[(randPic + 5) % imageRes.Length], 180f, centre + new Vector3(4.7f, 2.6f, (4.7f / 1.5f)));
                    }
                    break;
                case 2:
                    if (roomDoors[2] == 1)
                    {
                        pictureCreator.placePicture(imageRes[(randPic + 6) % imageRes.Length], -90f, centre + new Vector3((4.7f / 1.5f), 2.6f, -4.7f));
                        pictureCreator.placePicture(imageRes[(randPic + 7) % imageRes.Length], -90f, centre + new Vector3(-(4.7f / 1.5f), 2.6f, -4.7f));
                    }
                    else
                    {
                        pictureCreator.placePicture(imageRes[(randPic + 6) % imageRes.Length], -90f, centre + new Vector3((4.7f / 1.5f), 2.6f, -4.7f));
                        pictureCreator.placePicture(imageRes[(randPic + 7) % imageRes.Length], -90f, centre + new Vector3(0, 2.6f, -4.7f));
                        pictureCreator.placePicture(imageRes[(randPic + 8) % imageRes.Length], -90f, centre + new Vector3(-(4.7f / 1.5f), 2.6f, -4.7f));
                    }
                    break;
                case 3:
                    if (roomDoors[3] == 1)
                    {
                        pictureCreator.placePicture(imageRes[(randPic + 9) % imageRes.Length], 0f, centre + new Vector3(-4.7f, 2.6f, (4.7f / 1.5f)));
                        pictureCreator.placePicture(imageRes[(randPic + 10) % imageRes.Length], 0f, centre + new Vector3(-4.7f, 2.6f, -(4.7f / 1.5f)));
                    }
                    else
                    {
                        pictureCreator.placePicture(imageRes[(randPic + 9) % imageRes.Length], 0f, centre + new Vector3(-4.7f, 2.6f, (4.7f / 1.5f)));
                        pictureCreator.placePicture(imageRes[(randPic + 10) % imageRes.Length], 0f, centre + new Vector3(-4.7f, 2.6f, 0f));
                        pictureCreator.placePicture(imageRes[(randPic + 11) % imageRes.Length], 0f, centre + new Vector3(-4.7f, 2.6f, -(4.7f / 1.5f)));
                    }
                    break;
            }
        }
        }


	// Update is called once per frame
	void Update () {

	}
}