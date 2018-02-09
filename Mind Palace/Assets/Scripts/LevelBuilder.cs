using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {
	public GameObject room;
	public GameObject corridor;

	private GameObject currentRoom;
	private RoomBuilder roomBuilder;
	private CorridorBuilder corridorBuilder;
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
                op.createPrefab((int)item[0], item[1], item[2], item[3]);
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
                roomBuilder.setMaterials(m[0], m[1], m[2]);
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
                corridorBuilder.setMaterials(m[0], m[1], m[2]);
                addCorridor(new Vector3(ce[0], ce[1], ce[2]), d, c.getAngle());
            }
            catch { Debug.Log("Error: Corridor could not be generated due to missing values"); }
        }
    }

	// Use this for initialization
	void Start () {
		roomBuilder = room.GetComponent<RoomBuilder>();
		corridorBuilder = corridor.GetComponent<CorridorBuilder>();
        pictureCreator = new PictureCreator();

        loadTest(); 

        /*
        int[] roomDoors = {1, 1, 1, 0};
		Vector3 centre = new Vector3(0, 0, 0);

		roomBuilder.setMaterials (
			"Wood Texture 06", // floor material
			"Wood Texture 15", // roof material
			"Wood texture 12"  // wall material
		);
		roomBuilder.addFloor (centre);
		roomBuilder.addRoof (centre);
		roomBuilder.addWalls(
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
		corridorBuilder.setMaterials (
			"Wood Texture 06", // floor material
			"Wood Texture 15", // roof material
			"Wood texture 12"  // wall material
		);
		corridorBuilder.addFloor (
			centre, 0
		);
		corridorBuilder.addRoof (
			centre, 0
		);
		corridorBuilder.addWalls (
			centre, corridorDoors, 0
		);

		corridorDoors = new int[]{0, 0, 0, 1, 0, 1, 0, 1};
		centre = new Vector3 (20, 0, 17.5f);
		corridorBuilder.setMaterials (
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

		roomBuilder.setMaterials (
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
		roomBuilder.addFloor (roomCentre);
		roomBuilder.addRoof (roomCentre);
		roomBuilder.addWalls (
			roomCentre,
			doorStates
		);
	}
	private void addCorridor(Vector3 roomCentre, int[] doorStates, int angle){
		corridorBuilder.addFloor (
			roomCentre,
			angle
		);
		corridorBuilder.addRoof (
			roomCentre,
			angle
		);
		corridorBuilder.addWalls (
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