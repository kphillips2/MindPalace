using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class PictureSpaceFinder : MonoBehaviour {

    //given vectors
    private List<Vector3> doorPos = new List<Vector3>();
    //given sizes
    private float wallWidth;
    //private float wallHeight;     not needed here
    private float doorWidth;
    //private float doorHeight;     not needed here 
    int imageCount = 2;

    //to calculate
    private Vector3 doorVector;
    private List<Vector3> leftBounds;
    private List<Vector3> rightBounds;
    private List<Vector3> widthVectors;

    void Start(Vector3 rightCorner, Vector3 leftCorner, float doorWidth, List<List<string>> images)
    {
        CalculateDoorVector(rightCorner, leftCorner, doorWidth);
        GetSpaces(rightCorner, leftCorner);
        for(int i=0; i<leftBounds.Count; i++)
        {
            PictureSpacePlacer psp = new PictureSpacePlacer();
            psp.findSpace(leftBounds.ElementAt(i), rightBounds.ElementAt(i), widthVectors.ElementAt(i), imageCount);
        }
    }

    private void CalculateDoorVector(Vector3 rightCorner, Vector3 leftCorner, float doorWidth)
    {
        //case wall 0
        if (rightCorner.x - leftCorner.x > 0)
        {
            doorVector = new Vector3(doorWidth, 0, 0);
        }
        //case wall 1
        else if (rightCorner.z - leftCorner.z < 0)
        {
            doorVector = new Vector3(0, 0, -doorWidth);
        }
        //case wall 2
        else if(rightCorner.x - leftCorner.x < 0)
        {
            doorVector = new Vector3(-doorWidth, 0, 0);
        }
        //case wall 3
        else if (rightCorner.z - leftCorner.z > 0)
        {
            doorVector = new Vector3(0, 0, doorWidth);
        }
    }
	
	//finds wallspace for hanging pictures
	private void GetSpaces(Vector3 leftCorner, Vector3 rightCorner){
		
        //if no open doors, add entire wall
        if (doorPos.Count == 0){
			leftBounds.Add(leftCorner);
            rightBounds.Add(rightCorner);
            widthVectors.Add(rightCorner - leftCorner);
            return;
		}

		//if there is at least one open door, add open wall bounds
		else{
            Vector3 currLeftBound = leftCorner;
            for(int i=0; i<doorPos.Count; i++)
            {
                leftBounds.Add(currLeftBound);
                rightBounds.Add(doorPos.ElementAt(i)-((1/2)*doorVector));
                widthVectors.Add((doorPos.ElementAt(i) - ((1 / 2) * doorVector))-currLeftBound);
                currLeftBound = (doorPos.ElementAt(i) + ((1/2)* doorVector));
            }
		}
	}
		
	
		
}