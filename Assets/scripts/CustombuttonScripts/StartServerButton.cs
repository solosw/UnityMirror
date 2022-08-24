using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartServerButton : MirrorButtonBase
{
    public StartServerButton(NetworkManager manager, ButtonType buttonType, Button buttonKey) : base(manager, buttonType, buttonKey)
    {
    }
    public override void ButtonEvent()
    {
        manager.StartServer();
       
    }
}
