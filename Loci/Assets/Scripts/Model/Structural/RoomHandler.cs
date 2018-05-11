using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// Responsible for handling and room functionality.
/// </summary>
public class RoomHandler : MonoBehaviour
{
    public GameObject plusSign;
    public GameObject floor;
    public GameObject roof;

    public GameObject posZWall;
    public GameObject posXWall;
    public GameObject negZWall;
    public GameObject negXWall;

    private List<Vector3>[] doorsAndWindows;
    private RoomData thisRoom;
    public string fileLoc = "Assets/Resources/";

    /// <summary>
    /// Initializes the attributes for this room.
    /// </summary>
    public void InitData(float width, float length) {
        doorsAndWindows = new List<Vector3> [4];
        for (int k = 0; k < 4; k++)
            doorsAndWindows [k] = new List<Vector3> ();
        string[] mats = { "Wood Texture 06", "Wood Texture 15", "Wood Texture 12" };

        thisRoom = new RoomData (new float[] { floor.transform.position.x, 0, floor.transform.position.z });
        SetRoomSize (width, length);
        SetMaterials (mats [0], mats [1], mats [2]);
    }
    /// <summary>
    /// Retrieves all the information that will apear in the save file for this room.
    /// </summary>
    /// <returns> the save object that contains the room information </returns>
    public RoomData GetData() { return thisRoom; }
    public void SetData(RoomData data) { thisRoom = data; }
    /// <summary>
    /// Changes the dimensions of a room without scaling it.
    /// </summary>
    /// <param name="width"> determines the size of the room along the X axis </param>
    /// <param name="length"> determines the size of the room along the Z axis </param>
    public void SetRoomSize(float width, float length) {
        Vector3 dimensions = new Vector3 (width, 0.25f, length);
        FloorResizer.resize (floor, dimensions);
        FloorResizer.resize (roof, dimensions);

        posZWall.transform.localPosition = new Vector3 (0, 2.5f, length / 2 - 0.125f);
        posXWall.transform.localPosition = new Vector3 (0, 2.5f, width / 2 - 0.125f);
        negZWall.transform.localPosition = new Vector3 (0, 2.5f, length / 2 - 0.125f);
        negXWall.transform.localPosition = new Vector3 (0, 2.5f, width / 2 - 0.125f);

        thisRoom.SetRoomSize(width, length);

        AdjustWall (posZWall, 0);
        AdjustWall (posXWall, 1);
        AdjustWall (negZWall, 2);
        AdjustWall (negXWall, 3);
    }
    /// <summary>
    /// Changes the materials of a room.
    /// </summary>
    /// <param name="floorName"> the name of the material to be attached to the floor </param>
    /// <param name="roofName"> the name of the material to be attached to the roof </param>
    /// <param name="wallName"> the name of the material to be attached to the walls </param>
    public void SetMaterials(string floorName, string roofName, string wallName) {
        Material floorMat = Resources.Load ("Materials/" + floorName, typeof(Material)) as Material;
        Material roofMat = Resources.Load ("Materials/" + roofName, typeof(Material)) as Material;
        Material wallMat = Resources.Load ("Materials/" + wallName, typeof(Material)) as Material;

        floorMat.mainTexture.wrapMode = TextureWrapMode.Repeat;
        roofMat.mainTexture.wrapMode = TextureWrapMode.Repeat;
        wallMat.mainTexture.wrapMode = TextureWrapMode.Repeat;

        floor.GetComponent<Renderer> ().material = floorMat;
        roof.GetComponent<Renderer> ().material = roofMat;

        posZWall.GetComponent<Renderer> ().material = wallMat;
        posXWall.GetComponent<Renderer> ().material = wallMat;
        negZWall.GetComponent<Renderer> ().material = wallMat;
        negXWall.GetComponent<Renderer> ().material = wallMat;

        thisRoom.SetMaterials (floorName, roofName, wallName);
    }
    /// <summary>
    /// Sets all the doors and windows of a room at once.
    /// </summary>
    /// <param name="wallData"> a list per wall and a float array for each door or window vector </param>
    public void SetWallData(List<float[]>[] wallData) {
        for (int k = 0; k < 4; k++) {
            doorsAndWindows [k] = new List<Vector3> ();
            foreach (float[] loc in wallData [k])
                doorsAndWindows [k].Add (new Vector3 (loc [0], loc [1], loc [2]));
        }

        AdjustWall (posZWall, 0);
        AdjustWall (posXWall, 1);
        AdjustWall (negZWall, 2);
        AdjustWall (negXWall, 3);
    }
    /// <summary>
    /// Adds all pictures to room and to roomdata
    /// </summary>
    /// <param name="pictures"> a list of pictures in the room </param>
    public void SetPictures(List<Picture> pictures) {
        PictureCreator pc = new PictureCreator ();
        foreach (Picture p in pictures)
            pc.placePicture(p.getImgData (), p.getRoty (), p.getLocation ());
        thisRoom.SetPictures (pictures);
    }
    /// <summary>
    /// Saves this room and all its information to the currently open Loci.
    /// </summary>
    public void Save() {
        List<float[]>[] wallData = new List<float[]> [4];

        for (int k = 0; k < 4; k++) {
            wallData [k] = new List<float[]> ();
            foreach (Vector3 loc in doorsAndWindows [k])
                wallData [k].Add(new float[] { loc.x, loc.y, loc.z });
        }

        thisRoom.SetWallData (wallData);
        SaveFile.currentLoci.addRoom (thisRoom);
    }
    /// <summary>
    /// Adds plus signs to all locations on the walls.
    /// </summary>
    public void AddPlusSigns(){
        float wallLimit;
        FileStream file = File.Create (fileLoc + "pluslocs.txt");
        file.Close ();
        for (int k = 0; k < 4; k++) {
            wallLimit = GetWallSize (k) / 2;
            for (float loc = wallLimit - 2; loc > -wallLimit; loc -= 4)
                AddPlusSign (k, loc);
        }
    }
    /// <summary>
    /// Adds a plus sign to a given wall. Also saves the new plus sign to the room information.
    /// </summary>
    /// <param name="wallIndex"> the index of the wall being changed </param>
    /// <param name="plusLoc"> the location of the menu with 0 representing the centre of the wall </param>
    public void AddPlusSign(int wallIndex, float plusLoc) {
        if (wallIndex >= 0 && wallIndex <= 3) {
            float angle =
                (wallIndex == 1) ? 90 :
                (wallIndex == 2) ? 180 :
                (wallIndex == 3) ? 270 : 0;

            float dist = GetWallSize ((wallIndex + 1) % 4) / 2 - 0.3f;
            Vector3 centre = new Vector3 (plusLoc, 2.5f, dist);
            Vector3 roomCentre = floor.transform.position + new Vector3 (0, 0.125f, 0);

            GameObject component = Instantiate (
                plusSign,
                Vector3.zero,
                Quaternion.Euler (0, 0, 0)
            ) as GameObject;
            component.transform.Translate (roomCentre);
            component.transform.rotation = Quaternion.Euler (0, angle, 0);
            component.transform.Translate (centre);
            component.SetActive (true);

            component.transform.SetParent (this.transform);
            centre = component.transform.position;

            float[] newRoom = RoomTypes.GetNewRoomCentre (
                roomCentre, centre,
                thisRoom.GetWidth (), thisRoom.GetLength (), "room"
            );
            float[] newCorridor = RoomTypes.GetNewRoomCentre (
                roomCentre, centre,
                thisRoom.GetWidth (), thisRoom.GetLength (), RoomTypes.GetCorridorType (angle)
            );
            float[] plusCentre = { centre.x, centre.y, centre.z };

            StreamWriter writer = new StreamWriter (fileLoc + "pluslocs.txt", true);
            writer.WriteLine("<" + plusCentre [0] + ", " + plusCentre [1] + ", " + plusCentre [2] + ">");
            writer.Close ();

            PlusData thisPlus = new PlusData (plusCentre, newRoom, newCorridor, angle);
            component.GetComponent<SubMenuHandler> ().InitData (thisPlus, gameObject);
            thisRoom.AddPlusSign (thisPlus);
        }
        else
            Debug.LogError ("A wall with index of {" + wallIndex + "} doesn't exist.");
    }
    /// <summary>
    /// Adds a picture to the saved information for this room.
    /// </summary>
    /// <param name="picture"> a picture object to be added to the room information </param>
    public void AddPicture(Picture picture) { thisRoom.AddPicture (picture); }
    /// <summary>
    /// Adds a door to a given wall.
    /// </summary>
    /// <param name="wallIndex"> the index of the wall being changed </param>
    /// <param name="doorLoc"> the location of the door with 0 representing the centre of the wall </param>
	public void AddDoor(int wallIndex, float doorLoc) {
        // wall numbers correspond with indices of doorStates
        //   ----0----
        //  |         |
        //  |         |
        //  3         1
        //  |         |
        //  |         |
        //   ----2----

        GameObject component;
        if ((component = GetWallObject (wallIndex)) != null)
            AdjustForDoor (component, wallIndex, doorLoc);
        else
            Debug.LogError("A wall with index of {" + wallIndex + "} doesn't exist.");
    }
    /// <summary>
    /// Adds a window to a given wall.
    /// </summary>
    /// <param name="wallIndex"> the index of the wall being changed </param>
    /// <param name="windowLoc"> the location of the window with 0 representing the centre of the wall </param>
    public void AddWindow(int wallIndex, float windowLoc) {
        // wall numbers correspond with indices of doorStates
        //   ----0----
        //  |         |
        //  |         |
        //  3         1
        //  |         |
        //  |         |
        //   ----2----

        GameObject component;
        if ((component = GetWallObject (wallIndex)) != null)
            AdjustForWindow (component, wallIndex, windowLoc);
        else
            Debug.LogError ("A wall with index of {" + wallIndex + "} doesn't exist.");
    }
    /// <summary>
    /// Gets the length of a wall given its index.
    /// </summary>
    /// <param name="wallIndex"> the index of the wall being checked </param>
    /// <returns> the length of the wall </returns>
    private float GetWallSize(int wallIndex) {
        return (wallIndex % 2 == 0) ? thisRoom.GetWidth () : thisRoom.GetLength ();
    }
    /// <summary>
    /// Gets the wall object given its index.
    /// </summary>
    /// <param name="wallIndex"> the index of the wall being checked </param>
    /// <returns> the wall object </returns>
    private GameObject GetWallObject(int wallIndex) {
        return
            (wallIndex == 1) ? posXWall :
            (wallIndex == 2) ? negZWall :
            (wallIndex == 3) ? negXWall :
            (wallIndex == 0) ? posZWall : null;
    }
    /// <summary>
    /// Cuts all doors and windows in a wall after adding a new door.
    /// </summary>
    /// <param name="input"> the wall object being changed </param>
    /// <param name="wallIndex"> the index of the wall being checked </param>
    /// <param name="doorLoc"> the location of the door with 0 representing the centre of the wall </param>
    private void AdjustForDoor(GameObject input, int wallIndex, float doorLoc) {
        AdjustWall (
            input, wallIndex, doorLoc, 0, 2,
            "The door at {" + doorLoc + "} is close to or outside the end of the wall."
        );
    }
    /// <summary>
    /// Cuts all doors and windows in a wall after adding a new window.
    /// </summary>
    /// <param name="input"> the wall object being changed </param>
    /// <param name="wallIndex"> the index of the wall being changed </param>
    /// <param name="windowLoc"> the location of the window with 0 representing the centre of the wall </param>
    private void AdjustForWindow(GameObject input, int wallIndex, float windowLoc) {
        AdjustWall (
            input, wallIndex, windowLoc, 1.5f, 3,
            "The window at {" + windowLoc + "} is close to or outside the end of the wall."
        );
    }
    /// <summary>
    /// Cuts all doors and windows into a given wall object.
    /// </summary>
    /// <param name="input"> the wall object being changed </param>
    /// <param name="wallIndex"> the index of the wall being changed </param>
    /// <param name="loc"> the location of the new door or window with 0 representing the centre of the wall </param>
    /// <param name="height"> the distance between the floor and the bottom of the new door or window </param>
    /// <param name="width"> the width of the door or window </param>
    /// <param name="errMsg"> the message thrown if the door or window is not within the wall </param>
    private void AdjustWall(GameObject input, int wallIndex, float loc, float height, float width, string errMsg) {
        float wallLength = GetWallSize (wallIndex);
        float limit = wallLength / 2 - (width / 2 + 0.5f);
        float margin = 0.0001f;

        if (loc >= -limit-margin && loc <= limit+margin) {
            Vector3 cutCentre = new Vector3 (loc, height, 0);
            doorsAndWindows [wallIndex].Add (cutCentre);
            doorsAndWindows [wallIndex].Sort ((a, b) => a.x.CompareTo (b.x));
            if (WallCutter.cutDoorsAndWindows (input, doorsAndWindows [wallIndex].ToArray (), wallLength))
                doorsAndWindows [wallIndex].Remove (cutCentre);
            else
                RemovePlus (wallIndex, loc);
        }
        else
            Debug.LogError (errMsg);
    }
    /// <summary>
    /// Cuts all doors and windows into a given wall object.
    /// </summary>
    /// <param name="input"> the wall object being changed </param>
    /// <param name="wallIndex"> the index of the wall being changed </param>
    private void AdjustWall(GameObject input, int wallIndex) {
        float wallLength = GetWallSize (wallIndex);
        WallCutter.cutDoorsAndWindows (input, doorsAndWindows [wallIndex].ToArray (), wallLength);
    }
    /// <summary>
    /// Removes a plus sign from the save data and from the scene.
    /// </summary>
    /// <param name="wallIndex"> the index of the wall holding the plus sign </param>
    /// <param name="loc"> the location of the plus sign along the wall </param>
    public void RemovePlus(int wallIndex, float loc) {
        float angle =
            (wallIndex == 1) ? 90 :
            (wallIndex == 2) ? 180 :
            (wallIndex == 3) ? 270 : 0;
        float wall;
        string helper;
        float[] centre;
        float[] roomCentre = thisRoom.GetCentre ();
        bool collided;

        List<PlusData> plusSigns = thisRoom.GetPlusData ();
        Vector3 plusCentre;

        FileStream file = File.Create (fileLoc + "plusDeletion.txt");
        file.Close();

        for (int k = plusSigns.Count-1; k >= 0; k--) {
            if (plusSigns [k].GetAngle () == angle) {
                StreamWriter writer = new StreamWriter(fileLoc + "plusDeletion.txt", true);
                helper = "";
                centre = plusSigns [k].GetCentre ();
                centre [0] -= roomCentre [0];
                centre [1] -= roomCentre [1];
                centre [2] -= roomCentre [2];

                plusCentre = new Vector3 (centre[0], centre[1], centre[2]);
                wall = plusSigns [k].GetWallIndex ();
                collided = (wall == 1 || wall == 3) ?
                    plusCentre.z > loc - 2f && plusCentre.z < loc + 2f :
                    plusCentre.x > loc - 2f && plusCentre.x < loc + 2f;

                //plusCentre = Quaternion.Euler (0, -angle, 0) * new Vector3 (centre [0], centre [1], centre [2]);

                if (collided) {
                    helper += "------------------------------------------------------------------------------------------------\n";
                    centre = plusSigns[k].GetCentre();
                    helper += "RemovePlus called on Room at: <" + roomCentre[0] + ", " + roomCentre[1] + ", " + roomCentre[2] + ">\n";
                    helper += "    Plus centre relative: <" + plusCentre.x + ", " + plusCentre.y + ", " + plusCentre.z + ">\n";
                    helper += "    Plus centre world: <" + centre [0] + ", " + centre [1] + ", " + centre [2] + ">\n";

                    wall = 0;
                    foreach (Canvas plus in this.GetComponentsInChildren<Canvas>()) {
                        if (plus.tag == "PlusSign") {
                            int comp = plus.GetComponent<SubMenuHandler> ().GetData ().CompareTo (plusSigns [k]);
                            wall++;
                            if (comp == 0) {
                                float[] cp = plus.GetComponent<SubMenuHandler> ().GetData ().GetCentre ();
                                helper += "    Call Destroy on plus at: <" + cp [0] + ", " + cp [1] + ", " + cp [2] + ">\n";
                                Destroy (plus.gameObject);
                                break;
                            }
                        }
                    }
                    helper += "    Number of PlusSigns checked: " + wall + "\n";

                    helper += "    Call DeletePlus on index: " + k + "\n";
                    helper += "------------------------------------------------------------------------------------------------";
                    thisRoom.DeletePlus (plusSigns[k]);
                    break;
                }
                writer.WriteLine (helper);
                writer.Close ();
            }
        }
    }
    /// <summary>
    /// Removes a door or window from the save data and from the scene.
    /// </summary>
    /// <param name="wallIndex"> the index of the wall holding the door or window </param>
    /// <param name="loc"> the location of the door or window along the wall </param>
    public void RemoveDoorOrWindow(int wallIndex, float loc) {
        GameObject component;
        if ((component = GetWallObject (wallIndex)) != null) {
            float diffX, margin = 0.0001f;
            Vector3 chk;

            for (int k = doorsAndWindows [wallIndex].Count-1; k >= 0; k--) {
                chk = doorsAndWindows [wallIndex] [k];
                diffX = Math.Abs (loc - chk [0]);
                if (diffX < margin) {
                    doorsAndWindows [wallIndex].Remove (chk);
                    break;
                }
            }

            AdjustWall (component, wallIndex);
        } else
            Debug.LogError ("A wall with index of {" + wallIndex + "} doesn't exist.");
    }
    /// <summary>
    /// Removes a picture from the save data.
    /// </summary>
    /// <param name="location"> the location of the picture to be removed </param>
    public void RemovePicture (Vector3 location) {
        thisRoom.DeletePicture (location);
    }
}