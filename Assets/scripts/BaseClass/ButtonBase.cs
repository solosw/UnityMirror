using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class ButtonBase 
{
    private ButtonType buttonType;
    private Button buttonKey;
    public ButtonBase(ButtonType buttonType, Button buttonKey)
    {
        this.buttonType = buttonType;
        this.buttonKey = buttonKey;
    }
    public Button ButtonKey { get => buttonKey; set => buttonKey = value; }
    public ButtonType ButtonType { get => buttonType; set => buttonType = value; }

    private void AddListener()
    {
        buttonKey.onClick.AddListener(ButtonEvent);
    }
    public virtual void ButtonEvent()
    {
        Debug.Log(buttonType.ToString());
    }
    public void Init()
    {
        AddListener();
    }
}
