using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;
public class MatchDropProcess : DropProcessBase
{
    public void MatchingDropProcess(Action<NetworkIdentity> DeletMatchPlayerAction,NetworkIdentity identity)
    {
       // Debug.LogError(identity.netId);
        DeletMatchPlayerAction(identity);
    }
    public void MatchFinishedProcess(MatchRoom room,Action<MatchRoom> DeleteMatchRoomAction)
    {   
        if(room==null)
        {
            Debug.LogError("Matchroom is not existed");
            return;
        }
        if ( !room.IsGameStart)
        {
            foreach (var item in room.Players)
            {
                NetworkServer.SendToAll<MatchTimeOutMessage>(new MatchTimeOutMessage() { reason = "ÓÐÈËÍË³ö", WhoMatch = item });
            }
            DeleteMatchRoomAction(room);
        }
    }
    
    public void GamingDropProcess(Action<NetworkIdentity> DeletMatchPlayerAction, NetworkIdentity identity)
    {

    }

    public override void GamingDropProcess<T>(Action<T> GameDropAction, T obj)
    {
        base.GamingDropProcess(GameDropAction, obj);
    }
}
