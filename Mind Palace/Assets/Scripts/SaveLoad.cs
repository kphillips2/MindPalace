using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {
    public static List<Loci> savedLocis = new List<Loci>();

    // Save current Loci to list of saved Loci
    public static void save() {
        saveObjects();
        savedLocis.Add(Loci.current);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create("Assets/SaveFile/saveFile.gd");
        bf.Serialize(file, SaveLoad.savedLocis);
        file.Close();
    }

    // Load list of saved Loci
    public static void load() {
        if (File.Exists("Assets/SaveFile/saveFile.gd")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open("Assets/SaveFile/saveFile.gd", FileMode.Open);
            SaveLoad.savedLocis = (List<Loci>)bf.Deserialize(file);
            file.Close();
        }
    }

    // Saves all of the prefab objects in the scene to the current Loci
    private static void saveObjects() {
        //Reset the list of objects for the Loci
        Loci.current.objects = new List<float[]>();

        //Retrieves all of the GameObjects in the scene
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            float val;
            switch (go.ToString()) //Name of GameObject, only want to save ones with certain names
            {
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
            
            // Save the object using its corresponding prefab value, x-coordinate, z-coordinate, and y-rotation
            float[] f = { val, go.transform.position.x, go.transform.position.z, go.transform.eulerAngles.y };
            Loci.current.objects.Add(f); //Add to list of objects in current save
        }
    }
}
