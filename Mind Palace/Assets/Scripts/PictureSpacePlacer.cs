using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PictureSpacePlacer : MonoBehaviour {

    //given
    private float paddingScale = 1/8;

    //to calculate
    private Vector3 spacingVector;
    private float picSize;

    /**
     * calculates picture locations for a wallspace
     */
    void Start()
	{
		
    }

    public List<Vector3> findSpace(Vector3 leftBound, Vector3 rightBound, Vector3 widthVector, int imageCount)
    {
        //wallspace given in tuples, break into leftBound and rightBound
        CalculateSpacingVector(widthVector, imageCount);
        CalculateImageSize(widthVector, imageCount);
        return CalculatePicPos(leftBound, imageCount);
    }

    private void CalculateSpacingVector(Vector3 widthVector, int imageCount)
    {
        spacingVector = widthVector / (imageCount+1);
    }

    private void CalculateImageSize(Vector3 widthVector, int imageCount)
    {
        // get positive image scalar, account for padding
        if(widthVector.x + widthVector.y + widthVector.z > 0)
        {
            picSize = (1 - paddingScale) * (widthVector.x + widthVector.y + widthVector.z) / imageCount;
            return;
        }
        else
        {
            picSize = -(1 - paddingScale) * (widthVector.x + widthVector.y + widthVector.z) / imageCount;
            return;
        }
    }
	
	//finds wallspace for hanging pictures
	private List<Vector3> CalculatePicPos(Vector3 leftBound, int imageCount){
        Vector3 currLeftBound = leftBound;
        List<Vector3> picPos = new List<Vector3>();
		for(int i=0; i<imageCount; i++)
        {
            picPos.Add(currLeftBound + (1 / 2 * spacingVector));
            currLeftBound += spacingVector;
        }
        return picPos;
	}	
}