using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class MatchRoom : RoomBase
{
    public bool isHanderStart { get => ReadyNumber == StartPlayerCount; }
    protected int ReadyNumber = 0;
    public float waitTime=0;
    public MatchRoom(string roomName, int roomId, int startPlayerCount) : base(roomName, roomId, startPlayerCount)
    {
    }
    public bool IscontainPlayer(NetworkIdentity identity)
    {
        return Players.Contains(identity);
    }
    public void ReadyPlayerAdd()
    {
        ReadyNumber++;
    }
    protected void AllocateTeamId()//分配TeamID
    {
        foreach (var item in Players)
        {
            item.gameObject.GetComponent<NetworkTeam>().teamId = RoomName;
        }
    }
    protected void StartLevelScene()
    {
        foreach (var item in Players)
        {
            NetworkServer.SendToAll<LoadGameSceneMessage>(new LoadGameSceneMessage() { isLoadScene = true, WhoLoadScene = item });
        }
        
    }
    public void JoinCustomGameLevel()//开始第一关
    {
        AllocateTeamId();
        StartLevelScene();
        IsGameStart = true;
    }
    public void DeletePlayer(NetworkIdentity identity)
    {
        if(Players.Contains(identity))
        {
            Players.Remove(identity);
        }  
    }
    public NetworkIdentity GetNetworkIdentityByconnID(NetworkConnectionToClient conn)
    {
        foreach (var item in Players)
        {
            if(item.connectionToClient.connectionId==conn.connectionId)
            {
                return item;
            }
        }
        return null;
    }
}
