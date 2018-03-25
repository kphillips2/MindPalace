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

    private int lociNum; //Integer corresponding to loci save file

    public void setText(string textString)
    {
        text.text = textString;
    }

    public void setLociNum(int lociNum)
    {
        this.lociNum = lociNum;
    }

    public void respondToClick()
    {
        buttonControl.listButtonClicked(this.lociNum);
    }
}
