﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Responsible for Loading all stored information in a file.
/// </summary>
public static class LoadFile {
    /// <summary>
    /// Fills the savedLocis dictionary with the Loci objects that were saved in the file.
    /// </summary>
    public static void Load(){
        if (File.Exists (SaveFile.fileSaveLocation + "saveFile.gd")) {
            BinaryFormatter bf = new BinaryFormatter ();
            FileStream file = File.Open (SaveFile.fileSaveLocation + "saveFile.gd", FileMode.Open);
            SaveFile.savedLocis = (Dictionary<string, Loci>)bf.Deserialize (file);
            Debug.Log(SaveFile.savedLocis.Count);
            file.Close ();
            MonoBehaviour.print ("File successfully loaded.");
        } else
            Debug.LogError ("The file was not found");
    }
    /// <summary>
    /// Sets the currently open Loci to the Loci with the given name.
    /// </summary>
    /// <param name="name"> the name of the Loci to be opened </param>
    public static void LoadLoci(string name, bool editMode){
        if (SaveFile.savedLocis.TryGetValue (name, out SaveFile.currentLoci)) {
            SaveFile.name = name;
            SaveFile.EditMode = editMode;
            MonoBehaviour.print ("Loci successfully loaded.");
        } else
            Debug.LogError ("The Loci was not found.");
    }

    public static void ClearLoci()
    {
        SaveFile.currentLoci = new Loci();
        SaveFile.name = null;
    }
}