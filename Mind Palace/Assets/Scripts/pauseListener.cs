using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using UnityEngine.SceneManagement;

/*
 * Name: Max Baker
 * Date: 11/1/17
 * Add this to a controller, give it the menu canvases
 * Assign the methods to the corresponding menu button
 * */

public class pauseListener : MonoBehaviour
{

    public GameObject pauseMenu; //pause canvas
    public GameObject settingsMenu; //settings canvas
    public GameObject objectsMenu;
    public Transform cameraRigTransform; //camera rig
    public GameObject level;

    private SteamVR_TrackedController controller;
    private Vector3 viewDir;
    private float height = 1.5f;
    private float distance = 5.0f;
    private bool paused = false;
    private Building building;

    //adds listener to menu button
    void Start()
    {
        controller = GetComponent<SteamVR_TrackedController>();
        controller.MenuButtonClicked += MenuPress;
    }

    //reveals or hides menu and move it to infront of the user, plus rotates it to face the user
    private void MenuPress(object sender, ClickedEventArgs e)
    {
        paused = (pauseMenu.transform.position.y > -50 || settingsMenu.transform.position.y > -50 || objectsMenu.transform.position.y > -50);
        paused = !paused;
        if (paused)
        {
            viewDir = SteamVR_Render.Top().GetRay().direction;
            viewDir = new Vector3(viewDir.x, 0, viewDir.z);
            viewDir = viewDir.normalized;
            pauseMenu.transform.position = new Vector3(cameraRigTransform.position.x + (viewDir.x * 3), 2, cameraRigTransform.position.z + (viewDir.z * 3));
            pauseMenu.transform.rotation = Quaternion.LookRotation(new Vector3(viewDir.x, 0, viewDir.z));
        }
        else
        {
            pauseMenu.transform.position = new Vector3(0, -100, 0);
            settingsMenu.transform.position = new Vector3(0, -100, 0);
            objectsMenu.transform.position = new Vector3(0, -100, 0);
        }
    }

    //Moves and rotates the settings canvas
    public void GoToSettings()
    {
        settingsMenu.transform.position = pauseMenu.transform.position;
        settingsMenu.transform.rotation = pauseMenu.transform.rotation;
        pauseMenu.transform.position = new Vector3(0, -100, 0);
    }

    public void LeaveSettings()
    {
        pauseMenu.transform.position = settingsMenu.transform.position;
        pauseMenu.transform.rotation = settingsMenu.transform.rotation;
        settingsMenu.transform.position = new Vector3(0, -100, 0);
    }

    public void ResumeGame()
    {
        paused = false;
        pauseMenu.transform.position = new Vector3(0, -100, 0);
    }

    public void ToMainMenu()
    {
        building = level.GetComponent<Building>();
        building.Save();
        SaveFile.Save();
        SceneManager.LoadScene("MinimalMenu");
    }

    public void ViewObjectMenu()
    {
        objectsMenu.transform.position = pauseMenu.transform.position;
        objectsMenu.transform.rotation = pauseMenu.transform.rotation;
        pauseMenu.transform.position = new Vector3(0, -100, 0);
    }

    public void toStart()
    {
        height = cameraRigTransform.position.y;
        cameraRigTransform.position = new Vector3(0, height, 0);
        MenuPress(new object(), new ClickedEventArgs());
    }
}