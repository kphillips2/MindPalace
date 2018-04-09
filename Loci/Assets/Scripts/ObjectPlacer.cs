using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

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
    //pos are the position coordinates of the prefab, and rot is the value that 
    //the prefab is rotated by. If you want the user to be able to
    //move the object, set isGrabbable to true, otherwise set to false.
    public void createPrefab(int num, Vector3 pos, Vector3 rot, bool isGrabbable)
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

        if (isGrabbable) prefab += "_G";

        //Instanstiate the selected prefab with given position and rotation values
        GameObject newObj = Instantiate(Resources.Load("Objects/" + prefab), pos, Quaternion.identity) as GameObject;
        newObj.transform.Rotate(rot.x, rot.y, rot.z);
    }

    //Creates a prefab where num is the number that is associated with the prefab and places
    //it in the user's right hand
    public void createPrefabInHand(int num)
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

        prefab += "_G";

        //Find controller
        //GameObject controllerGameObj = GameObject.Find("RightController");
        GameObject controllerGameObj = VRTK_DeviceFinder.GetControllerRightHand();
        //Instanstiate the selected prefab
        GameObject gameObj = Instantiate(Resources.Load("Objects/" + prefab), controllerGameObj.transform.position, Quaternion.identity) as GameObject;
        //Place prefab in user's hand
        VRTK_InteractGrab myGrab = controllerGameObj.GetComponent<VRTK_InteractGrab>();
        myGrab.interactTouch.ForceStopTouching();
        myGrab.interactTouch.ForceTouch(gameObj.gameObject);
        myGrab.AttemptGrab();
    }
}
