using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchRoomButton : CustomButtonbase
{
    public SearchRoomButton(NetworkIdentity network, ButtonType buttonType, Button buttonKey) : base(network, buttonType, buttonKey)
    {
    }

    public override void ButtonEvent()
    {
        if (networkIdentity == null)
        {
            foreach (var item in GameObject.FindObjectsOfType<NetworkIdentity>())
            {
                if (item.isLocalPlayer && item.tag == "Player")
                {
                    networkIdentity = item;
                }
            }
        }
        RoomUiManager.Instance.roomListPanel.SetActive(true);
        NetworkClient.Send<SearchRequstMessage>(new SearchRequstMessage() { WhoSend = networkIdentity });
        RoomUiManager.Instance.AllNetWorkbuttonParent.SetActive(false);
        //RoomUiManager.Instance.roomPanel.SetActive(true);
    }
}
