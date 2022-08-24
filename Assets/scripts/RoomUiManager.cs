using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;
public class RoomUiManager : NetworkBehaviour
{
    private static RoomUiManager instance;
    public static RoomUiManager Instance { get=>instance; }
    protected NetworkManager manager;
    protected NetworkIdentity identity;
    
    public GameObject AllNetWorkbuttonParent;
    [Header("MatchCustomUI")]
    public GameObject MatchPanel;
    public Button sureMatchButton, CancelButton;
    public Text MatchText;
    [Header("CreateRoomCustomUI")]
    public GameObject CreateRoomPanel;
    public InputField InputRoomName;
    public Text playerNumber;
    public Button sureCreatebutton, CancelCreatebutton;
    [Header("RoomPanelUI")]
    public GameObject roomPanel;
    public GameObject content;
    public GameObject RoomPlayerPrefabsUI;
    public Button StartButton, ExitButton, ReadyButton, kickbutton,cancelReadybutton;
    [Header("RoomListUi")]
    public GameObject roomListPanel;
    public GameObject RoomlistContent;
    public GameObject RoomPrefbsUi;
    public Button RoomBackbutton,JoinRoomButton;
    [Header("SearchPanel")]
    public GameObject SearchPanel;
    public InputField SearchRoomNameInput;
    [Header("MessagePanel")]
    public GameObject MessagePanel;
    public Text MessageText;
    public override void OnStartClient()
    {
        Init();
    }
   
    void Init()
    {
        instance = this;
        manager = GameObject.FindObjectOfType<NetworkManager>();  
        foreach (buttonLabel item in GameObject.FindObjectsOfType<buttonLabel>())
        {
            if (item.isMirrorButton)
            {
                MirrorButtonBase newbutton = buttonFactory.CreateButton(item.type, manager, item.gameObject.GetComponent<Button>());
                ClientManager.Instance.ButtonsManager.InitMainButton(newbutton);
            }
            else
            {
                //Debug.LogWarning(item.type);
                ButtonBase button = buttonFactory.CreateButton(identity, item.type, item.gameObject.GetComponent<Button>());
                ClientManager.Instance.ButtonsManager.InitMainButton(button);
            }
        }
        
    } 
    public void SendAllowMatchMessage()
    {
        if(identity==null)
        {
            foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (item.GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    identity = item.GetComponent<NetworkIdentity>();
                }
            }
        }
        NetworkClient.Send<AllowMatchMessage>(new AllowMatchMessage { WhoAllow = identity });
        sureMatchButton.interactable = false;
        MatchText.text = "等待中...";
    }
    public void SendCancelMatchMessage()
    {
        if (identity == null)
        {
            foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (item.GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    identity = item.GetComponent<NetworkIdentity>();
                }
            }
        }
        NetworkClient.Send<CancelMatchMessage>(new CancelMatchMessage() { WhoCancelMatch = identity });
        InitUiShow();
    }
    public  void InitUiShow()
    {
        sureMatchButton.interactable = false;
        CancelButton.interactable = true;
        MatchText.text = "匹配中";
        MatchPanel.SetActive(false);
        AllNetWorkbuttonParent.SetActive(true);
        SearchPanel.SetActive(false);
        ReadyButton.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(false);
        kickbutton.gameObject.SetActive(false);
        cancelReadybutton.gameObject.SetActive(false);
        CreateRoomPanel.SetActive(false);
        roomListPanel.SetActive(false);
        roomPanel.SetActive(false);
        MessagePanel.SetActive(false);
    }
    //public void ReciveMatchTimeOut()
    //{
    //    InitUiShow();
        
    //}
    public void CreateRoomCancel()
    {
        AllNetWorkbuttonParent.SetActive(true);
        InputRoomName.text = "";
        CreateRoomPanel.SetActive(false);
    }
    public void SureCreateRoom()
    {
        if (identity == null)
        {
            foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (item.GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    identity = item.GetComponent<NetworkIdentity>();
                }
            }
        }
        string RoomName = InputRoomName.text;
        if(RoomName.Length==0||RoomName.Length>15)
        {
            Debug.LogError("名称过长");
            return;
        }
        int Number = Convert.ToInt32(playerNumber.text);
        string playerName = "player" + identity.netId.ToString();
        NetworkClient.Send<CreateRoomMessage>(new CreateRoomMessage()
        {
            RoomOwnerName = playerName,
            WhoseRoom = identity,
            PlayerNumber = Number,
            RoomName = RoomName
        }); 

    }
    public void ExitRoomList()
    {
        roomListPanel.SetActive(false);
        
        AllNetWorkbuttonParent.SetActive(true);
    }
    public void Join()
    {
        if (identity == null)
        {
            foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (item.GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    identity = item.GetComponent<NetworkIdentity>();
                }
            }
        }
        string roomName = RoomUiManager.instance.RoomlistContent.GetComponent<RoomListManager>().GetChoosenRoomName();
        if (roomName.Length == 0 || roomName == null)
        {
            StartShowMessage("请选择房间");
            return;
        }
        //Debug.LogWarning(roomName);
        NetworkClient.Send<JoinMessage>(new JoinMessage() { Whojoin = identity, RoomName = roomName,playerName="player"+identity.netId.ToString() });
    }
    public void RefreshRoomList()
    {
        if (identity == null)
        {
            foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (item.GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    identity = item.GetComponent<NetworkIdentity>();
                }
            }
        }
       // Debug.LogWarning("Send");
        NetworkClient.Send<SearchRequstMessage>(new SearchRequstMessage() { WhoSend = identity });
    }
    public void JoinRoomByName()
    {
        if (identity == null)
        {
            foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (item.GetComponent<NetworkIdentity>().isLocalPlayer)
                {
                    identity = item.GetComponent<NetworkIdentity>();
                }
            }
        }
        string roomName = SearchRoomNameInput.text;
        if (roomName.Length == 0 || roomName == null) return;
        NetworkClient.Send<JoinMessage>(new JoinMessage() { playerName = "player" + identity.netId.ToString() ,RoomName=roomName,Whojoin=identity});
        //SearchPanel.SetActive(false);
        //StartCoroutine(JoinByNameWaitTime(3));
    }
    public void Ready()
    {
        //Debug.LogWarning(identity.netId);
        cancelReadybutton.gameObject.SetActive(true);
        ReadyButton.gameObject.SetActive(false);
        NetworkClient.Send<ReadyMessage>(new ReadyMessage() { IsReady = true, WhoReady = identity });
    }
    public void Searchback()
    {
        SearchPanel.SetActive(false);
        AllNetWorkbuttonParent.SetActive(true);
    }
    public void ExitRoom()
    {
        content.GetComponent<RoomContentmanager>().DeletePlayer("player" + identity.netId.ToString());
        ReadyButton.gameObject.SetActive(false);
        StartButton.gameObject.SetActive(false);
        kickbutton.gameObject.SetActive(false);
        cancelReadybutton.gameObject.SetActive(false);
        roomPanel.SetActive(false);
        NetworkClient.Send<ExitRoomMessage>(new ExitRoomMessage() { WhoExit = identity });
        AllNetWorkbuttonParent.SetActive(true);
    }
    public void StartGame()
    {
        NetworkClient.Send<StartGameMessage>(new StartGameMessage() { whoSend = identity });
    }
    public void KickPlayer()
    {
        string kickName = content.GetComponent<RoomContentmanager>().GetChoosenPlayerName();
        if (kickName.Length == 0 || kickName == null)
        {
            Debug.LogError("没有此玩家");
            return;
        }
        NetworkClient.Send<KickPlayerMessage>(new KickPlayerMessage() { kickName = kickName, owner = identity });

    }
    //IEnumerator JoinByNameWaitTime(float MaxWaitTime)
    //{
    //    float waitTime = 0;
    //    while(waitTime<MaxWaitTime)
    //    {
    //        yield return new WaitForSeconds(1);
    //        waitTime += 1;
    //    }
    //    if(!SearchPanel.activeSelf&&!roomPanel.activeSelf)
    //    {
    //        SearchPanel.SetActive(true);
    //    }
    //}
    public void CancelReady()
    {
        ReadyButton.gameObject.SetActive(true);
        cancelReadybutton.gameObject.SetActive(false);
        NetworkClient.Send<ReadyMessage>(new ReadyMessage() { IsReady = false, WhoReady = identity });
    }
    IEnumerator ShowMessage(string message,float WaitTime)
    {
        float time=0;
        MessagePanel.SetActive(true);
        MessageText.text = message;
        while(time<WaitTime)
        {
            yield return new WaitForSeconds(1);
            time += 1;
        }
        MessageText.text = "";
        MessagePanel.SetActive(false);
    }
    public void StartShowMessage(string message, float WaitTime=1f)
    {
        if (MessagePanel.activeSelf) return;
        MessagePanel.transform.SetAsLastSibling();
        StartCoroutine(ShowMessage(message, WaitTime));
    }
}
