using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartingMenuHandler : MonoBehaviour {

    public GameObject LociMenu;
    public GameObject StartingMenu;
    public GameObject SettingsMenu;

	void Start () {}

    public void NewLoci()
    {
        LoadFile.ClearLoci();
        SceneManager.LoadScene("GreenDemo");
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
