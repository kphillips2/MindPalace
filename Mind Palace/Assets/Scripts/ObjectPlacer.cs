﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  Each prefab that can be placed in the scene is associated with a number. This
 *  number is needed in order to use the createPrefab function. All prefabs can
 *  be found in the Assets -> Resources -> Objects folder.
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

    //Creates a prefab where num is the number that is associated with the prefab,
    //xcor and zcor are the xz coordinates of the prefab, and roty is the value that 
    //the prefab is rotated around the y-axis by
    public void createPrefab(int num, float xcor, float zcor, float roty)
    {
        string prefab;

        //Retrieve the prefab that corresponds to num
        switch (num)
        {
            case 0:
                prefab = "Bed";
                break;
            case 1:
                prefab = "BookCase";
                break;
            case 2:
                prefab = "Chair";
                break;
            case 3:
                prefab = "Chair2";
                break;
            case 4:
                prefab = "CoffeeTable";
                break;
            case 5:
                prefab = "Couch";
                break;
            case 6:
                prefab = "Counter";
                break;
            case 7:
                prefab = "Fireplace";
                break;
            case 8:
                prefab = "FlowerTable";
                break;
            case 9:
                prefab = "Fridge";
                break;
            case 10:
                prefab = "Guitar";
                break;
            case 11:
                prefab = "Headboard";
                break;
            case 12:
                prefab = "LionStatue";
                break;
            case 13:
                prefab = "Oven";
                break;
            case 14:
                prefab = "Plant";
                break;
            case 15:
                prefab = "Sink";
                break;
            case 16:
                prefab = "SoccerBall";
                break;
            case 17:
                prefab = "Table";
                break;
            case 18:
                prefab = "Toilet";
                break;
            case 19:
                prefab = "TV";
                break;
            default:
                Debug.Log("Invalid Prefab");
                return;
        }

        //Instanstiate the selected prefab with given position and rotation values
        GameObject newObj = Instantiate(Resources.Load("Objects/" + prefab), new Vector3(xcor, 0f, zcor), Quaternion.identity) as GameObject;
        newObj.transform.Rotate(0f, roty, 0f);
    }
}
