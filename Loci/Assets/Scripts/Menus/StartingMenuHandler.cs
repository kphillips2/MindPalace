using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingMenuHandler : MonoBehaviour {

    public GameObject LociMenu;
    public GameObject StartingMenu;
    public GameObject SettingsMenu;
    public GameObject Keyboard;

	void Start () { NewLoci(); }

    public void NewLoci()
    {
        Keyboard.transform.position = StartingMenu.transform.position + new Vector3(0f, 1f, 0f);
        StartingMenu.transform.position = new Vector3(0, -100, 0);
    }

    public void DisplayLoadMenu()
    {
        LociMenu.transform.position = StartingMenu.transform.position;
        StartingMenu.transform.position = new Vector3(0, -100, 0);
    }

    public void DisplaySettingsMenu()
    {
        StartingMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void ExitSettingsMenu()
    {
        StartingMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }
}
