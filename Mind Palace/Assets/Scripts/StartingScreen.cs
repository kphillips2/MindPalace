using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingScreen : MonoBehaviour {

    public OldRoom room;
    private PictureCreator pictureCreator;
    private int[] roomDoors;

    // Use this for initialization
    void Start () {

        pictureCreator = new PictureCreator();
        roomDoors = new int[] { 0, 1, 0, 0 };
        room.setMaterials(
                "Wood Texture 15", // floor material
                "Wood Texture 05", // roof material
                "Wood texture 05"  // wall material
                 );
        room.addFloor(new Vector3(0, 0, 0));
        room.addRoof(new Vector3(0, 0, 0));
        room.addWalls(
            new Vector3(0, 0, 0),
            roomDoors
        );
        //add pictures
        //hangPictures(new Vector3(0, 0, 0), roomDoors);
    }
    /*
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
                        pictureCreator.placePicture(imageRes[(randPic + 1) % imageRes.Length], 90f, centre + new Vector3((4.7f / 1.5f), 2.6f, 4.7f));
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
*/

    // Update is called once per frame
    void Update () {
		
	}
}
