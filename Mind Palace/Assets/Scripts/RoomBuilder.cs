using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour {
    public GameObject floor;
    public GameObject filledWall;
    public GameObject doorWall;

    private GameObject[] components;
    private Vector3 roomCentre;

	// Use this for initialization
	void Start (){
        components = new GameObject[5];
        roomCentre = new Vector3(0, 0, 0);
        addDoors();
        addWall();
	}

    private void addDoors(){
        components[1] = Instantiate(
            doorWall,
            roomCentre,
            Quaternion.Euler(0,-90,0)
        ) as GameObject;
    }
	private void addWall(){
        components[1] = Instantiate(
            filledWall,
            roomCentre,
            Quaternion.Euler(0, 90, 0)
        ) as GameObject;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
