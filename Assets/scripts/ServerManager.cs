using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class ServerManager : NetworkBehaviour
{
    public static ServerManager Instance;
    public int StartPlayerIdex = 2;
    public float MaxWaitTime = 30;
    private MatchRoomManager matchRoomManager;
    private MatchDropProcess matchdropProcess;
    private CreateRoomDrop CreateRoomDrop;
    private CreateCustomRoomManager createroomManager;
    private List<GameObject> Allplayers = new List<GameObject>();
    public override void OnStartServer()
    {
        matchRoomManager = new MatchRoomManager(StartPlayerIdex, MaxWaitTime);
        matchdropProcess = new MatchDropProcess();
        Instance = this;
        createroomManager = new CreateCustomRoomManager();
        NetworkServer.RegisterHandler<MatchMesssage>(ReciveMatchMessage);
        NetworkServer.RegisterHandler<AllowMatchMessage>(ReciveAllowMatchMessage);
        NetworkServer.RegisterHandler<CancelMatchMessage>(ReciveCancelMatchMessage);
        NetworkServer.RegisterHandler<CreateRoomMessage>(ReciveCreateRoomMessage);
        NetworkServer.RegisterHandler<SearchRequstMessage>(ReciveSearchRoomMessage);
        NetworkServer.RegisterHandler<JoinMessage>(ReciveJoinRoomMessage);
        NetworkServer.RegisterHandler<ReadyMessage>(ReciveReadyMessage);
        NetworkServer.RegisterHandler<ExitRoomMessage>(ReciveExitRoomMessage);
        NetworkServer.RegisterHandler<StartGameMessage>(ReciveStartGameMessage);
        NetworkServer.RegisterHandler<KickPlayerMessage>(ReciveKickMessage);
    }
   
    private void ReciveMatchMessage(NetworkConnectionToClient connectionToClient, MatchMesssage msy)
    {
        //Debug.LogWarning(msy.WhoMatch.gameObject.name);
        Allplayers.Add(msy.WhoMatch.gameObject);
        MatchRoom room = matchRoomManager.AddMatchRoomPlayer(msy.WhoMatch);
        //Debug.LogError(msy.WhoMatch.connectionToServer.connectionId);
        // matchRoomManager.StartMatchGame(room);
        matchRoomManager.SendMatchSuccessMessage(room);
        if(room!=null)
            StartCoroutine(matchRoomManager.MatchTimeOut(room));
    }
    private void ReciveAllowMatchMessage(NetworkConnectionToClient connectionToClient, AllowMatchMessage msy)
    {
        //Debug.Log(msy.WhoAllow.netId);
        matchRoomManager.StartGameByHander(msy.WhoAllow);
    }
    private void ReciveCancelMatchMessage(NetworkConnectionToClient connectionToClient, CancelMatchMessage msy)
    {
        matchRoomManager.ReciveCancelMessage(msy.WhoCancelMatch);
    }
    
    public void MatchDropProcess(NetworkConnectionToClient con)
    {
        MatchRoom room= matchRoomManager.GetMatchRoomByConnID(con);
        if(room==null)
        {   
            Debug.Log("房间不存在");
            return;
        }
        NetworkIdentity identity = room.GetNetworkIdentityByconnID(con);
        if(identity==null)
        {
            Debug.LogError("player is not existed");
        }
        if(!room.IsAutoStart)
        {
            Debug.LogWarning("MatchingDrop");
            matchdropProcess.MatchingDropProcess(matchRoomManager.ReciveCancelMessage,identity);
        }else if(room.IsAutoStart&&!room.IsGameStart)
        {
            matchdropProcess.MatchFinishedProcess(room, matchRoomManager.DeleteMatchRoom);
            Debug.LogWarning("MatchFinishedDrop");
        }
        if(room.IsGameStart)
        {
            if (room.Players.Count == 1) matchdropProcess.GamingDropProcess<MatchRoom>(matchRoomManager.DeleteMatchRoom, room);
            if (room.Players.Count > 1) matchdropProcess.GamingDropProcess<NetworkIdentity>(matchRoomManager.ReciveCancelMessage, identity);
        }
    }
    private void ReciveCreateRoomMessage(NetworkConnectionToClient connectionToClient, CreateRoomMessage msy)
    {
        if (createroomManager.IsContaninRoom(msy.RoomName))
        {
            NetworkServer.SendToAll<IsSuccessCreate>(new IsSuccessCreate() { IsSuccess = false, WhoseRoom = msy.WhoseRoom });
        }else
        {
            createroomManager.AddCreateRoom(msy.RoomOwnerName, msy.RoomName, msy.PlayerNumber, msy.WhoseRoom, msy.RoomOwnerName);
            NetworkServer.SendToAll<IsSuccessCreate>(new IsSuccessCreate() { IsSuccess = true, WhoseRoom = msy.WhoseRoom });
        }
    }
    private void ReciveSearchRoomMessage(NetworkConnectionToClient connectionToClient, SearchRequstMessage msy)
    {
        NetworkServer.SendToAll<RoomListMessage>(new RoomListMessage() { Roomlist = createroomManager.ReturnAllRoomsByStr(), sendTo = msy.WhoSend });
    }
    private void ReciveJoinRoomMessage(NetworkConnectionToClient connectionToClient, JoinMessage msy)
    {
        //Debug.LogWarning(msy.RoomName);
        createroomManager.JoinRoom(msy.RoomName, msy.playerName, msy.Whojoin);
    }
    private void ReciveReadyMessage(NetworkConnectionToClient connectionToClient, ReadyMessage msy)
    {
        createroomManager.TellOtherReady(msy.WhoReady, msy.IsReady);
    }
    private void ReciveExitRoomMessage(NetworkConnectionToClient connectionToClient, ExitRoomMessage msy)
    {
        createroomManager.DeletePlayer(msy.WhoExit);
    }
    private void ReciveStartGameMessage(NetworkConnectionToClient connectionToClient, StartGameMessage msy)
    {
        createroomManager.StartGame(msy.whoSend);
    }
    private void ReciveKickMessage(NetworkConnectionToClient connectionToClient, KickPlayerMessage msy)
    {
        CreateCustomRoom room=createroomManager.GetCreateCustomRoomByPlayer(msy.owner);
        if(room==null)
        {
            Debug.LogError("房间不存在");
            return;
        }
        createroomManager.KickPlayer(room, msy.kickName);
    }
    public void CreateDropprocess(NetworkConnectionToClient con)
    {
        CreateCustomRoom room = createroomManager.GetCreateCustomRoomByConnectionID(con);
        if(room==null)
        {
            Debug.LogWarning("房间不存在");
            return;
        }
        RoomPlayerAttribute roomPlayerAttribute = room.GetRoomPlayerAttributeByConnectID(con);
        if (roomPlayerAttribute == null)
        {
            Debug.LogWarning("玩家不存在");
            return;
        }
        createroomManager.DeletePlayer(roomPlayerAttribute.identity);
        if(room.IsStart)
        {
            //ToDo
        }
          
    }
}