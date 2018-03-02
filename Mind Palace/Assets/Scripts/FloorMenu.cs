using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FloorMenu : MonoBehaviour {
    public GameObject MainButton;
    public GameObject Left;
    public GameObject Right;
    public GameObject CurrentThemeDisplay;
    public GameObject CloseButton;
    public GameObject Room;
    public static int NUMBER_OF_THEMES = 3;

    private int CurrentThemeNumber = 1;
    private RoomBuilder RoomTextureSetter;

	void Start () {
        RoomTextureSetter = Room.GetComponent<RoomBuilder>();
    }

    public void ShowMoreOptions()
    {
        MainButton.SetActive(false);
        Left.SetActive(true);
        Right.SetActive(true);
        CurrentThemeDisplay.SetActive(true);
        CloseButton.SetActive(true);
        CheckGreyOut();
    }

    public void DefaultState()
    {
        MainButton.SetActive(true);
        Left.SetActive(false);
        Right.SetActive(false);
        CurrentThemeDisplay.SetActive(false);
        CloseButton.SetActive(false);
    }

    public void LeftClick()
    {
        CurrentThemeNumber--;
        if (CurrentThemeNumber < 1)
        {
            CurrentThemeNumber = 1;
        }
        CurrentThemeDisplay.GetComponentInChildren<Text>().text= "Theme " +CurrentThemeNumber;
        CheckGreyOut();
        SetTheme(CurrentThemeNumber);
    }

    public void RightClick()
    {
        CurrentThemeNumber++;
        if (CurrentThemeNumber > NUMBER_OF_THEMES)
        {
            CurrentThemeNumber = NUMBER_OF_THEMES;
        }
        CurrentThemeDisplay.GetComponentInChildren<Text>().text = "Theme " + CurrentThemeNumber;
        CheckGreyOut();
        SetTheme(CurrentThemeNumber);
    }

    private void CheckGreyOut()
    {
        if (CurrentThemeNumber == 1)
        {
            Left.GetComponent<Button>().interactable = false;
            Right.GetComponent<Button>().interactable = true;
        }
        else if (CurrentThemeNumber == NUMBER_OF_THEMES)
        {
            Left.GetComponent<Button>().interactable = true;
            Right.GetComponent<Button>().interactable = false;
        }else
        {
            Left.GetComponent<Button>().interactable = true;
            Right.GetComponent<Button>().interactable = true;
        }
    }

    private void SetTheme(int ThemeNumber)
    {
        switch(ThemeNumber){
            case 1:
                RoomTextureSetter.setMaterials(
                "Wood Texture 06", // floor material
                "Wood Texture 15", // roof material
                "Wood texture 12"  // wall material
                 );
                break;
            case 2:
                RoomTextureSetter.setMaterials(
                "Wood Texture 12", // floor material
                "Wood Texture 06", // roof material
                "Wood texture 15"  // wall material
                 );
                break;
            default:
                RoomTextureSetter.setMaterials(
                "Wood Texture 15", // floor material
                "Wood Texture 12", // roof material
                "Wood texture 06"  // wall material
                 );
                break;
        }
    }

}
