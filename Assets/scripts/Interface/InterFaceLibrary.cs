using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public interface SingleMessage
{
    NetworkIdentity WhoSend { get; set; }
    NetworkIdentity SendTo { get; set; }
}
public interface ICreateRoomAttribute
{
    string OwnerName { get; set; }
    int PlayerNumber { get; set; }
    string RoomName { get; set; }
}
public interface IPlayerAttribute
{
    NetworkIdentity identity { get; set; }
    string PlayerName { get; set; }
    bool isOwner { get; set; }
}