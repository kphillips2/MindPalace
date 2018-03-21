using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * AKA: Max's Monstrosity
 * WARNING: Do not attempt to read or alter this script
 * lest you be driven to madness by its labyrinthine structure.
 * Remember, when you stare into darkness, the darkness stares back.
 */
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
    public int doorIndex;

    private bool CorridorOnZ;
    private bool RoomNotCorridor = true;
    private Vector3 newRoomCentre;
	private RoomBuilder roomBuilder;
    private LevelEditorBuilder levelBuilder;
    private ActivationManager MenuActivationManager;

    void Start () {
		roomBuilder = room.GetComponent<RoomBuilder>();
		levelBuilder = level.GetComponent<LevelEditorBuilder>();
        MenuActivationManager = SingularActivation.GetComponent<ActivationManager>();
    }
    //Menu Manipulators:
    public void ShowMoreOptions()
    {
        ClickedOn.SetActive(false);
        RoomButton.SetActive(true);
        CorridorButton.SetActive(true);
        Window.SetActive(true);
        Picture.SetActive(true);
        MenuActivationManager.ActivateMenu(this.gameObject);
        RoomButton.GetComponent<Button>().interactable= CheckRoomPlacement();
        CorridorButton.GetComponent<Button>().interactable = CheckCorridorPlacement();
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
    //Should Probably be Statics:
    private int WallIndexForRoom()
    {
        int wallIndex = -1;
        if (doorIndex == 11 || doorIndex == 0 || doorIndex == 1)
        {
            wallIndex = 0;
        }
        else if (doorIndex == 2 || doorIndex == 3 || doorIndex == 4)
        {
            wallIndex = 1;
        }
        else if (doorIndex == 5 || doorIndex == 6 || doorIndex == 7)
        {
            wallIndex = 2;
        }
        else
        {
            wallIndex = 3;
        }

        return wallIndex;
    }
    private int WallIndexForCorridor()
    {
        if (CorridorOnZ)
        {
            if (doorIndex == 0)
            {
                return 0;
            }
            else if (1 <= doorIndex && doorIndex <= 6)
            {
                return 1;
            }
            else if (doorIndex == 7)
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
        else
        {
            if (doorIndex == 3)
            {
                return 1;
            }
            else if (4 <= doorIndex && doorIndex <= 9)
            {
                return 2;
            }
            else if (doorIndex == 10)
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }
    }
    private float OldDoorLocationCorridor()
    {
        if (CorridorOnZ)
        {
            switch (doorIndex)
            {
                case 0:
                    return 0f;
                case 1:
                    return -10f;
                case 2:
                    return -6f;
                case 3:
                    return -2f;
                case 4:
                    return 2f;
                case 5:
                    return 6f;
                case 6:
                    return 10f;
                case 7:
                    return 0f;
                case 8:
                    return -10f;
                case 9:
                    return -6f;
                case 10:
                    return -2f;
                case 11:
                    return 2f;
                case 12:
                    return 6f;
                default:
                    return 10f;
            }
        }
        else
        {
            switch (doorIndex)
            {
                case 0:
                    return 2f;
                case 1:
                    return 6f;
                case 2:
                    return 10f;
                case 3:
                    return 0f;
                case 4:
                    return -10f;
                case 5:
                    return -6f;
                case 6:
                    return -2f;
                case 7:
                    return 2f;
                case 8:
                    return 6f;
                case 9:
                    return 10f;
                case 10:
                    return 0f;
                case 11:
                    return -10f;
                case 12:
                    return -6f;
                default:
                    return -2f;
            }
        }
    }
    private Vector3 FindCorridorCenterFromCorridor()
    {
        if (CorridorOnZ)
        {
            switch (doorIndex)
            {
                case 0:
                    return new Vector3(0, 0, 24);
                case 1:
                    return new Vector3(14, 0, 10);
                case 2:
                    return new Vector3(14, 0, 6);
                case 3:
                    return new Vector3(14, 0, 2);
                case 4:
                    return new Vector3(14, 0, -2);
                case 5:
                    return new Vector3(14, 0, -6);
                case 6:
                    return new Vector3(14, 0, -10);
                case 7:
                    return new Vector3(0, 0, -24);
                case 8:
                    return new Vector3(-14, 0, -10);
                case 9:
                    return new Vector3(-14, 0, -6);
                case 10:
                    return new Vector3(-14, 0, -2);
                case 11:
                    return new Vector3(-14, 0, 2);
                case 12:
                    return new Vector3(-14, 0, 6);
                default:
                    return new Vector3(-14, 0, 10);
            }
        }
        else
        {
            switch (doorIndex)
            {
                case 0:
                    return new Vector3(2, 0, 14);
                case 1:
                    return new Vector3(6, 0, 14);
                case 2:
                    return new Vector3(10, 0, 14);
                case 3:
                    return new Vector3(24, 0, 0);
                case 4:
                    return new Vector3(10, 0, -14);
                case 5:
                    return new Vector3(6, 0, -14);
                case 6:
                    return new Vector3(2, 0, -14);
                case 7:
                    return new Vector3(-2, 0, -14);
                case 8:
                    return new Vector3(-6, 0, -14);
                case 9:
                    return new Vector3(-10, 0, -14);
                case 10:
                    return new Vector3(-24, 0, 0);
                case 11:
                    return new Vector3(-10, 0, 14);
                case 12:
                    return new Vector3(-6, 0, 14);
                default:
                    return new Vector3(-2, 0, 14);
            }
        }
    }
    private Vector3 FindRoomCenterFromCorridor()
    {
        if (CorridorOnZ)
        {
            switch (doorIndex)
            {
                case 0:
                    return new Vector3(0, 0, 18);
                case 1:
                    return new Vector3(8, 0, 10);
                case 2:
                    return new Vector3(8, 0, 6);
                case 3:
                    return new Vector3(8, 0, 2);
                case 4:
                    return new Vector3(8, 0, -2);
                case 5:
                    return new Vector3(8, 0, -6);
                case 6:
                    return new Vector3(8, 0, -10);
                case 7:
                    return new Vector3(0, 0, -18);
                case 8:
                    return new Vector3(-8, 0, -10);
                case 9:
                    return new Vector3(-8, 0, -6);
                case 10:
                    return new Vector3(-8, 0, -2);
                case 11:
                    return new Vector3(-8, 0, 2);
                case 12:
                    return new Vector3(-8, 0, 6);
                default:
                    return new Vector3(-8, 0, 10);
            }
        }
        else
        {
            switch (doorIndex)
            {
                case 0:
                    return new Vector3(2, 0, 8);
                case 1:
                    return new Vector3(6, 0, 8);
                case 2:
                    return new Vector3(10, 0, 8);
                case 3:
                    return new Vector3(18, 0, 0);
                case 4:
                    return new Vector3(10, 0, -8);
                case 5:
                    return new Vector3(6, 0, -8);
                case 6:
                    return new Vector3(2, 0, -8);
                case 7:
                    return new Vector3(-2, 0, -8);
                case 8:
                    return new Vector3(-6, 0, -8);
                case 9:
                    return new Vector3(-10, 0, -8);
                case 10:
                    return new Vector3(-18, 0, 0);
                case 11:
                    return new Vector3(-10, 0, 8);
                case 12:
                    return new Vector3(-6, 0, 8);
                default:
                    return new Vector3(-2, 0, 8);
            }
        }
    }
    private Vector3 FindCorridorCenterFromRoom()
    {
        switch(doorIndex){
            case 0:
                return new Vector3(0, 0, 18);
            case 1:
                return new Vector3(4, 0, 18);
            case 2:
                return new Vector3(18, 0, 4);
            case 3:
                return new Vector3(18, 0, 0);
            case 4:
                return new Vector3(18, 0, -4);
            case 5:
                return new Vector3(4, 0, -18);
            case 6:
                return new Vector3(0, 0, -18);
            case 7:
                return new Vector3(-4, 0, -18);
            case 8:
                return new Vector3(-18, 0, -4);
            case 9:
                return new Vector3(-18, 0, 0);
            case 10:
                return new Vector3(-18, 0, 4);
            default:
                return new Vector3(-4, 0, 18);
        }
    }
    private Vector3 FindRoomCenterFromRoom()
    {
        switch (doorIndex)
        {
            case 0:
                return new Vector3(0, 0, 12);
            case 1:
                return new Vector3(4, 0, 12);
            case 2:
                return new Vector3(12, 0, 4);
            case 3:
                return new Vector3(12, 0, 0);
            case 4:
                return new Vector3(12, 0, -4);
            case 5:
                return new Vector3(4, 0, -12);
            case 6:
                return new Vector3(0, 0, -12);
            case 7:
                return new Vector3(-4, 0, -12);
            case 8:
                return new Vector3(-12, 0, -4);
            case 9:
                return new Vector3(-12, 0, 0);
            case 10:
                return new Vector3(-12, 0, 4);
            default:
                return new Vector3(-4, 0, 12);
        }
    }
    //Menu Options:
    public void AddRoom()
    {
        Vector3 buttonCenter = this.transform.position;
        int wallIndex = -1;
        if (RoomNotCorridor)
        {
           wallIndex = WallIndexForRoom();
           newRoomCentre = FindRoomCenterFromRoom();
        }
        else
        {
            wallIndex = WallIndexForCorridor();
            newRoomCentre = FindRoomCenterFromCorridor();
        }
        
        BuildRoom(newRoomCentre + currentRoomCenter, wallIndex, doorIndex);
        int[] DummyDoors = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        string[] DummyMat = { "", "", "" };
        SaveLoad.currentLoci.addRoom(new Room(DummyDoors, ConvertVectorToFloat(newRoomCentre + currentRoomCenter), DummyMat));
        HideAll();

    }
    public void AddCorridor()
    {
        Vector3 buttonCenter = this.transform.position;
        int wallIndex = -1;
        if (RoomNotCorridor)
        {
            wallIndex = WallIndexForRoom();
            newRoomCentre = FindCorridorCenterFromRoom();
            
        }
        else
        {
            wallIndex = WallIndexForCorridor();
            newRoomCentre = FindCorridorCenterFromCorridor();
        }
        if (wallIndex == 0 || wallIndex == 2)
        {
            BuildCorridor(newRoomCentre + currentRoomCenter, wallIndex, doorIndex, true);
            int[] DummyDoors = { 0, 0, 0, 0, 0, 0, 0, 0};
            string[] DummyMat = { "", "", "" };
            SaveLoad.currentLoci.addCorridor(new Corridor(DummyDoors, ConvertVectorToFloat(newRoomCentre + currentRoomCenter), DummyMat, 90));
            HideAll();
        }
        else
        {
            BuildCorridor(newRoomCentre + currentRoomCenter, wallIndex, doorIndex, false);
            int[] DummyDoors = { 0, 0, 0, 0, 0, 0, 0, 0 };
            string[] DummyMat = { "", "", "" };
            SaveLoad.currentLoci.addCorridor(new Corridor(DummyDoors, ConvertVectorToFloat(newRoomCentre + currentRoomCenter), DummyMat, 0));
            HideAll();
        }

    }
    public void AddWindow()
    {
        float windowLocation;
        if (RoomNotCorridor)
        {
            if(doorIndex==0 ||doorIndex==3||doorIndex==6||doorIndex==9)
            {
                windowLocation = 0;
            } else if (doorIndex == 1 || doorIndex == 4 || doorIndex == 7 || doorIndex == 10)
            {
                windowLocation = 4;
            }
            else
            {
                windowLocation = -4;
            }
            roomBuilder.addWindow(WallIndexForRoom(), windowLocation);
        }
        else
        {
            windowLocation = OldDoorLocationCorridor();
            roomBuilder.addWindow(WallIndexForCorridor(), windowLocation);
        }
        HideAll();

    }
    public void AddPicture()
    {
        ImageMenu.transform.position = this.transform.position;
        ImageMenu.transform.rotation = this.transform.rotation;
        HideAllStillActive();
    }
    //Plus Sign Populators:
    public void PopulateRoomPlusSigns(Vector3 roomcenter, List<Canvas> PlusSignList)
    {
        PlusSignList[11].transform.position = roomcenter + new Vector3(-4f, 2.5f, 5.7f);
        PlusSignList[11].transform.rotation = Quaternion.Euler(0, 0, 0);
        PlusSignList[0].transform.position = roomcenter + new Vector3(0, 2.5f, 5.7f);
        PlusSignList[0].transform.rotation = Quaternion.Euler(0, 0, 0);
        PlusSignList[1].transform.position = roomcenter + new Vector3(4f, 2.5f, 5.7f);
        PlusSignList[1].transform.rotation = Quaternion.Euler(0, 0, 0);

        PlusSignList[2].transform.position = roomcenter + new Vector3(5.7f, 2.5f, 4f);
        PlusSignList[2].transform.rotation = Quaternion.Euler(0, 90, 0);
        PlusSignList[3].transform.position = roomcenter + new Vector3(5.7f, 2.5f, 0f);
        PlusSignList[3].transform.rotation = Quaternion.Euler(0, 90, 0);
        PlusSignList[4].transform.position = roomcenter + new Vector3(5.7f, 2.5f, -4f);
        PlusSignList[4].transform.rotation = Quaternion.Euler(0, 90, 0);

        PlusSignList[5].transform.position = roomcenter + new Vector3(4f, 2.5f, -5.7f);
        PlusSignList[5].transform.rotation = Quaternion.Euler(0, 180, 0);
        PlusSignList[6].transform.position = roomcenter + new Vector3(0f, 2.5f, -5.7f);
        PlusSignList[6].transform.rotation = Quaternion.Euler(0, 180, 0);
        PlusSignList[7].transform.position = roomcenter + new Vector3(-4f, 2.5f, -5.7f);
        PlusSignList[7].transform.rotation = Quaternion.Euler(0, 180, 0);

        PlusSignList[8].transform.position = roomcenter + new Vector3(-5.7f, 2.5f, -4f);
        PlusSignList[8].transform.rotation = Quaternion.Euler(0, 270, 0);
        PlusSignList[9].transform.position = roomcenter + new Vector3(-5.7f, 2.5f, 0f);
        PlusSignList[9].transform.rotation = Quaternion.Euler(0, 270, 0);
        PlusSignList[10].transform.position = roomcenter + new Vector3(-5.7f, 2.5f, 4f);
        PlusSignList[10].transform.rotation = Quaternion.Euler(0, 270, 0);

        //Only For Corridors
        PlusSignList[12].transform.position = new Vector3(0f, -100f, 0f);
        PlusSignList[13].transform.position = new Vector3(0f, -100f, 0f);
    }
    public void PopulateZCorridorPlusSigns(Vector3 corridorCenter, int wallIndex, List<Canvas> PlusSignList)
    {
        PlusSignList[0].transform.position = corridorCenter + new Vector3(0, 2.5f, 11.7f);
        PlusSignList[0].transform.rotation = Quaternion.Euler(0, 0, 0);

        PlusSignList[1].transform.position = corridorCenter + new Vector3(1.7f, 2.5f, 10f);
        PlusSignList[1].transform.rotation = Quaternion.Euler(0, 90, 0);
        PlusSignList[2].transform.position = corridorCenter + new Vector3(1.7f, 2.5f, 6f);
        PlusSignList[2].transform.rotation = Quaternion.Euler(0, 90, 0);
        PlusSignList[3].transform.position = corridorCenter + new Vector3(1.7f, 2.5f, 2f);
        PlusSignList[3].transform.rotation = Quaternion.Euler(0, 90, 0);
        PlusSignList[4].transform.position = corridorCenter + new Vector3(1.7f, 2.5f, -2f);
        PlusSignList[4].transform.rotation = Quaternion.Euler(0, 90, 0);
        PlusSignList[5].transform.position = corridorCenter + new Vector3(1.7f, 2.5f, -6f);
        PlusSignList[5].transform.rotation = Quaternion.Euler(0, 90, 0);
        PlusSignList[6].transform.position = corridorCenter + new Vector3(1.7f, 2.5f, -10f);
        PlusSignList[6].transform.rotation = Quaternion.Euler(0, 90, 0);

        PlusSignList[7].transform.position = corridorCenter + new Vector3(0, 2.5f, -11.7f);
        PlusSignList[7].transform.rotation = Quaternion.Euler(0, 180, 0);

        PlusSignList[8].transform.position = corridorCenter + new Vector3(-1.7f, 2.5f, -10f);
        PlusSignList[8].transform.rotation = Quaternion.Euler(0, 270, 0);
        PlusSignList[9].transform.position = corridorCenter + new Vector3(-1.7f, 2.5f, -6f);
        PlusSignList[9].transform.rotation = Quaternion.Euler(0, 270, 0);
        PlusSignList[10].transform.position = corridorCenter + new Vector3(-1.7f, 2.5f, -2f);
        PlusSignList[10].transform.rotation = Quaternion.Euler(0, 270, 0);
        PlusSignList[11].transform.position = corridorCenter + new Vector3(-1.7f, 2.5f, 2f);
        PlusSignList[11].transform.rotation = Quaternion.Euler(0, 270, 0);
        PlusSignList[12].transform.position = corridorCenter + new Vector3(-1.7f, 2.5f, 6f);
        PlusSignList[12].transform.position = new Vector3(PlusSignList[12].transform.position.x, 2.5f, PlusSignList[12].transform.position.z);
        PlusSignList[12].transform.rotation = Quaternion.Euler(0, 270, 0);
        PlusSignList[13].transform.position = corridorCenter + new Vector3(-1.7f, 2.5f, 10f);
        PlusSignList[13].transform.position = new Vector3(PlusSignList[13].transform.position.x, 2.5f, PlusSignList[13].transform.position.z);
        PlusSignList[13].transform.rotation = Quaternion.Euler(0, 270, 0);

        if (wallIndex == 0)
        {
            PlusSignList[7].GetComponent<subMenuButtons>().HideAll();
        }
        else
        {
            PlusSignList[0].GetComponent<subMenuButtons>().HideAll();
        }
    }
    public void PopulateXCorridorPlusSigns(Vector3 corridorCenter, int wallIndex, List<Canvas> PlusSignList)
    {
        PlusSignList[0].transform.position = corridorCenter + new Vector3(2f, 2.5f, 1.7f);
        PlusSignList[0].transform.rotation = Quaternion.Euler(0, 0, 0);
        PlusSignList[1].transform.position = corridorCenter + new Vector3(6f, 2.5f, 1.7f);
        PlusSignList[1].transform.rotation = Quaternion.Euler(0, 0, 0);
        PlusSignList[2].transform.position = corridorCenter + new Vector3(10f, 2.5f, 1.7f);
        PlusSignList[2].transform.rotation = Quaternion.Euler(0, 0, 0);

        PlusSignList[3].transform.position = corridorCenter + new Vector3(11.7f, 2.5f, 0f);
        PlusSignList[3].transform.rotation = Quaternion.Euler(0, 90, 0);

        PlusSignList[4].transform.position = corridorCenter + new Vector3(10f, 2.5f, -1.7f);
        PlusSignList[4].transform.rotation = Quaternion.Euler(0, 180, 0);
        PlusSignList[5].transform.position = corridorCenter + new Vector3(6f, 2.5f, -1.7f);
        PlusSignList[5].transform.rotation = Quaternion.Euler(0, 180, 0);
        PlusSignList[6].transform.position = corridorCenter + new Vector3(2f, 2.5f, -1.7f);
        PlusSignList[6].transform.rotation = Quaternion.Euler(0, 180, 0);
        PlusSignList[7].transform.position = corridorCenter + new Vector3(-2f, 2.5f, -1.7f);
        PlusSignList[7].transform.rotation = Quaternion.Euler(0, 180, 0);
        PlusSignList[8].transform.position = corridorCenter + new Vector3(-6f, 2.5f, -1.7f);
        PlusSignList[8].transform.rotation = Quaternion.Euler(0, 180, 0);
        PlusSignList[9].transform.position = corridorCenter + new Vector3(-10f, 2.5f, -1.7f);
        PlusSignList[9].transform.rotation = Quaternion.Euler(0, 180, 0);

        PlusSignList[10].transform.position = corridorCenter + new Vector3(-11.7f, 2.5f, 0f);
        PlusSignList[10].transform.rotation = Quaternion.Euler(0, 270, 0);

        PlusSignList[11].transform.position = corridorCenter + new Vector3(-10f, 2.5f, 1.7f);
        PlusSignList[11].transform.rotation = Quaternion.Euler(0, 0, 0);
        PlusSignList[12].transform.position = corridorCenter + new Vector3(-6f, 2.5f, 1.7f);
        PlusSignList[12].transform.position = new Vector3(PlusSignList[12].transform.position.x, 2.5f, PlusSignList[12].transform.position.z);
        PlusSignList[12].transform.rotation = Quaternion.Euler(0, 0, 0);
        PlusSignList[13].transform.position = corridorCenter + new Vector3(-2f, 2.5f, 1.7f);
        PlusSignList[13].transform.position = new Vector3(PlusSignList[13].transform.position.x, 2.5f, PlusSignList[13].transform.position.z);
        PlusSignList[13].transform.rotation = Quaternion.Euler(0, 0, 0);

        if (wallIndex == 1)
        {
            PlusSignList[10].GetComponent<subMenuButtons>().HideAll();
        }
        else
        {
            PlusSignList[3].GetComponent<subMenuButtons>().HideAll();
        }
    }
    //Builders:
    private void BuildRoom(Vector3 newRoomCentre, int wallIndexParam, int doorIndexParam)
    {
        float oldDoorLoc = 0;
        if (RoomNotCorridor)
        {
            if (doorIndexParam == 0 || doorIndexParam == 3 || doorIndexParam == 6 || doorIndexParam == 9)
            {
                oldDoorLoc = 0;
            }
            else if (doorIndexParam == 1 || doorIndexParam == 4 || doorIndexParam == 7 || doorIndexParam == 10)
            {
                oldDoorLoc = 4;
            }
            else 
            {
                oldDoorLoc = -4;
            }
        }
        else
        {
            oldDoorLoc = OldDoorLocationCorridor();
        }
        roomBuilder.addDoor(wallIndexParam, oldDoorLoc);
        GameObject newRoom = levelBuilder.addRoom(newRoomCentre);

        //Make the plus sign on the new door dissapear in the new room
        Canvas[] PlusSigns = newRoom.GetComponentsInChildren<Canvas>();
        List<Canvas> PlusSignList = new List<Canvas>();
        for (int j = 0; j < PlusSigns.Length; j++)
        {
            if (PlusSigns[j].tag == "PlusSign")
            {
                PlusSignList.Add(PlusSigns[j]);
            }
        }
        //make sure only plus signs in list
        for (int i = 0; i < PlusSignList.Count; i++)
        {

            PlusSignList[i].GetComponent<subMenuButtons>().currentRoomCenter = newRoomCentre;
            PlusSignList[i].GetComponent<subMenuButtons>().DefaultState();
            PlusSignList[i].GetComponent<subMenuButtons>().RoomNotCorridor = true;
        }

        PopulateRoomPlusSigns(newRoomCentre, PlusSignList);
        if (RoomNotCorridor)
        {
            PlusSignList[CalcOppositeDoorIndex(doorIndexParam)].GetComponent<subMenuButtons>().HideAll();
        }
        else
        {
            int PlusSignToHide = -1;
            if (CorridorOnZ)
            {
                if (doorIndex == 0)
                {
                    PlusSignToHide = 6;
                }
                else if(2<=doorIndex && doorIndex<=6)
                {
                    PlusSignToHide = 9;
                }
                else if(doorIndex==7)
                {
                    PlusSignToHide = 0;
                }
                else
                {
                    PlusSignToHide = 3;
                }
            }
            else
            {
                if (doorIndex == 3)
                {
                    PlusSignToHide = 9;
                }
                else if (4 <= doorIndex && doorIndex <= 9)
                {
                    PlusSignToHide = 0;
                }
                else if (doorIndex == 10)
                {
                    PlusSignToHide = 3;
                }
                else
                {
                    PlusSignToHide = 6;
                }
            }
            PlusSignList[PlusSignToHide].GetComponent<subMenuButtons>().HideAll();
        }

        RoomBuilder useToCutDoor = newRoom.GetComponent<RoomBuilder>();

        //Door Cut in new room
        int oppositeDoorIndex = -1;
        if (wallIndexParam == 0)
        {
            oppositeDoorIndex = 2;
        }
        else if (wallIndexParam == 1)
        {
            oppositeDoorIndex = 3;
        }
        else if (wallIndexParam == 2)
        {
            oppositeDoorIndex = 0;
        }
        else if (wallIndexParam == 3)
        {
            oppositeDoorIndex = 1;
        }
        useToCutDoor.addDoor(oppositeDoorIndex, 0);
    }
    public void BuildCorridor(Vector3 corridorCenter, int wallIndexParam, int doorIndexParam, bool zAxis)
    {
        //print(corridorCenter);
        float oldDoorLoc = 0;
        if (RoomNotCorridor)
        {
            if (doorIndexParam == 0 || doorIndexParam == 3 || doorIndexParam == 6 || doorIndexParam == 9)
            {
                oldDoorLoc = 0;
            }
            else if (doorIndexParam == 1 || doorIndexParam == 4 || doorIndexParam == 7 || doorIndexParam == 10)
            {
                oldDoorLoc = 4;
            }
            if (doorIndexParam == 2 || doorIndexParam == 5 || doorIndexParam == 8 || doorIndexParam == 11)
            {
                oldDoorLoc = -4;
            }
        }
        else
        {
            oldDoorLoc = OldDoorLocationCorridor();
        }
        roomBuilder.addDoor(wallIndexParam, oldDoorLoc);
        GameObject newCorridor;
        if (zAxis)
        {
            newCorridor = levelBuilder.addCorridorAlongZ(corridorCenter);
        }
        else
        {
            newCorridor = levelBuilder.addCorridorAlongX(corridorCenter);
        }
       
        
        //Make the plus sign on the new door dissapear in the new room
        Canvas[] PlusSigns = newCorridor.GetComponentsInChildren<Canvas>();
        List<Canvas> PlusSignList = new List<Canvas>();
        for (int j = 0; j < PlusSigns.Length; j++)
        {
            if (PlusSigns[j].tag == "PlusSign")
            {
                PlusSignList.Add(PlusSigns[j]);
            }
        }
        //make sure only plus signs in list
        for (int i = 0; i < PlusSignList.Count; i++)
        {
            PlusSignList[i].GetComponent<subMenuButtons>().currentRoomCenter = corridorCenter;
            PlusSignList[i].GetComponent<subMenuButtons>().DefaultState();
            PlusSignList[i].GetComponent<subMenuButtons>().RoomNotCorridor = false;
            PlusSignList[i].GetComponent<subMenuButtons>().CorridorOnZ = zAxis;
        }


        RoomBuilder useToCutDoor = newCorridor.GetComponent<RoomBuilder>();

        //Door Cut in new room
        int oppositeDoorIndex = -1;
        if (wallIndexParam == 0)
        {
            oppositeDoorIndex = 2;
        }
        else if (wallIndexParam == 1)
        {
            oppositeDoorIndex = 3;
        }
        else if (wallIndexParam == 2)
        {
            oppositeDoorIndex = 0;
        }
        else if (wallIndexParam == 3)
        {
            oppositeDoorIndex = 1;
        }
        useToCutDoor.addDoor(oppositeDoorIndex, 0);

        if (zAxis)
        {
            PopulateZCorridorPlusSigns(corridorCenter, wallIndexParam, PlusSignList);
        }
        else
        {
            PopulateXCorridorPlusSigns(corridorCenter, wallIndexParam, PlusSignList);
        }
    }
    //Collider Checkers:
    private bool CheckRoomPlacement()
    {
        Vector3 RoomCenterToCheck = new Vector3(-1, -1, -1);
        if (RoomNotCorridor)
        {
            RoomCenterToCheck = FindRoomCenterFromRoom();

        }
        else
        {
            RoomCenterToCheck = FindRoomCenterFromCorridor();
        }
        return RoomCollision.canRoomBePlaced(RoomCenterToCheck + currentRoomCenter);
    }
    private bool CheckCorridorPlacement()
    {
        Vector3 CorridorCenterToCheck = new Vector3(-1, -1, -1);
        float corridorAngle = -1;
        if (RoomNotCorridor)
        {
            CorridorCenterToCheck = FindCorridorCenterFromRoom();
            int wallIndex = WallIndexForRoom();
            if (wallIndex % 2 == 0)
            {
                corridorAngle = 90;
            }
            else
            {
                corridorAngle = 0;
            }
            
        }
        else
        {
            CorridorCenterToCheck = FindCorridorCenterFromCorridor();
            int wallIndex = WallIndexForCorridor();
            if (wallIndex % 2 == 0)
            {
                corridorAngle = 90;
            }
            else
            {
                corridorAngle = 0;
            }
        }
        return RoomCollision.canCorridorBePlaced(CorridorCenterToCheck + currentRoomCenter,corridorAngle);
    }
    //Utility
    private float[] ConvertVectorToFloat(Vector3 ParamVector)
    {
        float[] Converted = { ParamVector.x, ParamVector.y, ParamVector.z };
        return Converted;
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
