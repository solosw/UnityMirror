using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public struct MatchMesssage : NetworkMessage
{
    public NetworkIdentity WhoMatch;
}
public struct MatchSuccessMessage:NetworkMessage
{
    public NetworkIdentity WhoMatchSuccess;
    public bool isMatchSuccess;
}
public struct AllowMatchMessage:NetworkMessage
{
    public NetworkIdentity WhoAllow;
}
public struct LoadGameSceneMessage:NetworkMessage
{
    public bool isLoadScene;
    public  NetworkIdentity WhoLoadScene;
}
public struct CancelMatchMessage:NetworkMessage
{
    public NetworkIdentity WhoCancelMatch;
}
public struct MatchTimeOutMessage:NetworkMessage
{
    public NetworkIdentity WhoMatch;
    public string reason;
}
public struct MatchDropMessage:NetworkMessage
{
    public NetworkIdentity WhoMatchDrop;
    public MatchsStatus status;
}
public struct CreateRoomMessage:NetworkMessage
{
    public string RoomOwnerName;
    public string RoomName;
    public NetworkIdentity WhoseRoom;
    public int PlayerNumber;

}
public struct IsSuccessCreate:NetworkMessage
{
    public NetworkIdentity WhoseRoom;
    public bool IsSuccess;
}
public struct SearchRequstMessage:NetworkMessage
{
    public NetworkIdentity WhoSend;
    
}
public struct RoomListMessage:NetworkMessage
{
    public NetworkIdentity sendTo;
    public string Roomlist;
}
public struct JoinMessage:NetworkMessage
{
    public NetworkIdentity Whojoin;
    public string RoomName;
    public string playerName;
}
public struct JoinIsSuccessMessage: NetworkMessage
{
    public bool isSuccess;
    public string msy;
    public NetworkIdentity sendTo;
}
public struct JoinIsSuccessTellAllMessage : NetworkMessage
{
    public string joinname;
    public NetworkIdentity SendTo;
}
public struct ReadyMessage : NetworkMessage
{
    public bool IsReady;
    public NetworkIdentity WhoReady;
}
public struct TellOtherReadyMessage:NetworkMessage
{
    public string ReadyPlayerName;
    public NetworkIdentity sendTo;
    public bool isReady;
}
public struct ExitRoomMessage:NetworkMessage
{
    public NetworkIdentity WhoExit;
}
public struct TellOtherExitroomMessage:NetworkMessage
{
    public NetworkIdentity sendto;
    public string who;
    public bool IsBecomeOwner;
}
public struct StartGameMessage : NetworkMessage
{
    public NetworkIdentity whoSend;
}
public struct LoadSceneByStartGame: NetworkMessage
{
    public bool isLoadScene;
    public NetworkIdentity WhoLoadScene;
}
public struct KickPlayerMessage:NetworkMessage
{
    public string kickName;
    public NetworkIdentity owner;
}
public struct KickTellotherMessage:NetworkMessage
{
    public NetworkIdentity sendto;
    public string kickName;
    public bool IsKicked;
}