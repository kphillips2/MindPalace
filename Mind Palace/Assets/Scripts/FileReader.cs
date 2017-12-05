using UnityEngine;
using UnityEditor;
using System.IO;

public class FileReader : MonoBehaviour
{
    //Prefabs
    public Transform arch;
    public Transform monkey;

    //At the start of the program, generates objects based on the contents of a text file
    //File is formatted:
    //  OBJECT_NAME
    //  X-POSITION Y-POSITION Z-POSITION
    //  X-ROTATION Y-ROTATION Z-ROTATION
    //  X-SCALE Y-SCALE Z-SCALE
    //  R-COLOR G-COLOR B-COLOR

    //The last line (color line) is only present for primitives
    
    //Objects that can be generated:
    //Primitives - cube, sphere
    //Prefabs - arch, monkey

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
            if (line1 == "cube" || line1 == "sphere") //Gets color values if primitive
            {
                string line5 = reader.ReadLine();
                makeObject(line1, line2, line3, line4, line5);
            }
            else createPrefab(line1, line2, line3, line4);
        }

        reader.Close();
    }

    //Creates a prefab given the name of the prefab and strings containing the position, 
    //rotation, and scale values to use when placing it in the world.
    //Can currently create the arch and monkey prefabs.
    public void createPrefab(string obj, string position, string rotation, string scale)
    {
        Transform prefab;
        switch (obj)
        {
            case "arch":
                prefab = arch;
                break;
            case "monkey":
                prefab = monkey;
                break;
            default:
                Debug.Log("Invalid Shape");
                return;
        }

        //Instanstiate the selected prefab at the given position coordinates
        var pos = position.Split(' ');
        Vector3 posVec = new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
        Transform newObj = Instantiate(prefab, posVec, Quaternion.identity);

        //Get rotation and scale values and apply them to prefab
        var r = rotation.Split(' ');
        newObj.Rotate(float.Parse(r[0]), float.Parse(r[1]), float.Parse(r[2]));
        var s = scale.Split(' ');
        newObj.localScale = new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
    }

    //Creates a primitive object given the name of the primitive and strings containing the position, 
    //rotation, scale, and color values to use when placing it in the world.
    //Can currently create the cube and sphere primitives.
    public void makeObject(string obj, string position, string rotation, string scale, string color)
    {
        //Create the selected primitive
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

        //Get and apply position, rotation, scale, and color values
        var pos = position.Split(' ');
        gameObj.transform.position = new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
        var r = rotation.Split(' ');
        gameObj.transform.Rotate(float.Parse(r[0]), float.Parse(r[1]), float.Parse(r[2]));
        var s = scale.Split(' ');
        gameObj.transform.localScale = new Vector3(float.Parse(s[0]), float.Parse(s[1]), float.Parse(s[2]));
        var c = color.Split(' ');
        gameObj.GetComponent<Renderer>().material.color = new Color(float.Parse(c[0]), float.Parse(c[1]), float.Parse(c[2]), 1);
    }

}