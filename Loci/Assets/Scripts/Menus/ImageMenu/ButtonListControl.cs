using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ButtonListControl : MonoBehaviour {
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private Button backButton;
    private List<GameObject> buttons = new List<GameObject>();
    private string startFilePath = "Assets/TestImages"; //Top level where pictures are stored
    private string currentFilePath = "Assets/TestImages"; //Current folder being looked at

    public GameObject ImageMenu;
    public GameObject ButtonManager;
    public GameObject currentRoom; //Room that menu is being displayed in

    // Use this for initialization
    void Start ()
    {
        backButton.GetComponentInChildren<Text>().text = "Cancel"; //Currently at top level
        generateButtons();
    }

    // Generate the buttons in the list
    public void generateButtons()
    {
        // If already buttons in list, remove them
        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }

        string[] files = Directory.GetFiles(currentFilePath);
        string[] directories = Directory.GetDirectories(currentFilePath);

        //Create a button for each directory in current path
        foreach(string d in directories)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            button.GetComponent<ButtonListButton>().setText(d.Substring(currentFilePath.Length + 1));
            button.GetComponent<ButtonListButton>().setIsFile(false);
            button.transform.SetParent(buttonTemplate.transform.parent, false);
            buttons.Add(button);
        }
        
        //Create a button for each jpg/png file in current path
        foreach (string f in files)
        {
            string extension = Path.GetExtension(f);
            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg" || extension.ToLower() == ".png") // Only list jpg/png files
            {
                GameObject button = Instantiate(buttonTemplate) as GameObject;
                button.SetActive(true);
                button.GetComponent<ButtonListButton>().setText(f.Substring(currentFilePath.Length + 1)); // Chop off file path and last '/'
                button.GetComponent<ButtonListButton>().setIsFile(true);
                button.transform.SetParent(buttonTemplate.transform.parent, false);
                buttons.Add(button);
            }
        }
    }
	
    //Defines action to be taken when a button in the list is clicked. Uses the textString from the button
    //and whether the button corresponds to a folder or file
	public void listButtonClicked(string textString, bool isFile)
    {
        // If image file, hang picture on the wall where the menu is
        if (isFile)
        {
            PictureCreator pc = new PictureCreator();
            string picFilePath = currentFilePath + "/" + textString;
            float roty = transform.eulerAngles.y + 90f;
            Vector3 location = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            List<GameObject> image = pc.placePicture(picFilePath, roty, location);
            ButtonManager.GetComponent<ActivationManager>().GetActive().GetComponent<SubMenuHandler>().PlaceXOverImage(image);
            ButtonManager.GetComponent<ActivationManager>().GetActive().GetComponent<SubMenuHandler>().HideAll();
            ButtonManager.GetComponent<ActivationManager>().NoActive();
            ImageMenu.transform.position= new Vector3(0,-100,0); //Like Deactivating, but deactivating breaks it

            Picture p = new Picture(picFilePath, roty, location);
            currentRoom.GetComponent<RoomHandler>().AddPicture(p);
        }
        // If directory, navigate to that directory
        else
        {
            currentFilePath = currentFilePath + "/" + textString;
            backButton.GetComponentInChildren<Text>().text = "Previous Folder";
            generateButtons();
        }
    }

    //Defines action when user wants to go back one folder
    public void backButtonClicked()
    {
        // If at top level, return to plus-sign if back button clicked
        if (currentFilePath == startFilePath)
        {
            ImageMenu.transform.position = new Vector3(0, -100, 0);
            ButtonManager.GetComponent<ActivationManager>().NoActive();
        }
        else
        {
            //Remove last slash and everything after it from file path
            int index = currentFilePath.LastIndexOf("/");
            if (index > 0) currentFilePath = currentFilePath.Substring(0, index);

            //Change text to "Back" if reach top level
            if (currentFilePath == startFilePath) backButton.GetComponentInChildren<Text>().text = "Cancel";
            generateButtons();
        }
    }
}
