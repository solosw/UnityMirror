using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class StartClientButton : MirrorButtonBase
{
    public StartClientButton(NetworkManager manager, ButtonType buttonType, Button buttonKey) : base(manager, buttonType, buttonKey)
    {

    }

    public override void ButtonEvent()
    {
        base.ButtonEvent();
  
        manager.StartClient();
    }
}
