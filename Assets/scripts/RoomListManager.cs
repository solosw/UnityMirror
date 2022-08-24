using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListManager : MonoBehaviour
{
    List<RoomPrebsManager> allrooms = new List<RoomPrebsManager>();
    public void AddRooms(string roomName,string currentnumber,string playernumber)
    {
        RoomPrebsManager go = GetRoombyName(roomName);
        if(go!=null)
        {
            go.GetComponent<RoomPrebsManager>().SetSelf(roomName, currentnumber, playernumber);
            return;
        }
        GameObject room = GameObject.Instantiate<GameObject>(RoomUiManager.Instance.RoomPrefbsUi);
        room.transform.SetParent(RoomUiManager.Instance.RoomlistContent.transform);
        room.GetComponent<RoomPrebsManager>().SetSelf(roomName, currentnumber, playernumber);
        allrooms.Add(room.GetComponent<RoomPrebsManager>());
    }
    private RoomPrebsManager GetRoombyName(string roomName)
    {
        foreach (var item in allrooms)
        {
            if (item.RoomName == roomName) return item;
        }
        return null;
    }
    public string GetChoosenRoomName()
    {   
        foreach (var item in allrooms)
        {
            if (item.isChosen) return item.RoomName;
        }
        return "";
    }
    public void InitAllPrefabs(RoomPrebsManager roomPrebs)
    {
        foreach (var item in allrooms)
        {
            if (item.isChosen&&roomPrebs!=item) 
            {
                item.isChosen = false;
                item.thisprebs.color = Color.white;
            }
        }
    }
}
