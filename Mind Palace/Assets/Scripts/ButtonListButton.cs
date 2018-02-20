using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour {
    [SerializeField]
    private Text text;
    [SerializeField]
    private ButtonListControl buttonControl;

    private string textString;

	public void setText(string textString)
    {
        this.textString = textString;
        text.text = textString;
    }
	
	public void outputString()
    {
        buttonControl.buttonClicked(this.textString);
    }
}
