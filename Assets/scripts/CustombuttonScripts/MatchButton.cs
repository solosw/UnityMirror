using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class MatchButton :CustomButtonbase
{
    public MatchButton(NetworkIdentity network, ButtonType buttonType, Button buttonKey) : base(network, buttonType, buttonKey)
    {
    }

    public override void ButtonEvent()
    {
        //base.ButtonEvent();
        if (networkIdentity == null)
        {
            foreach (var item in GameObject.FindObjectsOfType<NetworkIdentity>())
            {
                if(item.isLocalPlayer&&item.tag=="Player")
                {
                    networkIdentity = item;
                }
            }
        }
        if(networkIdentity==null)
        {
            Debug.LogError("identity is null");
            return;
        }
        NetworkClient.Send<MatchMesssage>(new MatchMesssage { WhoMatch = networkIdentity }) ;
        RoomUiManager.Instance.MatchPanel.SetActive(true);
        RoomUiManager.Instance.AllNetWorkbuttonParent.SetActive(false);
        ClientManager.Instance.RoomplayerStatus.roomPlayertype = RoomPlayertype.Match;
        //ButtonKey.interactable = false;
    }
    
}
