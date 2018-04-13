namespace VRTK.Examples
{
    using UnityEngine;
    using UnityEngine.UI;

    public class UI_Keyboard : MonoBehaviour
    {
        private InputField input;

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
            //Code for loading scene and storing entered loci name
            //Entered loci name is 'input.text'
            Debug.Log("You've typed [" + input.text + "]");
            input.text = "";
        }

        private void Start()
        {
            input = GetComponentInChildren<InputField>();
        }
    }
}