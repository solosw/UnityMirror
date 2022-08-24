using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class RoomPlayerAttribute : IPlayerAttribute
{
    public RoomPlayerAttribute(NetworkIdentity identity, string playerName, bool isOwner)
    {
        this.identity = identity;
        PlayerName = playerName;
        this.isOwner = isOwner;
    }
    public NetworkIdentity identity { get; set; }
    public string PlayerName { get ; set ; }
    public bool isOwner { get; set; }
    public bool isReady=false;
}
