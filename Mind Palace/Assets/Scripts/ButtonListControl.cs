using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ButtonListControl : MonoBehaviour {
    [SerializeField]
    private GameObject buttonTemplate;

    private List<GameObject> buttons = new List<GameObject>();
    private string filePath;

	// Use this for initialization
	void Start ()
    {
        filePath = "Assets/Resources"; //Where the pictures are stored
        generateButtons();
	}

    public void generateButtons()
    {
        if (buttons.Count > 0)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }

        string[] files = Directory.GetFiles(filePath);
        string[] directories = Directory.GetDirectories(filePath);

        foreach(string d in directories)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            button.GetComponent<ButtonListButton>().setText(d.Substring(filePath.Length + 1));
            button.transform.SetParent(buttonTemplate.transform.parent, false);
            buttons.Add(button);
        }

        foreach (string f in files)
        {
            string extension = Path.GetExtension(f);
            if (extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg") // Only list jpg files
            {
                GameObject button = Instantiate(buttonTemplate) as GameObject;
                button.SetActive(true);
                button.GetComponent<ButtonListButton>().setText(f.Substring(filePath.Length + 1));
                button.transform.SetParent(buttonTemplate.transform.parent, false);
                buttons.Add(button);
            }
        }
    }
	
	public void buttonClicked(string textString)
    {
        Debug.Log(textString);
    }
}
