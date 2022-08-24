using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public enum ButtonType
{   None,
    StartServer,
    StopServer,
    StartClient,
    StopClient,
    StartMatch,
    StopMatch,
    CreateRoom,
    ExitRoom,
    JoinRoom,
    SearchRoom,
    SureMatch,
    CancelMatch,
    JoinRoomByName,
    FreshRoomList,
    KickPlayer,
    StartGame,
    Ready,
    CancelReady,
}
public enum MatchsStatus
{  
    None,
    MatchIng,
    MatchFinished,
    Gaming,
}
public enum RoomPlayertype
{
    None,
    Match,
    Create,
}

