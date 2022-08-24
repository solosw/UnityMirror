using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class MirrorButtonBase : ButtonBase
{
    protected NetworkManager manager;

    public MirrorButtonBase(NetworkManager manager,ButtonType buttonType, Button buttonKey) : base(buttonType, buttonKey)
    {
        this.manager = manager;
    }

   
}
