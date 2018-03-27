using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LociListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private Button backButton;
    private List<GameObject> buttons = new List<GameObject>();
    public GameObject LociMenu;

    // Use this for initialization
    void Start()
    {
        SaveFile.load();
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
        foreach (Loci l in SaveFile.savedLocis)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            button.GetComponent<LociButton>().setText(l.getName());
            button.GetComponent<LociButton>().setLociNum(num);
            button.transform.SetParent(buttonTemplate.transform.parent, false);
            buttons.Add(button);
            num++;
        }
    }

    public void listButtonClicked(int lociNum)
    {
        SaveFile.currentLoci = SaveFile.savedLocis[lociNum];
        //Code for loading
    }

    public void backButtonClicked()
    {
        //Code for back button
    }
}
