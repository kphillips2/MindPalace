using UnityEngine;
using UnityEditor;
using System.IO;

public class FileReader : MonoBehaviour
{
    private void Awake()
    {
        string path = "Assets/Resources/test.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
       
        string line1;
        while ((line1 = reader.ReadLine()) != null)
        {
            string line2 = reader.ReadLine();
            string line3 = reader.ReadLine();
            string line4 = reader.ReadLine();
            makeObject(line1, line2, line3, line4);
        }

        reader.Close();
    }

    public void makeObject(string obj, string position, string scale, string color)
    {
        GameObject gameObj;
        switch (obj)
        {
            case "cube": 
                gameObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                break;
            case "sphere":
                gameObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                break;
            default:
                Debug.Log("Invalid Shape");
                return;
        }

        Destroy(gameObj.GetComponent<Rigidbody>());
        var pos = position.Split(' ');
        gameObj.transform.position = new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
        var s = scale.Split(' ');
        gameObj.transform.localScale = new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
        var c = color.Split(' ');
        gameObj.GetComponent<Renderer>().material.color = new Color(float.Parse(c[0]), float.Parse(c[1]), float.Parse(c[2]), 1);
    }

}