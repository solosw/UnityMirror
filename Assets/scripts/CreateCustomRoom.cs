using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class CreateCustomRoom :  ICreateRoomAttribute
{
    private List<RoomPlayerAttribute> roomPlayers = new List<RoomPlayerAttribute>();
    public List<RoomPlayerAttribute> RoomPlayers { get => roomPlayers; }
    private int currentPlayers=0;
    public bool isAllowStart { get => ReadyPlayers == PlayerNumber; }
    public bool isFilled { get => CurrentPlayers == PlayerNumber; }
    private int ReadyPlayers=1;
    public bool IsStart;
    public CreateCustomRoom(string roomName,string OwnerName,int playerNumber) 
    {
        this. OwnerName = OwnerName;
        PlayerNumber = playerNumber;
        RoomName = roomName;
    }
    public string OwnerName { get  ; set ; }
    public int PlayerNumber { get ; set; }
    public string RoomName { get ;set; }
    public int CurrentPlayers { get => currentPlayers;  }

    public void AddRoomPlayer(string name,NetworkIdentity identity,bool isOwner)
    {
        roomPlayers.Add(new RoomPlayerAttribute(identity, name, isOwner));
        Debug.LogWarning(identity.netId + " " + name);
        currentPlayers++;
    }
    public string GetAllPlayerName()
    {
        string allName = "";
        foreach (var item in roomPlayers)
        {
            allName += item.PlayerName + "/";
        }
        return allName;
    }
    public string GetPlayerNamebyIdentity(NetworkIdentity identity)
    {
        
        foreach (var item in roomPlayers)
        {
            if (item.identity==identity)
            {
               
                return item.PlayerName;
            } 
        }
        return "";
    }
    public void ChangeReadyPlayer(bool isReady)
    {
        if (isReady) ReadyPlayers++;
        else ReadyPlayers--;
        if (ReadyPlayers < 0) Debug.LogError("Readyplayer<0");
    }
    public RoomPlayerAttribute GetPlayerByIdentity(NetworkIdentity identity)
    {
        foreach (var item in roomPlayers)
        {
            if (item.identity == identity)
            {
                return item;
            }
        }
        return null;
    }
    public RoomPlayerAttribute GetPlayerByName(string name)
    {
        foreach (var item in roomPlayers)
        {
            if (item.PlayerName== name)
            {
                return item;
            }
        }
        return null;
    }
    public void RemovePlayer(NetworkIdentity identity)
    {
        RoomPlayerAttribute playerAttribute = GetPlayerByIdentity(identity);
        if(playerAttribute==null)
        {
            Debug.LogError("没有玩家");
            return;
        }
        if(playerAttribute.isReady)
        {
            ReadyPlayers--;
        }
        currentPlayers--;
        roomPlayers.Remove(playerAttribute);
    }
    public void RemovePlayer(string name)
    {
        RoomPlayerAttribute playerAttribute = GetPlayerByName(name);
        if (playerAttribute == null)
        {
            Debug.LogError("没有玩家");
            return;
        }
        if (playerAttribute.isReady)
        {
            ReadyPlayers--;
        }
        currentPlayers--;
        roomPlayers.Remove(playerAttribute);
    }
    public void ChangeOwner(RoomPlayerAttribute Oldowner, RoomPlayerAttribute newOwner)
    {
        if(Oldowner.isOwner)
        {
            Oldowner.isOwner = false;
            newOwner.isOwner = true;
        }else
        {
            Debug.LogWarning("没有房主授权");
        }
    }
    public RoomPlayerAttribute GetRoomPlayerAttributeByConnectID(NetworkConnectionToClient con)
    {
        foreach (var item in RoomPlayers)
        {
            if(item.identity.connectionToClient.connectionId==con.connectionId)
            {
                return item;
            }
        }
        return null;
    }
}
