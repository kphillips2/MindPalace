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
    private bool isFile; //True if corresponds to an image file, false if directory

	public void setText(string textString)
    {
        this.textString = textString;
        text.text = textString;
    }
    
    public void setIsFile(bool isFile)
    {
        this.isFile = isFile;
    }
	
	public void respondToClick()
    {
        buttonControl.listButtonClicked(this.textString, this.isFile);
    }
}
