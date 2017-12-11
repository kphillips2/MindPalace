using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  Each prefab that can be placed in the scene is associated with a number. This
 *  number is needed in order to use the createPrefab function. All prefabs can
 *  be found in the Assets -> Prefabs -> Objects folder.
 *  
 *  KEY:
 *  0 - bed      
 *  1 - bookCase
 *  2 - chair
 *  3 - chair2
 *  4 - coffeeTable
 *  5 - couch
 *  6 - counter
 *  7 - fireplace
 *  8 - flowerTable
 *  9 - fridge
 *  10 - guitar
 *  11 - headboard
 *  12 - lionStatue
 *  13 - oven
 *  14 - plant
 *  15 - sink
 *  16 - soccerBall
 *  17 - table
 *  18 - toilet
 *  19 - tv
 */

public class ObjectPlacer : MonoBehaviour {

    //Prefabs
    public Transform bed, bookCase, chair, chair2, coffeeTable, couch, counter, 
        fireplace, flowerTable, fridge, guitar, headboard, lionStatue, oven, plant, 
        sink, soccerBall, table, toilet, tv;

	//Adds objects to scene on start. In future, these values will be retrieved from
    //the loci files
	void Start () {
        createPrefab(0, -3.468538f, -12.47951f, 0f); //Bed
        createPrefab(1, 4.15f, 8.72f, -90f); //Book cases
        createPrefab(1, 4.15f, 10.43f, -90f);
        createPrefab(1, -4.16f, -6.81f, 90f);
        createPrefab(1, -4.44f, 7.66f, 90f);
        createPrefab(2, 2.7956f, -3.76479f, 0f); //Chair
        createPrefab(3, 19.07f, -7.08f, 90f); //Chair2
        createPrefab(3, 21.93f, -8.543f, 270f);
        createPrefab(3, 19.07f, -8.543f, 90f);
        createPrefab(3, 21.92f, -7.08f, 270f);
        createPrefab(4, -1.18f, 12.39f, 90f); //Coffee table
        createPrefab(5, -3.47f, 12.44f, 90f); //Couches
        createPrefab(5, 1f, 12.24f, -90f);
        createPrefab(5, 1.31f, -9.26f, 90f);
        createPrefab(6, 16f, -7.22f, 90f); //Counters
        createPrefab(6, 16f, -8.52f, 90f);
        createPrefab(7, -4.09f, 0.05f, 90f); //Fireplace
        createPrefab(8, 4.0925f, -3.876518f, 0f); //Flower tables
        createPrefab(8, -3.865167f, 3.955207f, 0f);
        createPrefab(8, -3.643468f, -3.745927f, 0f);
        createPrefab(8, 4.133508f, 3.98855f, 0f);
        createPrefab(8, 34.03f, 10.99f, 0f);
        createPrefab(8, 33.83f, 3.35468f, 0f);
        createPrefab(9, 16.12f, -11.23f, 90f); //Fridge
        createPrefab(10, 0.3411262f, -14.19815f, 0f); //Guitar
        createPrefab(11, 3.07f, -14.07f, 0f); //Headboard
        createPrefab(12, 33.44f, -0.1f, -90f); //Lion statue
        createPrefab(13, 16.13f, -9.81f, 90f); //Oven
        createPrefab(14, 23.8f, -3.97f, 0f); //Plants
        createPrefab(14, 3.810411f, 13.44f, 0f);
        createPrefab(14, 3.810411f, 6.19f, 0f);
        createPrefab(15, 34.25f, 5.94f, -90f); //Sink
        createPrefab(16, 1.01692f, -12.85651f, 0f); //Soccer ball
        createPrefab(17, 20.4941f, -7.77f, 90f); //Table
        createPrefab(18, 34.13f, 7.88f, -90f); //Toilet
        createPrefab(19, 3.937f, -9.35f, -90f); //TV
    }

    //Creates a prefab where num is the number that is associated with the prefab,
    //xcor and zcor are the xz coordinates of the prefab, and roty is the value that 
    //the prefab is rotated around the y-axis by
    public void createPrefab(int num, float xcor, float zcor, float roty)
    {
        Transform prefab;

        //Retrieve the prefab that corresponds to num
        switch (num)
        {
            case 0:
                prefab = bed;
                break;
            case 1:
                prefab = bookCase;
                break;
            case 2:
                prefab = chair;
                break;
            case 3:
                prefab = chair2;
                break;
            case 4:
                prefab = coffeeTable;
                break;
            case 5:
                prefab = couch;
                break;
            case 6:
                prefab = counter;
                break;
            case 7:
                prefab = fireplace;
                break;
            case 8:
                prefab = flowerTable;
                break;
            case 9:
                prefab = fridge;
                break;
            case 10:
                prefab = guitar;
                break;
            case 11:
                prefab = headboard;
                break;
            case 12:
                prefab = lionStatue;
                break;
            case 13:
                prefab = oven;
                break;
            case 14:
                prefab = plant;
                break;
            case 15:
                prefab = sink;
                break;
            case 16:
                prefab = soccerBall;
                break;
            case 17:
                prefab = table;
                break;
            case 18:
                prefab = toilet;
                break;
            case 19:
                prefab = tv;
                break;
            default:
                Debug.Log("Invalid Prefab");
                return;
        }

        //Instanstiate the selected prefab with given position and rotation values
        Transform newObj = Instantiate(prefab, new Vector3(xcor, 0f, zcor), Quaternion.identity);
        newObj.Rotate(0f, roty, 0f);
    }
}
