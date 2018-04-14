using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for handling and plus sign functionality.
/// </summary>
public class SubMenuHandler : MonoBehaviour {
    public GameObject level;
    public GameObject room;

    public GameObject ClickedOn;
    public GameObject RoomButton;
    public GameObject CorridorButton;
    public GameObject Picture;
    public GameObject Window;
    public GameObject DoorSubMenu;
    public GameObject Door;
    public GameObject SingularActivation;
    public GameObject ImageMenu;

    private Building building;
    private RoomHandler roomHandler;
    private ActivationManager MenuActivationManager;
    private PlusData thisPlus;

    /// <summary>
    /// Initializes the attributes for this plus sign.
    /// </summary>
    /// <param name="plus"></param>
    public void InitData(PlusData plus){
        roomHandler = room.GetComponent<RoomHandler> ();
        building = level.GetComponent<Building> ();
        MenuActivationManager = SingularActivation.GetComponent<ActivationManager> ();
        thisPlus = plus;
    }
    //Menu Manipulators:
    public void ShowMoreOptions(){
        ClickedOn.SetActive (false);
        RoomButton.SetActive (false);
        CorridorButton.SetActive (false);
        Window.SetActive (true);
        Picture.SetActive (true);
        DoorSubMenu.SetActive (true);
        Door.SetActive (false);

        MenuActivationManager.ActivateMenu (gameObject);
    }
    public void HideAll(){
        ClickedOn.SetActive (false);
        RoomButton.SetActive (false);
        Window.SetActive (false);
        Picture.SetActive (false);
        CorridorButton.SetActive (false);
        DoorSubMenu.SetActive (false);
        Door.SetActive (false);

        MenuActivationManager = SingularActivation.GetComponent<ActivationManager> ();
        MenuActivationManager.NoActive ();
        this.transform.position = new Vector3 (0, -100, 0);
    }
    public void HideAllStillActive(){
        ClickedOn.SetActive (false);
        RoomButton.SetActive (false);
        Window.SetActive (false);
        Picture.SetActive (false);
        CorridorButton.SetActive (false);
        DoorSubMenu.SetActive (false);
        Door.SetActive (false);
    }
    public void DefaultState(){
        ClickedOn.SetActive (true);
        RoomButton.SetActive (false);
        Window.SetActive (false);
        Picture.SetActive (false);
        CorridorButton.SetActive (false);
        DoorSubMenu.SetActive (false);
        Door.SetActive (false);
    }
    public void OpenDoorSubMenu(){
        ClickedOn.SetActive (false);
        RoomButton.SetActive (true);
        Window.SetActive (false);
        Picture.SetActive (false);
        CorridorButton.SetActive (true);
        DoorSubMenu.SetActive (false);
        Door.SetActive (true);

        string type = "room";
        RoomButton.GetComponent<Button> ().interactable = CheckRoomPlacement (type);
        type = RoomTypes.GetCorridorType (thisPlus.GetAngle ());
        CorridorButton.GetComponent<Button> ().interactable = CheckRoomPlacement (type);

        RoomData thisRoom = roomHandler.GetData ();
        //This line disables doors to the outside:
        //Door.GetComponent<Button> ().interactable = building.CheckDoorWindowPlacement (thisRoom.GetCentre (), thisPlus.GetCentre (), thisRoom.GetWidth (), thisRoom.GetLength ()) != null;
    }

    //On-Click Methods:
    public void AddDoor() {
        CutDoorOnPlusSign ();
        RoomData thisRoom = roomHandler.GetData ();
        GameObject collidingRoom = building.CheckDoorWindowPlacement (
            thisRoom.GetCentre (), thisPlus.GetCentre (), thisRoom.GetWidth (), thisRoom.GetLength ()
        );
        if (collidingRoom != null) {
            int wallIndex = thisPlus.GetWallIndex ();
            float[] plusLoc = thisPlus.GetCentre ();
            float[] roomLoc = thisRoom.GetCentre ();
            float[] colliderLoc = collidingRoom.GetComponent<RoomHandler> ().GetData ().GetCentre ();

            float doorLoc = (wallIndex % 2 == 0) ?
                -(plusLoc [0] + roomLoc [0] - colliderLoc [0]):
                plusLoc [2] + roomLoc [2] - colliderLoc [2];
            int oppositeWall = (wallIndex + 2) % 4;

            if (wallIndex < 2)
            {
                collidingRoom.GetComponent<RoomHandler>().AddDoor(oppositeWall, doorLoc);
            }
            else
            {
                collidingRoom.GetComponent<RoomHandler>().AddDoor(oppositeWall, -doorLoc);
            }
        }
    }
    public void AddPicture()
    {
        ImageMenu.transform.position = this.transform.position;
        ImageMenu.transform.rotation = this.transform.rotation;
        HideAllStillActive();
    }
    public void AddWindow()
    {
        int wallIndex = thisPlus.GetWallIndex();
        if (wallIndex ==0)
        {
            roomHandler.AddWindow(wallIndex, (int)thisPlus.GetCentre()[0]);
        }
        else if(wallIndex==1)
        {
            roomHandler.AddWindow(wallIndex, -(int)thisPlus.GetCentre()[2]);
        }
        else if (wallIndex == 2)
        {
            roomHandler.AddWindow(wallIndex, -(int)thisPlus.GetCentre()[0]);
        }
        else if (wallIndex == 3)
        {
            roomHandler.AddWindow(wallIndex, (int)thisPlus.GetCentre()[2]);
        }
        HideAll();
    }
    public void AddRoom()
    {
        this.CutDoorOnPlusSign();
        GameObject newRoom = building.AddRoom(ConvertToVector(thisPlus.GetNewRoom()));
        RoomHandler newRoomHandler = newRoom.GetComponent<RoomHandler>();
        int wallIndex = thisPlus.GetWallIndex();
        if (wallIndex == 0)
        {
            newRoomHandler.AddDoor(2, 0);
        }
        else if (wallIndex == 1)
        {
            newRoomHandler.AddDoor(3, 0);
        }
        else if (wallIndex == 2)
        {
            newRoomHandler.AddDoor(0, 0);
        }
        else if (wallIndex == 3)
        {
            newRoomHandler.AddDoor(1, 0);
        }
    }
    public void AddCorridor()
    {
        this.CutDoorOnPlusSign();
        int wallIndex = thisPlus.GetWallIndex();
        if (wallIndex == 0)
        {
            GameObject newCorridor = building.AddZCorridor(ConvertToVector(thisPlus.GetNewCorridor()));
            RoomHandler newCorridorHandler = newCorridor.GetComponent<RoomHandler>();
            newCorridorHandler.AddDoor(2, 0);
        }
        else if (wallIndex == 1)
        {
            GameObject newCorridor = building.AddXCorridor(ConvertToVector(thisPlus.GetNewCorridor()));
            RoomHandler newCorridorHandler = newCorridor.GetComponent<RoomHandler>();
            newCorridorHandler.AddDoor(3, 0);
        }
        else if (wallIndex == 2)
        {
            GameObject newCorridor = building.AddZCorridor(ConvertToVector(thisPlus.GetNewCorridor()));
            RoomHandler newCorridorHandler = newCorridor.GetComponent<RoomHandler>();
            newCorridorHandler.AddDoor(0, 0);
        }
        else if (wallIndex == 3)
        {
            GameObject newCorridor = building.AddXCorridor(ConvertToVector(thisPlus.GetNewCorridor()));
            RoomHandler newCorridorHandler = newCorridor.GetComponent<RoomHandler>();
            newCorridorHandler.AddDoor(1, 0);
        }
    }

    //Collider Checkers:
    private bool CheckRoomPlacement(string type){
        Vector3 newCentre = ConvertToVector (RoomTypes.GetNewRoomCentre (thisPlus, type));
        return building.CheckRoomPlacement (newCentre, type);
    }
    private Vector3 ConvertToVector(float[] data){
        return new Vector3 (data [0], data [1], data [2]);
    }

    private void CutDoorOnPlusSign()
    {
        int wallIndex = thisPlus.GetWallIndex();
        float[] roomCentre = roomHandler.GetData().GetCentre();
        if (wallIndex == 0)
        {
            roomHandler.AddDoor(wallIndex, (int)thisPlus.GetCentre()[0]-roomCentre[0]);
        }
        else if (wallIndex == 1)
        {
            roomHandler.AddDoor(wallIndex, -(int)thisPlus.GetCentre()[2] - roomCentre[2]);
        }
        else if (wallIndex == 2)
        {
            roomHandler.AddDoor(wallIndex, -(int)thisPlus.GetCentre()[0] - roomCentre[0]);
        }
        else if (wallIndex == 3)
        {
            roomHandler.AddDoor(wallIndex, (int)thisPlus.GetCentre()[2] - roomCentre[2]);
        }
        HideAll();
    }
}
