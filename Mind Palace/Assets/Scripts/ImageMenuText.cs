using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ImageMenuText : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        // *** CHANGE THIS TO FOLDER THAT YOUR IMAGES ARE STORED IN ***
        string filePath = "C:/Users/Katie/Documents/TestImages"; 

        string[] files = Directory.GetFiles(filePath);
        string[] directories = Directory.GetDirectories(filePath);
        int dir = 0;
        int img = 0;

        // For each of the components that are under the ImageMenu object in the scene
        foreach (Transform child in transform)
        {
            // For the buttons, list directories (folders) first
            if(child.name == "ImageButton" && dir < directories.Length)
            {
                child.GetComponentInChildren<Text>().text = directories[dir].Substring(filePath.Length + 1);
                dir++;
            }

            // If all directories are already listed, start listing any image files that are there
            else if(child.name == "ImageButton")
            {
                while (img < files.Length)
                {
                    string extension = Path.GetExtension(files[img]);
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg") // Only list jpg files
                    {
                        // Set button text to file name
                        child.GetComponentInChildren<Text>().text = files[img].Substring(filePath.Length + 1);

                        // Testing copying files to the Assets folder (this line should be moved elsewhere later on)
                        //File.Copy(files[img], "Assets/SaveFile/TestLoci/" + img + extension);
                        img++;
                        break;
                    }
                    img++;
                }
            }

            // Testing adding an image to the image display on the side
            else if(child.name == "ImageDisplay" && files.Length > 1)
            {
                child.GetComponent<Image>().overrideSprite = PictureCreator.LoadNewSprite(files[1]);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
