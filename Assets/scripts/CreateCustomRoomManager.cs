using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class CreateCustomRoomManager
{
   private List<CreateCustomRoom> allCreateRooms = new List<CreateCustomRoom>();
   public List<CreateCustomRoom> AllCreateRooms { get => allCreateRooms;  }
   public CreateCustomRoom GetCreateCustomRoomByName(string roomName)
    {
        foreach (var item in AllCreateRooms)
        {
            if(item.RoomName==roomName)
            {
                return item;
            }
        }
        return null;
    }
    public CreateCustomRoom GetCreateCustomRoomByPlayer(NetworkIdentity identity)
    {
        foreach (var item in AllCreateRooms)
        {
            foreach (var player in item.RoomPlayers)
            {
                if (player.identity == identity)
                {
                          return item;
                }
            }
        }
        return null;
    }
    public CreateCustomRoom GetSendReadyPlayerRoom(NetworkIdentity identity,bool isReady)
    {
        foreach (var item in AllCreateRooms)
        {
            foreach (var player in item.RoomPlayers)
            {
                if (player.identity == identity)
                {   
                    item.ChangeReadyPlayer(isReady);
                    player.isReady = isReady;
                    return item;
                } 
            }
        }
        return null;
    }
    public bool IsContaninRoom(string roomName)
    {
        if (GetCreateCustomRoomByName(roomName)!=null ) return true;
        return false;
    }
    
    public void AddCreateRoom(string owner,string roomName,int playernumber,NetworkIdentity identity,string playername)
    {
        CreateCustomRoom room = new CreateCustomRoom(roomName, owner, playernumber);
        room.AddRoomPlayer(playername, identity, true);
        allCreateRooms.Add(room);
    }
    public string ReturnAllRoomsByStr()
    {
        string roomlist="";
        foreach (var item in allCreateRooms)
        {
            roomlist += item.RoomName + "." + item.CurrentPlayers.ToString() + "." + item.PlayerNumber.ToString() + "/";
        }
        return roomlist;
    }
    public void JoinRoom(string RoomName,string playername,NetworkIdentity identity)
    {
        CreateCustomRoom room = GetCreateCustomRoomByName(RoomName);
        if(room==null)
        {
            NetworkServer.SendToAll<JoinIsSuccessMessage>(new JoinIsSuccessMessage() { isSuccess = false, msy = "房间不存在",sendTo=identity });
        }
        else
        {
            if(room.isFilled)
            {
                NetworkServer.SendToAll<JoinIsSuccessMessage>(new JoinIsSuccessMessage() { isSuccess = false, msy = "房间已满",sendTo=identity });
            }
            else
            {
                TellOtherWhoJoin(room, playername);
                room.AddRoomPlayer(playername, identity, false);
                NetworkServer.SendToAll<JoinIsSuccessMessage>(new JoinIsSuccessMessage() { isSuccess = true, msy = room.GetAllPlayerName(),sendTo=identity });
            }
        }
    }
    private void TellOtherWhoJoin(CreateCustomRoom room,string joinName)
    {
        foreach (var item in room.RoomPlayers)
        {
            NetworkServer.SendToAll<JoinIsSuccessTellAllMessage>(new JoinIsSuccessTellAllMessage() { joinname = joinName, SendTo = item.identity });
        }
    }
    public string GetReadyPlayerName(NetworkIdentity identity,bool isReady)
    {
        CreateCustomRoom room = GetSendReadyPlayerRoom(identity,isReady);
        if(room==null)
        {
            Debug.LogError("房间查找有误");
            return "";
        }
        return  room.GetPlayerNamebyIdentity(identity);
    }
    public void TellOtherReady(NetworkIdentity Whoready,bool isReady)
    {
        //Debug.LogWarning(Whoready.netId );
        CreateCustomRoom room = GetSendReadyPlayerRoom(Whoready, isReady);
        string readyName = room.GetPlayerNamebyIdentity(Whoready);
        
        if (room == null)
        {
            Debug.LogWarning("房间查找有误");
            return ;
        }
        foreach (var item in room.RoomPlayers)
        {
            //Debug.LogWarning(item.identity.netId + "  " + item.PlayerName);
            NetworkServer.SendToAll<TellOtherReadyMessage>(new TellOtherReadyMessage() { ReadyPlayerName=readyName,sendTo=item.identity,isReady=isReady});
        }
    }
    public void DeletePlayer(NetworkIdentity identity)
    {
        CreateCustomRoom room = GetCreateCustomRoomByPlayer(identity);
        if(room==null)
        {
            Debug.LogError("Room is not exist");
            return;
        }
        string name=room.GetPlayerNamebyIdentity(identity);
        RoomPlayerAttribute playerAttribute = room.GetPlayerByIdentity(identity);
        if(playerAttribute.isOwner&&room.CurrentPlayers>=2)
        {
            room.ChangeOwner(playerAttribute, room.RoomPlayers[1]);
        }
        room.RemovePlayer(identity);
        if(room.RoomPlayers.Count==0)
        {
            RemoveRoom(room);
        }
        else
        {
            TellOtherDelete(room, name);
        }
    }
    private void TellOtherDelete(CreateCustomRoom room, string deleteName)
    {
        foreach (var item in room.RoomPlayers)
        {
            NetworkServer.SendToAll<TellOtherExitroomMessage>(new TellOtherExitroomMessage { who = deleteName
                , sendto = item.identity ,IsBecomeOwner=item.isOwner});
        }
        
    }
    private void RemoveRoom(CreateCustomRoom room)
    {
        allCreateRooms.Remove(room);
    }
   public void StartGame(NetworkIdentity identity)
    {
        CreateCustomRoom room = GetCreateCustomRoomByPlayer(identity);
        if(room==null)
        {
            Debug.LogError("room is not exist");
            return;
        }else
        {
            if(!room.isAllowStart)
            {
                NetworkServer.SendToAll<LoadSceneByStartGame>(new LoadSceneByStartGame() { isLoadScene = false, WhoLoadScene = identity });
            }else
            {
                room.IsStart = true;
                foreach (var item in room.RoomPlayers)
                {
                    item.identity.GetComponent<NetworkTeam>().teamId = room.RoomName;
                }
                foreach (var item in room.RoomPlayers)
                {
                    NetworkServer.SendToAll<LoadSceneByStartGame>(new LoadSceneByStartGame() { isLoadScene = true, WhoLoadScene = item.identity });
                }
                
            }
        }
    }
    public void KickPlayer(CreateCustomRoom room,string kickname)
    {
        TellOtherKick(room, kickname);
        room.RemovePlayer(kickname);
    }
    private void TellOtherKick(CreateCustomRoom room,string kickName)
    {
        Debug.LogWarning(kickName);
        foreach (var item in room.RoomPlayers)
        {
           
            if(item.PlayerName==kickName)
            {
                NetworkServer.SendToAll<KickTellotherMessage>(new KickTellotherMessage() { IsKicked = true, sendto = item.identity, kickName = kickName });
            }else
            {
                NetworkServer.SendToAll<KickTellotherMessage>(new KickTellotherMessage() { IsKicked = false, sendto = item.identity, kickName = kickName });
            }
        }
    }
    public CreateCustomRoom GetCreateCustomRoomByConnectionID(NetworkConnectionToClient con)
    {
        foreach (var item in allCreateRooms )
        {
            foreach (var player in item.RoomPlayers )
            {
                if(player.identity.connectionToClient.connectionId==con.connectionId)
                {
                    return item;
                }
            }
        }
        return null;
    }
}
