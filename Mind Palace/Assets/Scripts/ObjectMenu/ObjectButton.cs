using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectButton : MonoBehaviour {
    [SerializeField]
    private Text text;
    [SerializeField]
    private ObjectListControl buttonControl;

    private int objectNum; //Integer corresponding to object

    public void setText(string textString)
    {
        text.text = textString;
    }

    public void setObjectNum(int objectNum)
    {
        this.objectNum = objectNum;
    }

    public void respondToClick()
    {
        buttonControl.listButtonClicked(this.objectNum);
    }
}
