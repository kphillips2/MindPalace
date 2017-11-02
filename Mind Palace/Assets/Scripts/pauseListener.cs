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

public class pauseListener : MonoBehaviour {
   
    public GameObject pauseMenu; //pause canvas
    public GameObject settingsMenu; //settings canvas
    public VRTK_Pointer pointer; //TBD
    public VRTK_StraightPointerRenderer poin; //TBD
    public Transform cameraRigTransform; //camera rig

    private Vector3 viewDir;
    private float height = 1.5f;
    private float distance = 5.0f;
    private SteamVR_TrackedController controller;
    private bool paused = false;

    //adds listener to menu button
    void OnEnable () {
        controller = GetComponent<SteamVR_TrackedController>();
        controller.MenuButtonClicked += MenuPress;
    }

    //reveals or hides menu and move it to infront of the user, plus rotates it to face the user
    private void MenuPress(object sender, ClickedEventArgs e)
    {
        paused = pauseMenu.activeSelf;
        paused = !paused;
        pauseMenu.SetActive(paused);
        settingsMenu.SetActive(false);
        viewDir = SteamVR_Render.Top().GetRay().direction;
        pauseMenu.transform.position = cameraRigTransform.position + distance*new Vector3(viewDir.normalized.x, height/distance,viewDir.normalized.z);
        pauseMenu.transform.rotation = Quaternion.LookRotation(new Vector3(viewDir.x,0,viewDir.z));
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
}
