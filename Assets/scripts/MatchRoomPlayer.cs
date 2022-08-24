using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
public class MatchRoomPlayer : PlayerBase
{   
    private Button MatchSure;
    private Button MatchCancel;
    private Text MatchText;
    private Image backGround;
    private NetworkIdentity identity;
    public MatchsStatus matchsStatus=MatchsStatus.None;
    public MatchRoomPlayer(Button matchSure, Button matchCancel, Text matchText, Image backGround, NetworkIdentity identity)
    {
        MatchSure = matchSure;
        MatchCancel = matchCancel;
        MatchText = matchText;
        this.backGround = backGround;
        this.identity = identity;
    }

    private void ReciveMatchSuccess(MatchSuccessMessage msy)
    {
        if (msy.WhoMatchSuccess != identity) return;
        if(msy.isMatchSuccess)
        {   
            MatchText.text = "匹配成功";
            MatchSure.interactable=true;
            MatchCancel.interactable = false;
        }else
        {
            RoomUiManager.Instance.StartShowMessage("匹配失败");
        }
    }
    public void RigisterMethondInit()
    {
        NetworkClient.RegisterHandler<MatchSuccessMessage>(ReciveMatchSuccess);
        NetworkClient.RegisterHandler<LoadGameSceneMessage>(ReciveLoadScene);
        NetworkClient.RegisterHandler<MatchTimeOutMessage>(ReciveMatchTimeOut);
    }
    private void ReciveLoadScene(LoadGameSceneMessage msy)
    {
        if (msy.WhoLoadScene != identity) return;
        if(msy.isLoadScene)
        {
            ClientManager.Instance.LevelsceneManager.LoadLevelScene();
            ClientManager.Instance.RoomplayerStatus.IsGamIng = true;
        }else
        {
            RoomUiManager.Instance.StartShowMessage("游戏无法开始");
        }
        
    }//共有
    private void ReciveMatchTimeOut(MatchTimeOutMessage msy)
    {
        if (msy.WhoMatch != identity) return;
        RoomUiManager.Instance.InitUiShow();
        RoomUiManager.Instance.StartShowMessage(msy.reason);
    }
    
}
