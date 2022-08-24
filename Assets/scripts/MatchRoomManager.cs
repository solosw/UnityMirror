using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;
public class MatchRoomManager 
{
    private int StartPlayerIdex = 1;
    private int MatchIdex = 0;
    private float MaxWaitTime=30;
    private List<MatchRoom> AllMatchRoom = new List<MatchRoom>();
    public MatchRoomManager(int startPlayerIdex,float maxWaittime)
    {
        StartPlayerIdex = startPlayerIdex;
        MaxWaitTime = maxWaittime;
    }

    private string MatchRoomName()
    {
        MatchIdex++;
        return "match" + MatchIdex.ToString();
    }
    public MatchRoom AddMatchRoomPlayer(NetworkIdentity player)
    {
        if (AllMatchRoom.Count == 0)
        {
            AllMatchRoom.Add(new MatchRoom(MatchRoomName(), MatchIdex, StartPlayerIdex));
            AllMatchRoom[0].AddPlayer(player);
            return null;
        }
        else 
        {
            foreach (var item in AllMatchRoom)
            {
                if (!item.IsAutoStart&&!item.IsGameStart)
                {
                    item.AddPlayer(player);
                    Debug.LogWarning(item.Players.Count);
                    if (item.IsAutoStart) return item;
                    return null;
                }
            }
        }
        AllMatchRoom.Add(new MatchRoom(MatchRoomName(), MatchIdex, StartPlayerIdex));
        AllMatchRoom[AllMatchRoom.Count - 1].AddPlayer(player);
        return null;    
    }
    public void StartMatchGamebyAuto(MatchRoom room)
    {
        if (room == null)
        {
            Debug.LogWarning("game is not start");
            return;
        }
        else
        {
            foreach (var item in room.Players)
            {
                item.gameObject.GetComponent<NetworkTeam>().teamId = room.RoomName;
            }
        }
    }
   private bool IsMatchSuccess(MatchRoom room)
    {
        if (room == null) return false;
        else
        {
            return true;
        }
    }
    public void SendMatchSuccessMessage(MatchRoom room)
    {
        if(IsMatchSuccess(room))
        {
            foreach (var item in room.Players)
            {
                NetworkServer.SendToAll<MatchSuccessMessage>(new MatchSuccessMessage { isMatchSuccess = true, WhoMatchSuccess = item });
            }
        }
        
    }
    public void StartGameByHander(NetworkIdentity identity)
    {
        MatchRoom room = GetMatchRoomByNetworkIdentity(identity);
        if (room == null) return;
        room.ReadyPlayerAdd();
        if(room.isHanderStart)
        {
            room.JoinCustomGameLevel();
        }
    }
    public MatchRoom GetMatchRoomByNetworkIdentity(NetworkIdentity identity)
    {
        foreach (var item in AllMatchRoom)
        {
            if(item.IscontainPlayer(identity))
            {
                return item;
            }
        }
        return null;
    }
    protected void DeleteMatchRoomPlayer(NetworkIdentity identity)
    {
        MatchRoom room = GetMatchRoomByNetworkIdentity(identity);
        if (room == null) { Debug.LogError("Player is not existed");return; }
        room.DeletePlayer(identity);
    }
    public void ReciveCancelMessage(NetworkIdentity identity)
    {
        DeleteMatchRoomPlayer(identity);
    }
   public void DeleteMatchRoom(MatchRoom room)
    {
        if(AllMatchRoom.Contains(room))
        {
            AllMatchRoom.Remove(room);
        }
        else
        {
            Debug.LogError("MatchRoom is not existed");
        }
    }
    public IEnumerator MatchTimeOut(MatchRoom room)
    {
        while(!room.IsGameStart&&room.waitTime<MaxWaitTime)
        {
            yield return new WaitForSeconds(1);
            room.waitTime++;
        }
        if(room.waitTime>=MaxWaitTime&&!room.IsGameStart)
        {
            foreach (var item in room.Players)
            {   
                NetworkServer.SendToAll<MatchTimeOutMessage>(new MatchTimeOutMessage() { reason = "ÓÐÈËÍË³ö",WhoMatch=item });
            }
            DeleteMatchRoom(room);
        }
    }
   public MatchRoom GetMatchRoomByConnID(NetworkConnectionToClient con)
    {
        foreach (var item in AllMatchRoom)
        {
            foreach (var player in item.Players)
            {  
                if(player.connectionToClient.connectionId==con.connectionId)
                {
                    return item;
                }
            }
        }
        return null;
    }
    
}
