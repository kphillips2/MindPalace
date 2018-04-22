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
    public GameObject XButton;

    private Building building;
    private RoomHandler roomHandler;
    private PlusData thisPlus;

    /// <summary>
    /// Initializes the attributes for this plus sign.
    /// </summary>
    /// <param name="plus"></param>
    public void InitData(PlusData plus){
        roomHandler = room.GetComponent<RoomHandler> ();
        building = level.GetComponent<Building> ();
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

        ActivationManager.ActivateMenu (gameObject);
    }
    public void HideAll(){
        ClickedOn.SetActive (false);
        RoomButton.SetActive (false);
        Window.SetActive (false);
        Picture.SetActive (false);
        CorridorButton.SetActive (false);
        DoorSubMenu.SetActive (false);
        Door.SetActive (false);
        ActivationManager.NoActive ();
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
        CutDoorOnPlusSign();
        RoomData thisRoom = roomHandler.GetData ();
        float[] plusLoc ={ this.transform.position.x, this.transform.position.y, this.transform.position.z };
        float[] plusLocRoss = thisPlus.GetCentre();
        GameObject collidingRoom = building.CheckDoorWindowPlacement (
            thisRoom.GetCentre (), plusLoc, thisRoom.GetWidth (), thisRoom.GetLength ()
        );
        print(collidingRoom != null);
        if (collidingRoom != null) {
            int wallIndex = thisPlus.GetWallIndex();
            float[] doorLoc = building.GetNewDoorLoc(thisRoom.GetCentre(), plusLoc, thisRoom.GetWidth(), thisRoom.GetLength());
            print("X:"+doorLoc[0]+", Y:"+ doorLoc[1]+ ", Z:"+ doorLoc[2]);
            float[] colliderLoc = collidingRoom.GetComponent<RoomHandler>().GetData().GetCentre();
            float doorCordinate;
            switch (wallIndex)
            {
                case 0:
                    doorCordinate = -(doorLoc[0] - colliderLoc[0]);
                    break;
                case 1:
                    doorCordinate = doorLoc[2] - colliderLoc[2];
                    break;
                case 2:
                    doorCordinate = doorLoc[0] - colliderLoc[0];
                    break;
                default://case:3
                    doorCordinate = -(doorLoc[2] - colliderLoc[2]);
                    break;
            }
            collidingRoom.GetComponent<RoomHandler>().AddDoor((wallIndex+2)%4, doorCordinate);
            /*
            int wallIndex = thisPlus.GetWallIndex ();
            float[] roomLoc = thisRoom.GetCentre ();
            float[] colliderLoc = collidingRoom.GetComponent<RoomHandler> ().GetData ().GetCentre ();

            print("--------------------------");
            print("PlusLoc:" + this.transform.position.x + ',' + this.transform.position.z);
            print("RossPlusLoc" + plusLocRoss[0]+','+ plusLocRoss[2]);
            print("RoomLoc:" + roomLoc[0] + ',' + roomLoc[2]);
            print("ColliderLoc:" + colliderLoc[0] + ',' + colliderLoc[2]);
            print("--------------------------");

            float doorLoc = (wallIndex % 2 == 0) ?
                -(this.transform.position.x - roomLoc [0] - colliderLoc [0]):
                this.transform.position.z - roomLoc [2] - colliderLoc [2];
            int oppositeWall = (wallIndex + 2) % 4;

            if (wallIndex < 2)
            {
                collidingRoom.GetComponent<RoomHandler>().AddDoor(oppositeWall, doorLoc);
            }
            else
            {
                collidingRoom.GetComponent<RoomHandler>().AddDoor(oppositeWall, -doorLoc);
            }
            */
        }
    }
    public void AddPicture()
    {
        ImageMenu.GetComponentInChildren<ButtonListControl>().currentRoom = room;
        ImageMenu.transform.position = this.transform.position;
        ImageMenu.transform.rotation = this.transform.rotation;
        HideAllStillActive();
    }
    public void AddWindow()
    {
        int wallIndex = thisPlus.GetWallIndex();
        float windowLoc;
        //X Button start:
        GameObject component = Instantiate(
               XButton,
               Vector3.zero,
               Quaternion.Euler(0, 0, 0)
           ) as GameObject;
        component.transform.position = this.transform.position;
        component.transform.rotation = this.transform.rotation;
        component.SetActive(true);
        XData thisX = component.GetComponent<XData>();
        thisX.PlusSignThisReplaces = thisPlus;
        thisX.offset = new Vector3(0, 0, 0);
        thisX.wallIndex = wallIndex;
        thisX.AssignedToWindow = true;
        //X End
        if (wallIndex ==0)
        {
            windowLoc = this.transform.position.x - roomHandler.GetData().GetCentre()[0];
            thisX.wallLoc = windowLoc;
            roomHandler.AddWindow(wallIndex, windowLoc);
        }
        else if(wallIndex==1)
        {
            windowLoc = -(this.transform.position.z - roomHandler.GetData().GetCentre()[2]);
            thisX.wallLoc = windowLoc;
            roomHandler.AddWindow(wallIndex, windowLoc);
        }
        else if (wallIndex == 2)
        {
            windowLoc = -(this.transform.position.x - roomHandler.GetData().GetCentre()[0]);
            thisX.wallLoc = windowLoc;
            roomHandler.AddWindow(wallIndex, windowLoc);
        }
        else if (wallIndex == 3)
        {
            windowLoc = this.transform.position.z - roomHandler.GetData().GetCentre()[2];
            thisX.wallLoc = windowLoc;
            roomHandler.AddWindow(wallIndex, windowLoc);
        }
    }
    public void AddRoom()
    {
        CutDoorOnPlusSign();
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

    // Collider Checkers:
    private bool CheckRoomPlacement(string type){
        Vector3 newCentre = ConvertToVector (RoomTypes.GetNewRoomCentre (thisPlus, type));
        return building.CheckRoomPlacement (newCentre, type);
    }
    private Vector3 ConvertToVector(float[] data){
        return new Vector3 (data [0], data [1], data [2]);
    }
    public PlusData GetData() { return thisPlus; }

    private void CutDoorOnPlusSign()
    {
        int wallIndex = thisPlus.GetWallIndex();
        float[] roomCentre = roomHandler.GetData().GetCentre();
        if (wallIndex == 0)
        {
            roomHandler.AddDoor(wallIndex, this.transform.position.x-roomCentre[0]);
        }
        else if (wallIndex == 1)
        {
            roomHandler.AddDoor(wallIndex, -(this.transform.position.z - roomCentre[2]));
        }
        else if (wallIndex == 2)
        {
            roomHandler.AddDoor(wallIndex, -(this.transform.position.x - roomCentre[0]));
        }
        else if (wallIndex == 3)
        {
            roomHandler.AddDoor(wallIndex, this.transform.position.z - roomCentre[2]);
        }
        //HideAll();
    }

    public void PlaceXOverImage(List<GameObject> image)
    {
        GameObject component = Instantiate(
               XButton,
               Vector3.zero,
               Quaternion.Euler(0, 0, 0)
           ) as GameObject;
        Vector3 OffsetFromWall ;
        switch (thisPlus.GetWallIndex())
        {
            case 0:
                OffsetFromWall = new Vector3(0, 0, -0.1f);
                break;
            case 1:
                OffsetFromWall = new Vector3(-0.1f, 0, 0);
                break;
            case 2:
                OffsetFromWall = new Vector3(0, 0, 0.1f);
                break;
            default:
                OffsetFromWall = new Vector3(0.1f, 0, 0);
                break;
        }
        component.transform.position = this.transform.position+OffsetFromWall;
        component.transform.rotation = this.transform.rotation;
        component.SetActive(true);
        XData thisX = component.GetComponent<XData>();
        thisX.PlusSignThisReplaces = thisPlus;
        thisX.offset = OffsetFromWall;
        thisX.wallIndex = thisPlus.GetWallIndex();
        thisX.AssignedToWindow = false;
        thisX.ImagePlacedOver = image;
    }
}
