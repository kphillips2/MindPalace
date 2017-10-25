using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour {
    public GameObject smallSquareRoom;
    public GameObject regularDoor;
    private GameObject startRoom;
    private Vector3 scale;
    private GameObject door;
    private DoorFiller doorFiller;

    // Use this for initialization
    void Start()
    {
        scale = new Vector3(40, 40, 40);
        doorFiller = new DoorFiller();
        initStartRoom();
    }
    private void initStartRoom()
    {
        startRoom = Instantiate(
            smallSquareRoom,
            Vector3.zero,
            Quaternion.identity
        ) as GameObject;
        startRoom.transform.localScale += scale;

        door = Instantiate(
           regularDoor,
           new Vector3(0, 0, -4.2f),
           Quaternion.identity
        ) as GameObject;
        door.transform.localScale += scale;

        door = Instantiate(
            regularDoor,
            new Vector3(-4.2f, 0, 0),
            Quaternion.Euler(0, -90, 0)
        ) as GameObject;
        door.transform.localScale += scale;

        door = Instantiate(
            regularDoor,
            new Vector3(4.2f, 0, 0),
            Quaternion.Euler(0, 90, 0)
        ) as GameObject;
        door.transform.localScale += scale;
    }
}
