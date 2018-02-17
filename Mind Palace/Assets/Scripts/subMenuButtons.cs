using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subMenuButtons : MonoBehaviour {
    public GameObject room;
    public Vector3 currentRoomCenter;
    private Vector3 newRoomCentre;
    private OldRoom oldRoom;
    public GameObject ClickedOn;
    public GameObject RoomButton;
    public GameObject Window;
    public GameObject Picture;

    // Use this for initialization
    void Start () {
		oldRoom = room.GetComponent<OldRoom>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowMoreOptions()
    {
        ClickedOn.SetActive(false);
        RoomButton.SetActive(true);
        Window.SetActive(true);
        Picture.SetActive(true);
    }
    public void HideAll()
    {
        ClickedOn.SetActive(false);
        RoomButton.SetActive(false);
        Window.SetActive(false);
        Picture.SetActive(false);
    }
    
    public void DefaultState()
    {
        ClickedOn.SetActive(true);
        RoomButton.SetActive(false);
        Window.SetActive(false);
        Picture.SetActive(false);
    }

    public void AddRoom()
    {
        Vector3 buttonCenter = this.transform.position;
        int doorIndex = -1;
        print(buttonCenter.x + "=X");
        print(buttonCenter.z + "=Z");
        if (buttonCenter.x>=4.6f + currentRoomCenter.x)
        {
            if (buttonCenter.z == 0f + currentRoomCenter.z)
            {
                doorIndex = 3;
                newRoomCentre = new Vector3(10, 0, 0);
            }
            else if (buttonCenter.z > 0f + currentRoomCenter.z)
            {
                doorIndex = 2;
                newRoomCentre = new Vector3(10, 0, 3);
            } else if (buttonCenter.z < 0f + currentRoomCenter.z)
            {
                doorIndex = 4;
                newRoomCentre = new Vector3(10, 0, -3);
            }
        }
        else if (buttonCenter.x <= -4.6f+ currentRoomCenter.x)
        {
            if (buttonCenter.z == 0f + currentRoomCenter.z)
            {
                doorIndex = 9;
                newRoomCentre = new Vector3(-10, 0, 0);
            }else if (buttonCenter.z > 0f + currentRoomCenter.z)
            {
                doorIndex = 10;
                newRoomCentre = new Vector3(-10, 0, 3);
            }else if (buttonCenter.z < 0f + currentRoomCenter.z)
            {
                doorIndex = 8;
                newRoomCentre = new Vector3(-10, 0, -3);
            }
        }
        else if (buttonCenter.z >= 4.6f+ currentRoomCenter.z)
        {
            if (buttonCenter.x == 0f + currentRoomCenter.x)
            {
                doorIndex = 0;
                newRoomCentre = new Vector3(0, 0, 10);
            }else if (buttonCenter.x > 0f + currentRoomCenter.x)
            {
                doorIndex = 1;
                newRoomCentre = new Vector3(3, 0, 10);
            }else if (buttonCenter.x < 0f + currentRoomCenter.x)
            {
                doorIndex = 11;
                newRoomCentre = new Vector3(-3, 0, 10);
            }
        }
        else if (buttonCenter.z <= -4.6f+ currentRoomCenter.z)
        {
            if (buttonCenter.x == 0f + currentRoomCenter.x)
            {
                doorIndex = 6;
                newRoomCentre = new Vector3(0, 0, -10);
            }else if (buttonCenter.x > 0f + currentRoomCenter.x)
            {
                doorIndex = 5;
                newRoomCentre = new Vector3(3, 0, -10);
            }else if (buttonCenter.x < 0f + currentRoomCenter.x)
            {
                doorIndex = 7;
                newRoomCentre = new Vector3(-3, 0, -10);
            }
        }
        else
        {
            print("nowhere?");
        }
        print(buttonCenter);
		oldRoom.setMaterials(
            "Wood Texture 06", // floor material
            "Wood Texture 15", // roof material
            "Wood texture 12"  // wall material
        );
        int[] roomDoors = new int[] { 1, 1, 1, 1 };
        buildRoom(newRoomCentre + currentRoomCenter, roomDoors, doorIndex);
        HideAll();

    }
    private void buildRoom(Vector3 newRoomCentre, int[] doorStates, int doorIndexParam)
    {
		oldRoom.addFloor(newRoomCentre);
		oldRoom.addRoof(newRoomCentre);
		oldRoom.addPlusSigns(newRoomCentre, doorIndexParam);
		oldRoom.addWalls(
            newRoomCentre,
            doorStates
        );
    }
}
