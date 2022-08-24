using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRoomButton : CustomButtonbase
{
    public CreateRoomButton(NetworkIdentity network, ButtonType buttonType, Button buttonKey) : base(network, buttonType, buttonKey)
    {
    }

    public override void ButtonEvent()
    {
        RoomUiManager.Instance.AllNetWorkbuttonParent.SetActive(false);
        RoomUiManager.Instance.CreateRoomPanel.SetActive(true);
    }
}
    

