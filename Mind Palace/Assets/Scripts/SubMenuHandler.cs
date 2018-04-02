using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
