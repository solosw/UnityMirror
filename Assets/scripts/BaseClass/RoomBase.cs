using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public abstract class RoomBase
{
    private string roomName;
    private int roomId;
    private bool isAutoStart;//为假就为匹配中，为真就是匹配完成
    protected int StartPlayerCount;
    private List<NetworkIdentity> players = new List<NetworkIdentity>();
    public bool IsGameStart=false;
    protected RoomBase(string roomName, int roomId, int startPlayerCount)
    {
        this.roomName = roomName;
        this.roomId = roomId;
        StartPlayerCount = startPlayerCount;
    }
    protected RoomBase() { }

    public void AddPlayer(NetworkIdentity identity)
    {   if (players.Contains(identity))
        {
            Debug.LogError("Player has existed");
            return;
        }
       players.Add(identity);
    }
    public bool IsAutoStart { get => players.Count == StartPlayerCount; }
    public List<NetworkIdentity> Players { get => players;  }
    public string RoomName { get => roomName;  }
}
