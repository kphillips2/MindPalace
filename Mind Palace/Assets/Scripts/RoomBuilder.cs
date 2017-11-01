using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBuilder : MonoBehaviour {
    public GameObject floor;
    public GameObject filledWall;
    public GameObject doorWall;

    private GameObject component;
	
    // Use this for initialization
	void Start (){
    }

    public void addDoors(Vector3 centre){
        component = Instantiate(
            doorWall,
            centre,
            Quaternion.Euler(0,-90,0)
        ) as GameObject;
    }
	public void addWalls(Vector3 centre){
        component = Instantiate(
            filledWall,
            centre,
            Quaternion.Euler(0, 90, 0)
        ) as GameObject;
    }
}
