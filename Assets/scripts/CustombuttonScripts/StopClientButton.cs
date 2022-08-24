using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopClientButton : MirrorButtonBase
{
    public StopClientButton(NetworkManager manager, ButtonType buttonType, Button buttonKey) : base(manager, buttonType, buttonKey)
    {
    }

    public override void ButtonEvent()
    {   
            manager.StopClient();
       
    }
}
