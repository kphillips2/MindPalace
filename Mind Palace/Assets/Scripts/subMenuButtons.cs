using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subMenuButtons : MonoBehaviour {
    public GameObject room;
    public GameObject level;

    public Vector3 currentRoomCenter;
    public GameObject ClickedOn;
    public GameObject RoomButton;
    public GameObject Window;
    public GameObject Picture;

    private Vector3 newRoomCentre;
    private RoomCreator roomCreator;
    private LevelEditorBuilder roomBuilder;

    void Start () {
        roomCreator = room.GetComponent<RoomCreator>();
        roomBuilder = level.GetComponent<LevelEditorBuilder>();
        
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
        int wallIndex = -1;
        if (buttonCenter.x>=4.6f + currentRoomCenter.x)
        {
            if (buttonCenter.z == 0f + currentRoomCenter.z)
            {
                doorIndex = 3;
                wallIndex = 1;
                newRoomCentre = new Vector3(12, 0, 0);
            }
            else if (buttonCenter.z > 0f + currentRoomCenter.z)
            {
                doorIndex = 2;
                wallIndex = 1;
                newRoomCentre = new Vector3(12, 0, 4);
            } else if (buttonCenter.z < 0f + currentRoomCenter.z)
            {
                doorIndex = 4;
                wallIndex = 1;
                newRoomCentre = new Vector3(12, 0, -4);
            }
        }
        else if (buttonCenter.x <= -4.6f+ currentRoomCenter.x)
        {
            if (buttonCenter.z == 0f + currentRoomCenter.z)
            {
                doorIndex = 9;
                wallIndex = 3;
                newRoomCentre = new Vector3(-12, 0, 0);
            }else if (buttonCenter.z > 0f + currentRoomCenter.z)
            {
                doorIndex = 10;
                wallIndex = 3;
                newRoomCentre = new Vector3(-12, 0, 4);
            }else if (buttonCenter.z < 0f + currentRoomCenter.z)
            {
                doorIndex = 8;
                wallIndex = 3;
                newRoomCentre = new Vector3(-12, 0, -4);
            }
        }
        else if (buttonCenter.z >= 4.6f+ currentRoomCenter.z)
        {
            if (buttonCenter.x == 0f + currentRoomCenter.x)
            {
                doorIndex = 0;
                wallIndex = 0;
                newRoomCentre = new Vector3(0, 0, 12);
            }else if (buttonCenter.x > 0f + currentRoomCenter.x)
            {
                doorIndex = 1;
                wallIndex = 0;
                newRoomCentre = new Vector3(4, 0, 12);
            }else if (buttonCenter.x < 0f + currentRoomCenter.x)
            {
                doorIndex = 11;
                wallIndex = 0;
                newRoomCentre = new Vector3(-4, 0, 12);
            }
        }
        else if (buttonCenter.z <= -4.6f+ currentRoomCenter.z)
        {
            if (buttonCenter.x == 0f + currentRoomCenter.x)
            {
                doorIndex = 6;
                wallIndex = 2;
                newRoomCentre = new Vector3(0, 0, -12);
            }else if (buttonCenter.x > 0f + currentRoomCenter.x)
            {
                doorIndex = 5;
                wallIndex = 2;
                newRoomCentre = new Vector3(4, 0, -12);
            }else if (buttonCenter.x < 0f + currentRoomCenter.x)
            {
                doorIndex = 7;
                wallIndex = 2;
                newRoomCentre = new Vector3(-4, 0, -12);
            }
        }
        else
        {
            print("nowhere?");
        }
		roomCreator.setMaterials(
            "Wood Texture 06", // floor material
            "Wood Texture 15", // roof material
            "Wood texture 12"  // wall material
        );
        buildRoom(newRoomCentre + currentRoomCenter,wallIndex, doorIndex);
        HideAll();

    }
    private void buildRoom(Vector3 newRoomCentre, int wallIndexParam, int doorIndexParam)
    {
        float oldDoorLoc = 0;
        if(doorIndexParam==0 || doorIndexParam == 3 || doorIndexParam == 6 || doorIndexParam == 9)
        {
            oldDoorLoc = 0;
        } else if (doorIndexParam == 1 || doorIndexParam == 4 || doorIndexParam == 7 || doorIndexParam == 10)
        {
            oldDoorLoc = 4;
        }
        if (doorIndexParam == 2 || doorIndexParam == 5 || doorIndexParam == 8 || doorIndexParam == 11)
        {
            oldDoorLoc = -4;
        }
        roomCreator.addDoor(wallIndexParam, oldDoorLoc);
        GameObject newRoom = roomBuilder.addRoom(newRoomCentre);

        //Make the plus sign on the new door dissapear in the new room
        Canvas[] PlusSigns = newRoom.GetComponentsInChildren<Canvas>();
        List<Canvas> PlusSignList = new List<Canvas>();
        for (int j = 0; j < PlusSigns.Length; j++)
        {
            if(PlusSigns[j].tag == "PlusSign")
            {
                PlusSignList.Add(PlusSigns[j]);
            }
        }
            //make sure only plus signs in list
        print(PlusSignList.Count);
        for (int i = 0; i < PlusSignList.Count; i++)
        {
            
            PlusSignList[i].GetComponent<subMenuButtons>().currentRoomCenter = newRoomCentre;
            PlusSignList[i].GetComponent<subMenuButtons>().DefaultState();
        }
        PlusSignList[CalcOppositeDoorIndex(doorIndexParam)].GetComponent<subMenuButtons>().HideAll();


        RoomCreator useToCutDoor = newRoom.GetComponent<RoomCreator>();

        //Door Cut in new room
        int oppositeDoorIndex = -1;
        if (wallIndexParam == 0)
        {
            oppositeDoorIndex = 2;
        } else if (wallIndexParam == 1) {
            oppositeDoorIndex = 3;
        } else if (wallIndexParam == 2)
        {
            oppositeDoorIndex = 0;
        } else if (wallIndexParam == 3)
        {
            oppositeDoorIndex = 1;
        }
        useToCutDoor.addDoor(oppositeDoorIndex, 0);
    }

    public int CalcOppositeDoorIndex(int doorIndex)
    {
        if (doorIndex == 11 || doorIndex == 0 || doorIndex == 1)
        {
            return 6;
        }
        else if (doorIndex == 2 || doorIndex == 3 || doorIndex == 4)
        {
            return 9;
        }
        else if (doorIndex == 5 || doorIndex == 6 || doorIndex == 7)
        {
            return 0;
        }
        else
        {
            return 3;
        }
    }
}
