using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LociButton : MonoBehaviour
{
    [SerializeField]
    private Text text;
    [SerializeField]
    private LociListControl buttonControl;

    private string lociName; //Name corresponding to loci save file

    public void setText(string textString)
    {
        text.text = textString;
        lociName = textString;
    }

    public void respondToClick()
    {
        buttonControl.listButtonClicked(this.lociName);
    }
}
