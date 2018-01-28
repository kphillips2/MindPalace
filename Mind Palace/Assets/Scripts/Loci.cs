using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Loci {
    public static Loci current;

    public string name;
    public List<float[]> objects;

    public Loci (string name) {
        this.name = name;
        objects = new List<float[]>();
    }
}
