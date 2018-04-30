namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;

    public class UI_Keyboard : MonoBehaviour
    {
        private InputField input;
        public GameObject Keyboard;
        public GameObject StartingMenu;
        public GameObject level;
        private Building building;

        public void ClickKey(string character)
        {
            input.text += character;
        }

        public void Backspace()
        {
            if (input.text.Length > 0)
            {
                input.text = input.text.Substring(0, input.text.Length - 1);
            }
        }

        //Function called when Enter key is pressed
        public void Enter()
        {
            if(input.text.Length > 0)
            {
                LoadFile.ClearLoci();
                SaveFile.name = input.text;
                SaveFile.isNewLoci = true;
                input.text = "";
                SaveFile.EditMode = true;
                SceneManager.LoadScene("GreenDemo");
            }  
        }

        public void SaveAs()
        {
            building = level.GetComponent<Building>();
            building.Save();
            SaveFile.name = input.text;
            SaveFile.Save();
            input.text = "";
            SceneManager.LoadScene(0);
        }

        public void Cancel()
        {
            input.text = "";
            StartingMenu.transform.position = Keyboard.transform.position - new Vector3(0f,1f,0f);
            Keyboard.transform.position = new Vector3(0f, -100f, 0f);
        }

        private void Start()
        {
            input = GetComponentInChildren<InputField>();
            ChangeFont("BebasNeue", 180);
        }

        //Font on keyboard keys will be changed to given font and font size
        public void ChangeFont(string fontName, int fontSize)
        {
            foreach (Transform t in transform.GetComponentsInChildren<Transform>())
            {
                if (t.name == "Text")
                {
                    t.GetComponent<Text>().font = Resources.Load("Fonts/" + fontName, typeof(Font)) as Font;
                    t.GetComponent<Text>().fontSize = fontSize;
                }
                else if(t.name == "TopText")
                {
                    t.GetComponent<Text>().font = Resources.Load("Fonts/" + fontName, typeof(Font)) as Font;
                    t.GetComponent<Text>().fontSize = fontSize * 25/150;
                }
                else if (t.name == "CancelText")
                {
                    t.GetComponent<Text>().font = Resources.Load("Fonts/" + fontName, typeof(Font)) as Font;
                    t.GetComponent<Text>().fontSize = fontSize * 95 / 150;
                }
                else if (t.name == "Placeholder")
                {
                    t.GetComponent<Text>().font = Resources.Load("Fonts/" + fontName, typeof(Font)) as Font;
                    t.GetComponent<Text>().fontSize = fontSize * 14 / 150;
                }
                else if (t.name == "InputText")
                {
                    t.GetComponent<Text>().font = Resources.Load("Fonts/" + fontName, typeof(Font)) as Font;
                    t.GetComponent<Text>().fontSize = fontSize * 24 / 150;
                }
            }
        }
    }
}