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
            LoadFile.ClearLoci();
            SaveFile.name = input.text;
            SaveFile.isNewLoci = true;
            input.text = "";
            SceneManager.LoadScene("GreenDemo");
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
        }
    }
}