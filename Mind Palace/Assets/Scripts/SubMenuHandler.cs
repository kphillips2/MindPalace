using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for handling and plus sign functionality.
/// </summary>
public class SubMenuHandler : MonoBehaviour {
    public GameObject level;
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
        roomHandler = gameObject.GetComponent<RoomHandler> ();
        building = level.GetComponent<Building> ();
        MenuActivationManager = SingularActivation.GetComponent<ActivationManager> ();
        thisPlus = plus;
    }
    //Menu Manipulators:
    public void ShowMoreOptions()
    {
        ClickedOn.SetActive(false);
        RoomButton.SetActive(false);
        CorridorButton.SetActive(false);
        Window.SetActive(true);
        Picture.SetActive(true);
        DoorSubMenu.SetActive(true);
        Door.SetActive(false);
        MenuActivationManager.ActivateMenu(this.gameObject);

        string type = "room";
        RoomButton.GetComponent<Button> ().interactable = CheckRoomPlacement (type);
        type = RoomTypes.GetCorridorType (thisPlus.GetAngle ());
        CorridorButton.GetComponent<Button> ().interactable = CheckRoomPlacement (type);
    }
    public void HideAll()
    {
        ClickedOn.SetActive(false);
        RoomButton.SetActive(false);
        Window.SetActive(false);
        Picture.SetActive(false);
        CorridorButton.SetActive(false);
        DoorSubMenu.SetActive(false);
        Door.SetActive(false);
        MenuActivationManager = SingularActivation.GetComponent<ActivationManager>();
        MenuActivationManager.NoActive();
        this.transform.position = new Vector3(0, -100, 0);
    }
    public void HideAllStillActive()
    {
        ClickedOn.SetActive(false);
        RoomButton.SetActive(false);
        Window.SetActive(false);
        Picture.SetActive(false);
        CorridorButton.SetActive(false);
        DoorSubMenu.SetActive(false);
        Door.SetActive(false);
    }
    public void DefaultState()
    {
        ClickedOn.SetActive(true);
        RoomButton.SetActive(false);
        Window.SetActive(false);
        Picture.SetActive(false);
        CorridorButton.SetActive(false);
        DoorSubMenu.SetActive(false);
        Door.SetActive(false);
    }

    public void OpenDoorSubMenu()
    {
        ClickedOn.SetActive(false);
        RoomButton.SetActive(true);
        Window.SetActive(false);
        Picture.SetActive(false);
        CorridorButton.SetActive(true);
        DoorSubMenu.SetActive(false);
        Door.SetActive(true);
    }

    public void AddDoor()
    {

    }

    //Collider Checkers:
    private bool CheckRoomPlacement(string type){
        Vector3 newCentre = ConvertToVector (RoomTypes.GetNewRoomCentre (thisPlus, type));
        return building.CheckRoomPlacement (newCentre, type);
    }
    private Vector3 ConvertToVector(float[] data){
        return new Vector3 (data [0], data [1], data [2]);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
