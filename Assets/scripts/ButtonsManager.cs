using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsManager 
{

    private List<ButtonBase> allButtons = new List<ButtonBase>();
    public void InitMainButton(ButtonBase button)
    {   
        button.Init();
    }
    public ButtonBase GetButtonByType(ButtonType type)
    {
        foreach (var item in allButtons)
        {
            if(item.ButtonType==type)
            {
                return item;
            }
        }
        return null;
    }
}
