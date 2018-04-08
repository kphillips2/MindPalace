using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject buttonTemplate;
    [SerializeField]
    private Button backButton;
    private List<GameObject> buttons = new List<GameObject>();
    public GameObject ObjectMenu;
    private string[] objectList = {"Bed", "Bookcase", "Chair", "Dining Chair","Coffee Table", "Couch", "Counter", "Fireplace",
        "Flower Table", "Fridge", "Guitar","Headboard", "Lion Statue", "Oven", "Plant", "Sink", "Soccerball", "Table", "Toilet", "TV"};

    // Use this for initialization
    void Start()
    {
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
        foreach (string objectName in objectList)
        {
            GameObject button = Instantiate(buttonTemplate) as GameObject;
            button.SetActive(true);
            button.GetComponent<ObjectButton>().setText(objectName);
            button.GetComponent<ObjectButton>().setObjectNum(num);
            button.transform.SetParent(buttonTemplate.transform.parent, false);
            buttons.Add(button);
            num++;
        }
    }

    public void listButtonClicked(int objectNum)
    {
        ObjectPlacer op = new ObjectPlacer();
        op.createPrefabInHand(objectNum);
    }

    public void backButtonClicked()
    {
        ObjectMenu.transform.position = new Vector3(0, -100, 0);
    }

    /*
    private int counter = 0;
    private int timeshappened = 0;
    void Update()
    {
        if(counter == 250 && timeshappened < 1)
        {
            listButtonClicked(1);
            timeshappened++;
            counter = 0;
        }
        counter++;
    }*/
}
