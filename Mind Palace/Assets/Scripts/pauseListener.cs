﻿using System.Collections;
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

public class pauseListener : MonoBehaviour {
   
    public GameObject pauseMenu; //pause canvas
    public GameObject settingsMenu; //settings canvas
    public GameObject objectsMenu;
    public Transform cameraRigTransform; //camera rig

    private SteamVR_TrackedController controller;
    private Vector3 viewDir;
    private float height = 1.5f;
    private float distance = 5.0f;
    private bool paused = false;

    //adds listener to menu button
    void Start()
    {
        controller = GetComponent<SteamVR_TrackedController>();
        controller.MenuButtonClicked += MenuPress;

    }

    //reveals or hides menu and move it to infront of the user, plus rotates it to face the user
    private void MenuPress(object sender, ClickedEventArgs e)
    {
        paused = (pauseMenu.transform.position.y>-50 || settingsMenu.transform.position.y > -50 || objectsMenu.transform.position.y > -50);
        print(paused);
        paused = !paused;
        if (paused)
        {
            objectsMenu.transform.position = new Vector3(cameraRigTransform.position.x, 1, cameraRigTransform.position.z);
        }
        else
        {
            objectsMenu.transform.position = new Vector3(0,-100,0);
        }
    }
	
    //Moves and rotates the settings canvas
    public void GoToSettings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
        settingsMenu.transform.position = pauseMenu.transform.position;
        settingsMenu.transform.rotation = pauseMenu.transform.rotation;
    }

    public void LeaveSettings()
    {
        pauseMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void ResumeGame()
    {
        paused = false;
        pauseMenu.SetActive(false);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void toStart()
    {
        height = cameraRigTransform.position.y;
        cameraRigTransform.position = new Vector3(0, height, 0);
        MenuPress(new object(),new ClickedEventArgs());
    }
}
