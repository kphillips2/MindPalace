using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class LoadFile {
    /// <summary>
    /// Fills the savedLocis dictionary with the Loci objects that were saved in the file.
    /// </summary>
    public static void Load(){
        if (File.Exists ("Assets/SaveFile/saveFile.gd")) {
            BinaryFormatter bf = new BinaryFormatter ();
            FileStream file = File.Open ("Assets/SaveFile/saveFile.gd", FileMode.Open);
            SaveFile.savedLocis = (Dictionary<string, Loci>)bf.Deserialize (file);
            file.Close ();
        } else
            Debug.LogError("The file was not found");
    }
    /// <summary>
    /// Sets the currently open Loci to the Loci with the given name.
    /// </summary>
    /// <param name="name"> the name of the Loci to be opened </param>
    public static void LoadLoci(string name){
        if (SaveFile.savedLocis.TryGetValue (name, out SaveFile.currentLoci)) {
            SaveFile.name = name;
            MonoBehaviour.print ("File and Loci successfully loaded.");
        } else
            Debug.LogError ("The file was found but the given Loci was not.");
    }
}
