using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class subMenuButtons : MonoBehaviour {
    public GameObject room;
    public GameObject level;

    public Vector3 currentRoomCenter;
    public GameObject ClickedOn;
    public GameObject RoomButton;
    public GameObject CorridorButton;
    public GameObject Picture;
    public GameObject Window;
    public GameObject SingularActivation;
    public GameObject ImageMenu;

    private Vector3 newRoomCentre;
	private RoomBuilder roomBuilder;
    private LevelEditorBuilder levelBuilder;
    private ActivationManager MenuActivationManager;

    void Start () {
		roomBuilder = room.GetComponent<RoomBuilder>();
		levelBuilder = level.GetComponent<LevelEditorBuilder>();
        MenuActivationManager = SingularActivation.GetComponent<ActivationManager>();
    }

    public void ShowMoreOptions()
    {
        ClickedOn.SetActive(false);
        RoomButton.SetActive(true);
        CorridorButton.SetActive(true);
        Window.SetActive(true);
        Picture.SetActive(true);
        MenuActivationManager.ActivateMenu(this.gameObject);
        RoomButton.GetComponent<Button>().interactable= CheckRoomPlacement();
    }
    public void HideAll()
    {
        ClickedOn.SetActive(false);
        RoomButton.SetActive(false);
        Window.SetActive(false);
        Picture.SetActive(false);
        CorridorButton.SetActive(false);
        MenuActivationManager = SingularActivation.GetComponent<ActivationManager>();
        MenuActivationManager.NoActive();
    }

    public void HideAllStillActive()
    {
        ClickedOn.SetActive(false);
        RoomButton.SetActive(false);
        Window.SetActive(false);
        Picture.SetActive(false);
        CorridorButton.SetActive(false);
    }

    public void DefaultState()
    {
        ClickedOn.SetActive(true);
        RoomButton.SetActive(false);
        Window.SetActive(false);
        Picture.SetActive(false);
        CorridorButton.SetActive(false);
    }

    public void AddCorridor()
    {
        print("TODO");
    }

    public void AddWindow()
    {
        print("TODO");
    }

    public void AddPicture()
    {
        ImageMenu.transform.position = this.transform.position;
        ImageMenu.transform.rotation = this.transform.rotation;
        HideAllStillActive();
    }

    private bool CheckRoomPlacement()
    {
        Vector3 buttonCenter = this.transform.position;
        Vector3 RoomCenterToCheck = new Vector3(-1, -1, -1);
        if (buttonCenter.x >= 4.6f + currentRoomCenter.x)
        {
            if (buttonCenter.z == 0f + currentRoomCenter.z)
            {
                RoomCenterToCheck = new Vector3(12, 0, 0);
            }
            else if (buttonCenter.z > 0f + currentRoomCenter.z)
            {
                RoomCenterToCheck = new Vector3(12, 0, 4);
            }
            else if (buttonCenter.z < 0f + currentRoomCenter.z)
            {
                RoomCenterToCheck = new Vector3(12, 0, -4);
            }
        }
        else if (buttonCenter.x <= -4.6f + currentRoomCenter.x)
        {
            if (buttonCenter.z == 0f + currentRoomCenter.z)
            {
                RoomCenterToCheck = new Vector3(-12, 0, 0);
            }
            else if (buttonCenter.z > 0f + currentRoomCenter.z)
            {
                RoomCenterToCheck = new Vector3(-12, 0, 4);
            }
            else if (buttonCenter.z < 0f + currentRoomCenter.z)
            {
                RoomCenterToCheck = new Vector3(-12, 0, -4);
            }
        }
        else if (buttonCenter.z >= 4.6f + currentRoomCenter.z)
        {
            if (buttonCenter.x == 0f + currentRoomCenter.x)
            {
                RoomCenterToCheck = new Vector3(0, 0, 12);
            }
            else if (buttonCenter.x > 0f + currentRoomCenter.x)
            {
                RoomCenterToCheck = new Vector3(4, 0, 12);
            }
            else if (buttonCenter.x < 0f + currentRoomCenter.x)
            {
                RoomCenterToCheck = new Vector3(-4, 0, 12);
            }
        }
        else if (buttonCenter.z <= -4.6f + currentRoomCenter.z)
        {
            if (buttonCenter.x == 0f + currentRoomCenter.x)
            {
                RoomCenterToCheck = new Vector3(0, 0, -12);
            }
            else if (buttonCenter.x > 0f + currentRoomCenter.x)
            {
                RoomCenterToCheck = new Vector3(4, 0, -12);
            }
            else if (buttonCenter.x < 0f + currentRoomCenter.x)
            {
                RoomCenterToCheck = new Vector3(-4, 0, -12);
            }
        }
        else
        {
            print("nowhere?");
        }
        return RoomCollision.canRoomBePlaced(RoomCenterToCheck+currentRoomCenter);
    }

    public void AddRoom()
    {
        Vector3 buttonCenter = this.transform.position;
        int doorIndex = -1;
        int wallIndex = -1;
        if (buttonCenter.x >= 4.6f + currentRoomCenter.x)
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
        else if (buttonCenter.x <= -4.6f + currentRoomCenter.x)
        {
            if (buttonCenter.z == 0f + currentRoomCenter.z)
            {
                doorIndex = 9;
                wallIndex = 3;
                newRoomCentre = new Vector3(-12, 0, 0);
            } else if (buttonCenter.z > 0f + currentRoomCenter.z)
            {
                doorIndex = 10;
                wallIndex = 3;
                newRoomCentre = new Vector3(-12, 0, 4);
            } else if (buttonCenter.z < 0f + currentRoomCenter.z)
            {
                doorIndex = 8;
                wallIndex = 3;
                newRoomCentre = new Vector3(-12, 0, -4);
            }
        }
        else if (buttonCenter.z >= 4.6f + currentRoomCenter.z)
        {
            if (buttonCenter.x == 0f + currentRoomCenter.x)
            {
                doorIndex = 0;
                wallIndex = 0;
                newRoomCentre = new Vector3(0, 0, 12);
            } else if (buttonCenter.x > 0f + currentRoomCenter.x)
            {
                doorIndex = 1;
                wallIndex = 0;
                newRoomCentre = new Vector3(4, 0, 12);
            } else if (buttonCenter.x < 0f + currentRoomCenter.x)
            {
                doorIndex = 11;
                wallIndex = 0;
                newRoomCentre = new Vector3(-4, 0, 12);
            }
        }
        else if (buttonCenter.z <= -4.6f + currentRoomCenter.z)
        {
            if (buttonCenter.x == 0f + currentRoomCenter.x)
            {
                doorIndex = 6;
                wallIndex = 2;
                newRoomCentre = new Vector3(0, 0, -12);
            } else if (buttonCenter.x > 0f + currentRoomCenter.x)
            {
                doorIndex = 5;
                wallIndex = 2;
                newRoomCentre = new Vector3(4, 0, -12);
            } else if (buttonCenter.x < 0f + currentRoomCenter.x)
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
        buildRoom(newRoomCentre + currentRoomCenter, wallIndex, doorIndex);
        int[] DummyDoors = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        string[] DummyMat = {"","","" };
        SaveLoad.currentLoci.addRoom(new Room(DummyDoors, ConvertVectorToFloat(newRoomCentre + currentRoomCenter),DummyMat));
        HideAll();

    }

    private float[] ConvertVectorToFloat(Vector3 ParamVector)
    {
        float[] Converted = { ParamVector.x, ParamVector.y, ParamVector.z };
        return Converted;
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
        roomBuilder.addDoor(wallIndexParam, oldDoorLoc);
        GameObject newRoom = levelBuilder.addRoom(newRoomCentre);

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
        for (int i = 0; i < PlusSignList.Count; i++)
        {
            
            PlusSignList[i].GetComponent<subMenuButtons>().currentRoomCenter = newRoomCentre;
            PlusSignList[i].GetComponent<subMenuButtons>().DefaultState();
        }
        PlusSignList[CalcOppositeDoorIndex(doorIndexParam)].GetComponent<subMenuButtons>().HideAll();


        RoomBuilder useToCutDoor = newRoom.GetComponent<RoomBuilder>();

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
