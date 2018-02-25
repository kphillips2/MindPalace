using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ImageMenu : MonoBehaviour {

	// Use this for initialization
	void Start ()
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
            if (child.name == "ImageButton" && dir < directories.Length)
            {
                //child.GetComponent<Image>().overrideSprite = PictureCreator.LoadNewSprite("Assets/Resources/folder.jpg");
                dir++;
            }

            // If all directories are already listed, start listing any image files that are there
            else if (child.name == "ImageButton")
            {
                while (img < files.Length)
                {
                    string extension = Path.GetExtension(files[img]);
                    if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png") // Only list image files
                    {
                        // Set button sprite to image
                        child.GetComponent<Image>().overrideSprite = PictureCreator.LoadNewSprite(files[img]);
                        img++;
                        break;
                    }
                    img++;
                }
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
