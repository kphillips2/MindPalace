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
    public GameObject ViewModePause;
    public GameObject EditModePause;
    public GameObject keyboard; //settings canvas
    public GameObject objectsMenu;
    public Transform cameraRigTransform; //camera rig
    public GameObject level;

    public GameObject ActivePauseMenu; //pause canvas
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
        if (SaveFile.EditMode)
        {
            ActivePauseMenu = EditModePause;
        }
        else
        {
            ActivePauseMenu = ViewModePause;
        }
    }

    //reveals or hides menu and move it to infront of the user, plus rotates it to face the user
    private void MenuPress(object sender, ClickedEventArgs e)
    {
        if (ActivationManager.objectHeld1 == null && ActivationManager.objectHeld2 == null)
        {
            paused = (ActivePauseMenu.transform.position.y > -50 || keyboard.transform.position.y > -50 || objectsMenu.transform.position.y > -50);
            paused = !paused;
            if (paused)
            {
                viewDir = SteamVR_Render.Top().GetRay().direction;
                viewDir = new Vector3(viewDir.x, 0, viewDir.z);
                viewDir = viewDir.normalized;
                ActivePauseMenu.transform.position = new Vector3(cameraRigTransform.position.x + (viewDir.x * 3), 2, cameraRigTransform.position.z + (viewDir.z * 3));
                ActivePauseMenu.transform.rotation = Quaternion.LookRotation(new Vector3(viewDir.x, 0, viewDir.z));
            }
            else
            {
                ActivePauseMenu.transform.position = new Vector3(0, -100, 0);
                keyboard.transform.position = new Vector3(0, -100, 0);
                objectsMenu.transform.position = new Vector3(0, -100, 0);
            }
        }
        else
        {
            //Delete objects
            if(ActivationManager.objectHeld1 != null) Destroy(ActivationManager.objectHeld1);
            if(ActivationManager.objectHeld2 != null) Destroy(ActivationManager.objectHeld2);

            //Tell controllers that an object is no longer being held
            GameObject controllerGameObj = VRTK_DeviceFinder.GetControllerLeftHand();
            GameObject controllerGameObj2 = VRTK_DeviceFinder.GetControllerRightHand();
            VRTK_InteractGrab myGrab = controllerGameObj.GetComponent<VRTK_InteractGrab>();
            VRTK_InteractGrab myGrab2 = controllerGameObj2.GetComponent<VRTK_InteractGrab>();
            myGrab.ForceRelease();
            myGrab2.ForceRelease();

            ActivationManager.objectHeld1 = null;
            ActivationManager.objectHeld2 = null;
        }
    }

    //Moves and rotates the settings canvas
    public void GoToKeyboard()
    {
        keyboard.transform.position = ActivePauseMenu.transform.position;
        keyboard.transform.rotation = ActivePauseMenu.transform.rotation;
        ActivePauseMenu.transform.position = new Vector3(0, -100, 0);
    }

    public void LeaveSettings()
    {
        ActivePauseMenu.transform.position = keyboard.transform.position;
        ActivePauseMenu.transform.rotation = keyboard.transform.rotation;
        keyboard.transform.position = new Vector3(0, -100, 0);
    }

    public void ResumeGame()
    {
        paused = false;
        ActivePauseMenu.transform.position = new Vector3(0, -100, 0);
    }

    public void ToMainMenu()
    {
        building = level.GetComponent<Building>();
        building.Save();
        SaveFile.Save();
        SceneManager.LoadScene(0);
    }

    public void ViewObjectMenu()
    {
        objectsMenu.transform.position = ActivePauseMenu.transform.position;
        objectsMenu.transform.rotation = ActivePauseMenu.transform.rotation;
        ActivePauseMenu.transform.position = new Vector3(0, -100, 0);
    }

    public void toStart()
    {
        height = cameraRigTransform.position.y;
        cameraRigTransform.position = new Vector3(0, height, 0);
        MenuPress(new object(), new ClickedEventArgs());
    }
}