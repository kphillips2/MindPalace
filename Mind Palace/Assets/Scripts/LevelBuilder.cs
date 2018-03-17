using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {
	public GameObject room;
	public GameObject corridor;
	public GameObject space;

	private GameObject currentRoom;
	private OldRoom oldRoom;
	private OldCorridor oldCorridor;
	private RoomBuilder roomCreator;
	private PictureCreator pictureCreator;

	// Test loading the first saved Loci in the list of saved Loci files
	public void loadTest()
	{
		SaveLoad.load(); // Loads save file
		SaveLoad.currentLoci = SaveLoad.savedLocis[0]; // Sets first saved Loci to be the one being viewed

		// Place objects
		ObjectPlacer op = new ObjectPlacer();
		foreach (float[] item in SaveLoad.currentLoci.getObjects())
		{
			try
			{
				op.createPrefab((int)item[0], item[1], item[2], item[3], false);
			}
			catch { Debug.Log("Error: Object could not be generated due to missing values"); }
		}

		// Create rooms
		foreach (Room r in SaveLoad.currentLoci.getRooms())
		{
			string[] m = r.getMaterials();
			float[] ce = r.getCentre();
			int[] d = r.getRoomDoors();

			try
			{
				oldRoom.setMaterials(m[0], m[1], m[2]);
				addRoom(new Vector3(ce[0], ce[1], ce[2]), d);
				hangPictures(new Vector3(ce[0], ce[1], ce[2]), d);
			}
			catch { Debug.Log("Error: Room could not be generated due to missing values"); }
		}

		// Create corridors
		foreach (Corridor c in SaveLoad.currentLoci.getCorridors())
		{
			string[] m = c.getMaterials();
			float[] ce = c.getCentre();
			int[] d = c.getCorrDoors();

			try
			{
				oldCorridor.setMaterials(m[0], m[1], m[2]);
				addCorridor(new Vector3(ce[0], ce[1], ce[2]), d, c.getAngle());
			}
			catch { Debug.Log("Error: Corridor could not be generated due to missing values"); }
		}
    }

	// Use this for initialization
	void Start () {
		oldRoom = room.GetComponent<OldRoom>();
		oldCorridor = corridor.GetComponent<OldCorridor>();
		pictureCreator = new PictureCreator();

        //roomCreator = space.GetComponent<RoomCreator>();
        //roomCreator.addDoor (0, -3);
        //roomCreator.addDoor (1, 0);
        //roomCreator.addDoor (2, -3);
        //roomCreator.addDoor (3, 3);
        //
        //roomCreator.setMaterials (
        //	"Wood Texture 06", // floor material
        //	"Wood Texture 15", // roof material
        //	"Wood texture 12"  // wall material
        //);

        //loadTest();

        int[] roomDoors = {1, 1, 1, 0};
		Vector3 centre = new Vector3(0, 0, 0);

		oldRoom.setMaterials (
			"Wood Texture 06", // floor material
			"Wood Texture 15", // roof material
			"Wood texture 12"  // wall material
		);
		oldRoom.addFloor (centre);
		oldRoom.addRoof (centre);
		oldRoom.addWalls(
			centre,
			roomDoors
		);
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
		addCorridor (centre, corridorDoors, 90);
        addObjects();
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
			"Wood texture 06"  // wall material
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

    private void addObjects()
    {
        ObjectPlacer op = new ObjectPlacer();
        op.createPrefab(0, -3.468538f, -12.47951f, 0f, false); //Bed
        op.createPrefab(1, 4.15f, 8.72f, -90f, false); //Book cases
        op.createPrefab(1, 4.15f, 10.43f, -90f, false);
        op.createPrefab(1, -4.16f, -6.81f, 90f, false);
        op.createPrefab(1, -4.44f, 7.66f, 90f, false);
        op.createPrefab(2, 2.7956f, -3.76479f, 0f, false); //Chair
        op.createPrefab(3, 19.07f, -7.08f, 90f, false); //Chair2
        op.createPrefab(3, 21.93f, -8.543f, 270f, false);
        op.createPrefab(3, 19.07f, -8.543f, 90f, false);
        op.createPrefab(3, 21.92f, -7.08f, 270f, false);
        op.createPrefab(4, -1.18f, 12.39f, 90f, false); //Coffee table
        op.createPrefab(5, -3.47f, 12.44f, 90f, false); //Couches
        op.createPrefab(5, 1f, 12.24f, -90f, false);
        op.createPrefab(5, 1.31f, -9.26f, 90f, false);
        op.createPrefab(6, 16f, -7.22f, 90f, false); //Counters
        op.createPrefab(6, 16f, -8.52f, 90f, false);
        op.createPrefab(7, -4.09f, 0.05f, 90f, false); //Fireplace
        op.createPrefab(8, 4.0925f, -3.876518f, 0f, false); //Flower tables
        op.createPrefab(8, -3.865167f, 3.955207f, 0f, false);
        op.createPrefab(8, -3.643468f, -3.745927f, 0f, false);
        op.createPrefab(8, 4.133508f, 3.98855f, 0f, false);
        op.createPrefab(8, 34.03f, 10.99f, 0f, false);
        op.createPrefab(8, 33.83f, 3.35468f, 0f, false);
        op.createPrefab(9, 16.12f, -11.23f, 90f, false); //Fridge
        op.createPrefab(10, 0.3411262f, -14.19815f, 0f, false); //Guitar
        op.createPrefab(11, 3.07f, -14.07f, 0f, false); //Headboard
        op.createPrefab(12, 33.44f, -0.1f, -90f, false); //Lion statue
        op.createPrefab(13, 16.13f, -9.81f, 90f, false); //Oven
        op.createPrefab(14, 23.8f, -3.97f, 0f, false); //Plants
        op.createPrefab(14, 3.810411f, 13.44f, 0f, false);
        op.createPrefab(14, 3.810411f, 6.19f, 0f, false);
        op.createPrefab(15, 34.25f, 5.94f, -90f, false); //Sink
        op.createPrefab(16, 1.01692f, -12.85651f, 0f, false); //Soccer ball
        op.createPrefab(17, 20.4941f, -7.77f, 90f, false); //Table
        op.createPrefab(18, 34.13f, 7.88f, -90f, false); //Toilet
        op.createPrefab(19, 3.937f, -9.35f, -90f, false); //TV
    }


	// Update is called once per frame
	void Update () {

	}
}