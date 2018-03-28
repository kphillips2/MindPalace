using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

public static class SaveFile {
    public static string name = null;
    public static Loci currentLoci= new Loci (); //The current save file being loaded/edited/viewed
    public static Dictionary<string, Loci> savedLocis = new Dictionary<string, Loci> (); //List of save files

    /// <summary>
    /// Adds the current Loci to the list of saved Locis and saves the list to a file.
    /// All new information about the Loci (except object info) has been added to the currentLoci attribute.
    /// </summary>
    public static void Save(){
        if (name == null)
            name = DateTime.Now.ToLongDateString () + ", " + DateTime.Now.ToLongTimeString ();
        SaveObjects ();
        savedLocis.Add (name, currentLoci);
        BinaryFormatter bf = new BinaryFormatter ();
        FileStream file = File.Create ("Assets/SaveFile/saveFile.gd");
        bf.Serialize (file, savedLocis);
        file.Close ();
    }

    /// <summary>
    /// Saves all of the prefab objects in the scene to the currently open Loci.
    /// </summary>
    private static void SaveObjects(){
        // Reset the list of objects for the Loci
        currentLoci.clearObjects ();

        // Retrieves all of the GameObjects in the scene
        foreach (GameObject go in Resources.FindObjectsOfTypeAll (typeof(GameObject)))
        {
            float val;
            // Name of GameObject, only want to save ones with certain names
            switch (go.ToString ()) {
                case "Bed(Clone) (UnityEngine.GameObject)":
                    val = 0f;
                    break;
                case "BookCase(Clone) (UnityEngine.GameObject)":
                    val = 1f;
                    break;
                case "Chair(Clone) (UnityEngine.GameObject)":
                    val = 2f;
                    break;
                case "Chair2(Clone) (UnityEngine.GameObject)":
                    val = 3f;
                    break;
                case "CoffeeTable(Clone) (UnityEngine.GameObject)":
                    val = 4f;
                    break;
                case "Couch(Clone) (UnityEngine.GameObject)":
                    val = 5f;
                    break;
                case "Counter(Clone) (UnityEngine.GameObject)":
                    val = 6f;
                    break;
                case "Fireplace(Clone) (UnityEngine.GameObject)":
                    val = 7f;
                    break;
                case "FlowerTable(Clone) (UnityEngine.GameObject)":
                    val = 8f;
                    break;
                case "Fridge(Clone) (UnityEngine.GameObject)":
                    val = 9f;
                    break;
                case "Guitar(Clone) (UnityEngine.GameObject)":
                    val = 10f;
                    break;
                case "Headboard(Clone) (UnityEngine.GameObject)":
                    val = 11f;
                    break;
                case "LionStatue(Clone) (UnityEngine.GameObject)":
                    val = 12f;
                    break;
                case "Oven(Clone) (UnityEngine.GameObject)":
                    val = 13f;
                    break;
                case "Plant(Clone) (UnityEngine.GameObject)":
                    val = 14f;
                    break;
                case "Sink(Clone) (UnityEngine.GameObject)":
                    val = 15f;
                    break;
                case "SoccerBall(Clone) (UnityEngine.GameObject)":
                    val = 16f;
                    break;
                case "Table(Clone) (UnityEngine.GameObject)":
                    val = 17f;
                    break;
                case "Toilet(Clone) (UnityEngine.GameObject)":
                    val = 18f;
                    break;
                case "TV(Clone) (UnityEngine.GameObject)":
                    val = 19f;
                    break;
                default:
                    continue; //Don't save object if it doesn't match any of the above names
            }
            
            // SaveFile the object using its corresponding prefab value, x-coordinate, z-coordinate, and y-rotation
            float[] f = new float[] {
                val, go.transform.position.x, go.transform.position.y, go.transform.position.z,
                go.transform.eulerAngles.x, go.transform.eulerAngles.y, go.transform.eulerAngles.z
            };
            currentLoci.addObject (f); //Add to list of objects in current save
        }
    }
}
