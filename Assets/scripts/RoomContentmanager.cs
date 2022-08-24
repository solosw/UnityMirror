using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContentmanager : MonoBehaviour
{
    List<GameObject> RoomPlayersUi = new List<GameObject>();
    
    public void InitRoomPlayerUi(string name,int number)
    {   
            for (int i = 0; i < number; i++)
            {
                GameObject go = GameObject.Instantiate<GameObject>(RoomUiManager.Instance.RoomPlayerPrefabsUI);
                go.transform.SetParent(this.gameObject.transform);
                go.SetActive(false);
                RoomPlayersUi.Add(go);
            }
        RoomPlayersUi[0].SetActive(true);
        RoomPlayersUi[0].GetComponent<RoomPlayerManager>().SetSelf(true, name);
    }
    public void AddRoomPlayer(string name)
    {
        foreach (var item in RoomPlayersUi)
        {
            if(!item.activeSelf)
            {
                item.SetActive(true);
                item.GetComponent<RoomPlayerManager>().SetSelf(false, name);
                return;
            }
        }
    }
    private void InitAllPlayerprebs()
    {
        foreach (var item in RoomPlayersUi)
        {
            if (item.activeSelf)
            {
                item.SetActive(false);
                
            }
        }
    }
    public void JoinInit(string allName)
    {
        InitAllPlayerprebs();
        int number = allName.Length - 1;
        if (RoomPlayersUi.Count<number)
        {
            for(int i=0;i<number-RoomPlayersUi.Count;i++)
            {
                GameObject go = GameObject.Instantiate<GameObject>(RoomUiManager.Instance.RoomPlayerPrefabsUI);
                go.transform.SetParent(this.gameObject.transform);
                go.SetActive(false);
                RoomPlayersUi.Add(go);
            }
        }
        string[] AllPlayers = allName.Split("/");
        for (int i = 0; i < AllPlayers.Length - 1; i++)
        {
            if(i==0)
            {
                RoomPlayersUi[i].SetActive(true);
                RoomPlayersUi[i].GetComponent<RoomPlayerManager>().SetSelf(true, AllPlayers[i]);
            }else
            {
                RoomPlayersUi[i].SetActive(true);
                RoomPlayersUi[i].GetComponent<RoomPlayerManager>().SetSelf(false, AllPlayers[i]);
            }
        }
    }
    public void ReadyPlayerSet(string playerName,bool isReady)
    {
        foreach (var item in RoomPlayersUi)
        {   RoomPlayerManager roomPlayer = item.GetComponent<RoomPlayerManager>();
            if (roomPlayer.Playername==playerName)
            {
                roomPlayer.isReady = isReady;
                if(isReady)
                    roomPlayer.ChangeColor(Color.green);
                else
                {
                    roomPlayer.ChangeColor(roomPlayer.InitColor);
                }
                break;
            }
        }
    }
    public void DeletePlayer(string name)
    {
        foreach (var item in RoomPlayersUi)
        {
            RoomPlayerManager roomPlayerUi = item.GetComponent<RoomPlayerManager>();
            if (roomPlayerUi.Playername==name)
            {
                roomPlayerUi.ChangeColor(roomPlayerUi.InitColor);
                item.SetActive(false);
            }
        }
    }
    public void ChangeOwner(string playerName)
    {
        foreach (var item in RoomPlayersUi)
        {
            if (item.GetComponent<RoomPlayerManager>().Playername == playerName)
            {
                item.GetComponent<RoomPlayerManager>().becomeOwner();
            }
        }
    }
    public  void InitByChoose(RoomPlayerManager roomPlayer)
    {
        foreach (var item in RoomPlayersUi)
        {
            RoomPlayerManager roomPlayerUi = item.GetComponent<RoomPlayerManager>();
            if (roomPlayerUi .isChosen && roomPlayer != roomPlayerUi)
            {   
                if(!roomPlayerUi.IsOwner)
                {
                    roomPlayerUi.isChosen = false;
                    if(roomPlayerUi.isReady)
                    {
                        roomPlayerUi.ChangeColor(roomPlayerUi.Readycolor);
                    }else
                    {
                        roomPlayerUi.ChangeColor(roomPlayerUi.InitColor);
                    }
                }
            }
        }
    }
    public string GetChoosenPlayerName()
    {
        foreach (var item in RoomPlayersUi)
        {
            RoomPlayerManager roomPlayerUi = item.GetComponent<RoomPlayerManager>();
            if (roomPlayerUi.isChosen)
            {
                return roomPlayerUi.Playername;
            }
        }
        return "";
    }
    public void InitAll()
    {
        foreach (var item in RoomPlayersUi)
        {
            RoomPlayerManager roomPlayerUi = item.GetComponent<RoomPlayerManager>();
            roomPlayerUi.isChosen = false;
            roomPlayerUi.ChangeColor(roomPlayerUi.InitColor);
            item.SetActive(false);
        }
    }
}
