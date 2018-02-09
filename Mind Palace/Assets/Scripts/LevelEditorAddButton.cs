using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorAddButton : MonoBehaviour {
    public GameObject ClickedOn;
    public GameObject Room;
    public GameObject Window;
    public GameObject Picture;


    public void ShowMoreOptions()
    {
        ClickedOn.SetActive(false);
        Room.SetActive(true);
        Window.SetActive(true);
        Picture.SetActive(true);
    }
}
