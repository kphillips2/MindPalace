using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subMenuButtons : MonoBehaviour {
    public GameObject room;
    public Vector3 currentRoomCenter;
    private Vector3 newRoomCentre;
    private RoomBuilder roomBuilder;
    public GameObject ClickedOn;
    public GameObject RoomButton;
    public GameObject Window;
    public GameObject Picture;

    // Use this for initialization
    void Start () {
        roomBuilder = room.GetComponent<RoomBuilder>();
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

    public void AddRoom()
    {
        Vector3 buttonCenter = this.transform.position;
        print(buttonCenter.x + "=X");
        print(buttonCenter.z + "=Z");
        if (buttonCenter.x>=4.6f + currentRoomCenter.x)
        {
            if (buttonCenter.z == 0f + currentRoomCenter.z)
            {
                newRoomCentre = new Vector3(10, 0, 0);
            }
            else if (buttonCenter.z > 0f + currentRoomCenter.z)
            {
                newRoomCentre = new Vector3(10, 0, 3);
            } else if (buttonCenter.z < 0f + currentRoomCenter.z)
            {
                newRoomCentre = new Vector3(10, 0, -3);
            }
        }
        else if (buttonCenter.x <= -4.6f+ currentRoomCenter.x)
        {
            if (buttonCenter.z == 0f + currentRoomCenter.z)
            {
                newRoomCentre = new Vector3(-10, 0, 0);
            }else if (buttonCenter.z > 0f + currentRoomCenter.z)
            {
                newRoomCentre = new Vector3(-10, 0, 3);
            }else if (buttonCenter.z < 0f + currentRoomCenter.z)
            {
                newRoomCentre = new Vector3(-10, 0, -3);
            }
        }
        else if (buttonCenter.z >= 4.6f+ currentRoomCenter.z)
        {
            if (buttonCenter.x == 0f + currentRoomCenter.x)
            {
                newRoomCentre = new Vector3(0, 0, 10);
            }else if (buttonCenter.x > 0f + currentRoomCenter.x)
            {
                newRoomCentre = new Vector3(3, 0, 10);
            }else if (buttonCenter.x < 0f + currentRoomCenter.x)
            {
                newRoomCentre = new Vector3(-3, 0, 10);
            }
        }
        else if (buttonCenter.z <= -4.6f+ currentRoomCenter.z)
        {
            if (buttonCenter.x == 0f + currentRoomCenter.x)
            {
                newRoomCentre = new Vector3(0, 0, -10);
            }else if (buttonCenter.x > 0f + currentRoomCenter.x)
            {
                newRoomCentre = new Vector3(3, 0, -10);
            }else if (buttonCenter.x < 0f + currentRoomCenter.x)
            {
                newRoomCentre = new Vector3(-3, 0, -10);
            }
        }
        else
        {
            print("nowhere?");
        }
        print(buttonCenter);
        roomBuilder.setMaterials(
            "Wood Texture 06", // floor material
            "Wood Texture 15", // roof material
            "Wood texture 12"  // wall material
        );
        int[] roomDoors = new int[] { 1, 1, 1, 1 };
        buildRoom(newRoomCentre + currentRoomCenter, roomDoors);
        HideAll();

    }
    private void buildRoom(Vector3 newRoomCentre, int[] doorStates)
    {
        roomBuilder.addFloor(newRoomCentre);
        roomBuilder.addRoof(newRoomCentre);
        roomBuilder.addPlusSigns(newRoomCentre);
        roomBuilder.addWalls(
            newRoomCentre,
            doorStates
        );
    }
}
