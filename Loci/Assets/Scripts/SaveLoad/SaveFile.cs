using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

/// <summary>
/// Responsible for storing all saved information in a file.
/// </summary>
public static class SaveFile {
    public static bool isNewLoci = false;
    public static string name = null;
    public static bool EditMode;
    public static Loci currentLoci= new Loci (); //The current save file being loaded/edited/viewed
    public static Dictionary<string, Loci> savedLocis= new Dictionary<string, Loci> (); //List of save files
    public static string fileSaveLocation = "Assets/Resources/SaveFile/";

    /// <summary>
    /// Adds the current Loci to the list of saved Locis and saves the list to a file.
    /// All new information about the Loci (except object info) has been added to the currentLoci attribute.
    /// </summary>
    public static void Save() {
        SaveObjects ();
        if (name == null) {
            name = DateTime.Now.ToLongDateString() + ", " + DateTime.Now.ToLongTimeString();
            savedLocis.Add(name, currentLoci);
        }
        else if (isNewLoci) {
            savedLocis.Add(name, currentLoci);
        } else
            savedLocis[name] = currentLoci;

        BinaryFormatter bf = new BinaryFormatter ();
        if(!Directory.Exists(fileSaveLocation)) System.IO.Directory.CreateDirectory(fileSaveLocation);
        FileStream file = File.Create (fileSaveLocation + "saveFile.gd");
        bf.Serialize (file, savedLocis);
        file.Close ();
    }

    /// <summary>
    /// Saves all of the prefab objects in the scene to the currently open Loci.
    /// </summary>
    private static void SaveObjects() {
        // Reset the list of objects for the Loci
        currentLoci.clearObjects ();

        // Retrieves all of the GameObjects in the scene
        foreach (GameObject go in Resources.FindObjectsOfTypeAll (typeof(GameObject)))
        {
            float val;
            // Name of GameObject, only want to save ones with certain names
            switch (go.ToString ()) {
                case "Bed_G(Clone) (UnityEngine.GameObject)":
                    val = 0f;
                    break;
                case "BookCase_G(Clone) (UnityEngine.GameObject)":
                    val = 1f;
                    break;
                case "Chair_G(Clone) (UnityEngine.GameObject)":
                    val = 2f;
                    break;
                case "Chair2_G(Clone) (UnityEngine.GameObject)":
                    val = 3f;
                    break;
                case "CoffeeTable_G(Clone) (UnityEngine.GameObject)":
                    val = 4f;
                    break;
                case "Couch_G(Clone) (UnityEngine.GameObject)":
                    val = 5f;
                    break;
                case "Counter_G(Clone) (UnityEngine.GameObject)":
                    val = 6f;
                    break;
                case "Fireplace_G(Clone) (UnityEngine.GameObject)":
                    val = 7f;
                    break;
                case "FlowerTable_G(Clone) (UnityEngine.GameObject)":
                    val = 8f;
                    break;
                case "Fridge_G(Clone) (UnityEngine.GameObject)":
                    val = 9f;
                    break;
                case "Guitar_G(Clone) (UnityEngine.GameObject)":
                    val = 10f;
                    break;
                case "Headboard_G(Clone) (UnityEngine.GameObject)":
                    val = 11f;
                    break;
                case "LionStatue_G(Clone) (UnityEngine.GameObject)":
                    val = 12f;
                    break;
                case "Oven_G(Clone) (UnityEngine.GameObject)":
                    val = 13f;
                    break;
                case "Plant_G(Clone) (UnityEngine.GameObject)":
                    val = 14f;
                    break;
                case "Sink_G(Clone) (UnityEngine.GameObject)":
                    val = 15f;
                    break;
                case "SoccerBall_G(Clone) (UnityEngine.GameObject)":
                    val = 16f;
                    break;
                case "Table_G(Clone) (UnityEngine.GameObject)":
                    val = 17f;
                    break;
                case "Toilet_G(Clone) (UnityEngine.GameObject)":
                    val = 18f;
                    break;
                case "TV_G(Clone) (UnityEngine.GameObject)":
                    val = 19f;
                    break;
                default:
                    continue; //Don't save object if it doesn't match any of the above names
            }
            
            // Save the object using its corresponding prefab value, position coordinates (xyz), then rotation (xyz)
            float[] f = new float[] {
                val, go.transform.position.x, go.transform.position.y, go.transform.position.z,
                go.transform.eulerAngles.x, go.transform.eulerAngles.y, go.transform.eulerAngles.z
            };
            currentLoci.addObject (f); //Add to list of objects in current save
        }
    }
}
