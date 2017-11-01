using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {
    public GameObject room;

    private GameObject currentRoom;
    private RoomBuilder roomBuilder;

    // Use this for initialization
    void Start () {
        roomBuilder = room.GetComponent<RoomBuilder>();
        currentRoom = Instantiate(
            room,
            new Vector3(0,0,20),
            Quaternion.Euler(0, 0, 0)
        ) as GameObject;
        roomBuilder.addDoors(new Vector3(0, 0, 20));
        roomBuilder.addWalls(new Vector3(0, 0, 20));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
