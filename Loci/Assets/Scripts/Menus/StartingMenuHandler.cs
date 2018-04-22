using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingMenuHandler : MonoBehaviour {

    public GameObject LociMenu;
    public GameObject StartingMenu;
    public GameObject SettingsMenu;
    public GameObject Keyboard;
    public GameObject LociButtons;

	void Start () { }

    public void NewLoci()
    {
        Keyboard.transform.position = StartingMenu.transform.position + new Vector3(0f, 1f, 0f);
        StartingMenu.transform.position = new Vector3(0, -100, 0);
    }

    public void DisplayLoadMenuEdit()
    {
        LociMenu.transform.position = StartingMenu.transform.position;
        StartingMenu.transform.position = new Vector3(0, -100, 0);
        LociButtons.GetComponent<LociListControl>().EditMode = true;
    }

    public void DisplayLoadMenuView()
    {
        LociMenu.transform.position = StartingMenu.transform.position;
        StartingMenu.transform.position = new Vector3(0, -100, 0);
        LociButtons.GetComponent<LociListControl>().EditMode = false;
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
