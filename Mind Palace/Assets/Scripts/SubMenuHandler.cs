using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubMenuHandler : MonoBehaviour {
    public GameObject room;
    public GameObject level;

    public GameObject ClickedOn;
    public GameObject RoomButton;
    public GameObject CorridorButton;
    public GameObject Picture;
    public GameObject Window;
    public GameObject SingularActivation;
    public GameObject ImageMenu;

    private Building building;
    private RoomHandler roomHandler;
    private ActivationManager MenuActivationManager;
    private PlusData thisPlus;
    private Vector3 newRoomCentre;
    private Vector3 newCorridorCentre;

    // Use this for initialization
    void Start () {
        roomHandler = room.GetComponent<RoomHandler> ();
        building = level.GetComponent<Building> ();
        MenuActivationManager = SingularActivation.GetComponent<ActivationManager> ();
        newRoomCentre = GetNewRoomCentre ("room");
        newCorridorCentre = GetNewRoomCentre (GetCorridorType ());
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
        RoomButton.GetComponent<Button> ().interactable = CheckRoomPlacement ("room");
        CorridorButton.GetComponent<Button> ().interactable = CheckRoomPlacement (GetCorridorType ());
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
        this.transform.position = new Vector3(0, -100, 0);
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
    //Collider Checkers:
    private string GetCorridorType(){
        return (thisPlus.GetAngle () % 180 == 0) ? "zCorridor" : "xCorridor";
    }
    private bool CheckRoomPlacement(string type){
        Vector3 newCentre;
        switch (type) {
            case "room":
                newCentre = newRoomCentre;
                break;
            case "xCorridor":
                newCentre = newCorridorCentre;
                break;
            case "zCorridor":
                newCentre = newCorridorCentre;
                break;
            default:
                return false;
        }

        return building.CheckRoomPlacement (newCentre, type);
    }
    private Vector3 GetNewRoomCentre(string type){
        RoomData data = roomHandler.GetData ();

        Vector3 centre = ConvertToVector(thisPlus.GetCentre ());
        Vector3 roomCentre = ConvertToVector(data.GetCentre ());
        centre = Quaternion.AngleAxis(thisPlus.GetAngle (), roomCentre) * centre;

        float width = data.GetWidth (), length = data.GetLength ();
        return building.GetNewRoomCentre (roomCentre, centre, width, length, type);
    }
    private Vector3 ConvertToVector(float[] data){
        return new Vector3 (data [0], data [1], data [2]);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
