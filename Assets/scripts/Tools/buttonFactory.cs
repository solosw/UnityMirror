using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
public class buttonFactory 
{
    public static MirrorButtonBase CreateButton(ButtonType type,NetworkManager manager,Button buttonkey)
    {   //todo
        switch (type)
        {
            case ButtonType.None:
                Debug.LogError("buttonType is Null");
                return null;  
            case ButtonType.StartServer:
                return new StartServerButton(manager,type,buttonkey);
            case ButtonType.StopServer:
                return new StopServerButton(manager, type, buttonkey);
            case ButtonType.StartClient:
                return new StartClientButton(manager,type,buttonkey);
            case ButtonType.StopClient:
                return new StopClientButton(manager, type, buttonkey); 
        }
        return null;
    }
    public static CustomButtonbase CreateButton(NetworkIdentity identity, ButtonType buttontype,Button buttonkey)
    {
        switch (buttontype)
        {
            case ButtonType.None:
                Debug.LogError("buttonType is Null");
                return null;
            case ButtonType.StartMatch:
                return new MatchButton( identity,buttontype, buttonkey);
            case ButtonType.CreateRoom:
                return new CreateRoomButton(identity, buttontype, buttonkey);
            case ButtonType.JoinRoom:
                return new JoinRoombutton(identity, buttontype, buttonkey);
            case ButtonType.SearchRoom:
                return new SearchRoomButton(identity, buttontype, buttonkey);
        }
        return null;
    }
}
