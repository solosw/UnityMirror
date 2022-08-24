using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class CustomButtonbase :ButtonBase
{
    protected NetworkIdentity networkIdentity;

    public CustomButtonbase(NetworkIdentity network, ButtonType buttonType, Button buttonKey) : base(buttonType, buttonKey)
    {
        networkIdentity = network;
    }
}
