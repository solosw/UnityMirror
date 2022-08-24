using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
public class CreateCustomRoomPlayer : PlayerBase
{
    private NetworkIdentity identity;
    public CreateCustomRoomPlayer(NetworkIdentity identity)
    {
        this.identity = identity;
    }

    public void RigisterMethondInit()
    {
        NetworkClient.RegisterHandler<IsSuccessCreate>(ReciveSuccessCreateMessage);
        NetworkClient.RegisterHandler<RoomListMessage>(ReciveRoomlistMessage);
        NetworkClient.RegisterHandler<JoinIsSuccessMessage>(ReciveJoinSuccessMessage);
        NetworkClient.RegisterHandler<JoinIsSuccessTellAllMessage>(ReciveOtherJoinMessage);
        NetworkClient.RegisterHandler<TellOtherReadyMessage>(ReciveOtherReadyMessage);
        NetworkClient.RegisterHandler<TellOtherExitroomMessage>(ReciveTellOtherExitMessage);
        NetworkClient.RegisterHandler<LoadSceneByStartGame>(ReciveLoadSceneMessage);
        NetworkClient.RegisterHandler<KickTellotherMessage>(ReciveKickMessage);
    }
   private void ReciveSuccessCreateMessage(IsSuccessCreate msy)
    {   if (msy.WhoseRoom != identity) return;
        if(msy.IsSuccess==false)
        {
            //消息面板....ToDo
            Debug.LogWarning("房间名重复");
            RoomUiManager.Instance.StartShowMessage("房间名重复");
            RoomUiManager.Instance.InputRoomName.text = "";

        }else
        {
            Debug.LogWarning("recive");
            RoomUiManager.Instance.CreateRoomPanel.SetActive(false);
            RoomUiManager.Instance.roomPanel.SetActive(true);
            RoomUiManager.Instance.kickbutton.gameObject.SetActive(true);
            RoomUiManager.Instance.StartButton.gameObject.SetActive(true);
            RoomUiManager.Instance.content.GetComponent<RoomContentmanager>().InitRoomPlayerUi("player" + identity.netId.ToString(), Convert.ToInt32(RoomUiManager.Instance.playerNumber.text));
        }
    }
    private void ReciveRoomlistMessage(RoomListMessage msy)
    {
        if (msy.sendTo != identity) return;
        string[] rooms = msy.Roomlist.Split("/");
        RoomListManager roomListManager = RoomUiManager.Instance.RoomlistContent.GetComponent<RoomListManager>();
        foreach (var item in rooms)
        {   
            if(item.Length!=0)
            {
                string[] roomInf = item.Split(".");
                if(roomInf.Length==3)
                {
                   roomListManager.AddRooms(roomInf[0], roomInf[1], roomInf[2]);
                }
                
            }
        }
    }
    private void ReciveJoinSuccessMessage(JoinIsSuccessMessage mess)
    {
        if (mess.sendTo != identity) return;
        if(!mess.isSuccess)
        {
            Debug.LogWarning(mess.msy);
            RoomUiManager.Instance.StartShowMessage(mess.msy);
            if(RoomUiManager.Instance.SearchPanel.activeSelf)
            {
                RoomUiManager.Instance.InputRoomName.text = "";
            }
            return;
        }
        RoomUiManager.Instance.roomListPanel.SetActive(false);
        RoomUiManager.Instance.roomPanel.SetActive(true);
        RoomUiManager.Instance.content.GetComponent<RoomContentmanager>().JoinInit(mess.msy);
        RoomUiManager.Instance.ReadyButton.gameObject.SetActive(true);
        if(RoomUiManager.Instance.SearchPanel.activeSelf)
        {
            RoomUiManager.Instance.SearchPanel.SetActive(false);
        }
    }
    private void ReciveOtherJoinMessage(JoinIsSuccessTellAllMessage msy)
    {
        if (msy.SendTo != identity) return;
        RoomUiManager.Instance.content.GetComponent<RoomContentmanager>().AddRoomPlayer(msy.joinname);
    }
    private void ReciveOtherReadyMessage(TellOtherReadyMessage msy)
    {
        if (msy.sendTo != identity) return;
        //Debug.LogWarning(msy.ReadyPlayerName);
        RoomUiManager.Instance.content.GetComponent<RoomContentmanager>().ReadyPlayerSet(msy.ReadyPlayerName,msy.isReady);
    }
    private void ReciveTellOtherExitMessage(TellOtherExitroomMessage msy)
    {
        if (msy.sendto != identity) return;
        if (ClientManager.Instance.RoomplayerStatus.IsGamIng) return;
        RoomUiManager.Instance.content.GetComponent<RoomContentmanager>().DeletePlayer(msy.who);

        if(msy.IsBecomeOwner)
        {
            RoomUiManager.Instance.content.GetComponent<RoomContentmanager>().ChangeOwner("player" + identity.netId.ToString());
            RoomUiManager.Instance.StartButton.gameObject.SetActive(true);
            RoomUiManager.Instance.ReadyButton.gameObject.SetActive(false);
            RoomUiManager.Instance.kickbutton.gameObject.SetActive(true);
            RoomUiManager.Instance.cancelReadybutton.gameObject.SetActive(false);
        }
    }
    private void ReciveLoadSceneMessage(LoadSceneByStartGame msy)
    {
        if (msy.WhoLoadScene != identity) return;
        if (msy.isLoadScene)
        {
            RoomUiManager.Instance.InitUiShow();
            ClientManager.Instance.LevelsceneManager.LoadLevelScene();
            ClientManager.Instance.RoomplayerStatus.IsGamIng = true;
            ClientManager.Instance.RoomplayerStatus.roomPlayertype= RoomPlayertype.Create;
        }
        else
        {
            Debug.LogWarning("无法开始");
            RoomUiManager.Instance.StartShowMessage("无法开始游戏");
        }
    }
    private void ReciveKickMessage(KickTellotherMessage msy)
    {
        if (msy.sendto != identity) return;
        if(msy.IsKicked)
        {
            RoomUiManager.Instance.content.GetComponent<RoomContentmanager>().InitAll();
            RoomUiManager.Instance.roomPanel.SetActive(false);
            RoomUiManager.Instance.roomListPanel.SetActive(true);
            RoomUiManager.Instance.StartShowMessage("你已被踢出房间");
        }
        else
        {
            RoomUiManager.Instance.content.GetComponent<RoomContentmanager>().DeletePlayer(msy.kickName);
        }
    }
  
}
