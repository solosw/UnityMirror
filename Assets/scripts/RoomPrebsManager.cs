using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoomPrebsManager : MonoBehaviour
{   
    private string roomName;
    private string currentplayerNum;
    private string allNum;
    public Text tags;
    public Text nameText;
    public Image thisprebs;
    public bool isChosen;
    public string RoomName { get => roomName;  }
    public void SetSelf(string roomname,string currentnum,string allnum)
    {
        this. roomName = roomname;
        currentplayerNum = currentnum;
        this.allNum = allnum;
        tags.text = currentnum + "/" + allnum;
        nameText.text = roomName;
    }
    public void ChooseRoom()
    {
        Init();
        isChosen = !isChosen;
        if (isChosen)
        {
            thisprebs.color = Color.blue;
        }
        else thisprebs.color = Color.white;
    }
    private void Init()
    {
        RoomListManager roomListManager = transform.parent.GetComponent<RoomListManager>();
        roomListManager.InitAllPrefabs(this);
    }
}
