using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class LoadFile {

    // Fills the savedLocis list with the Loci objects that were saved in the save file
    public static void load()
    {
        if (File.Exists("Assets/SaveFile/saveFile.gd"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open("Assets/SaveFile/saveFile.gd", FileMode.Open);
            SaveFile.savedLocis = (List<Loci>)bf.Deserialize(file);
            file.Close();
        }
    }
}
