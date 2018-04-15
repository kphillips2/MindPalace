using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LociListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private Button backButton;
    private List<GameObject> buttons = new List<GameObject>();
    public GameObject LociMenu;
    public GameObject StartingMenu;

    // Use this for initialization
    void Start()
    {
        LoadFile.Load ();
        generateButtons();
    }

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
        int num = 0;
        foreach (string name in SaveFile.savedLocis.Keys)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            button.GetComponent<LociButton>().setText(name);
            button.transform.SetParent(buttonTemplate.transform.parent, false);
            buttons.Add(button);
            num++;
        }
    }

    public void listButtonClicked(string lociName)
    {
        LoadFile.LoadLoci(lociName);
        SaveFile.isNewLoci = false;
        SceneManager.LoadScene("GreenDemo");
    }

    public void backButtonClicked()
    {
        StartingMenu.transform.position = LociMenu.transform.position;
        LociMenu.transform.position = new Vector3(0, -100, 0);
    }
}
