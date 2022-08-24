using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
public class LevelSceneManager 
{
    private List<string> sceneNames;
    public LevelSceneManager(List<string> scenes)
    {
        this.sceneNames = scenes;
    }
    public void LoadLevelScene(int LevelNumber=0)
    {   
        if(LevelNumber>=sceneNames.Count)
        {
            Debug.LogError("Level is not existed");
            return;
        }
        SceneManager.LoadScene(sceneNames[LevelNumber]);
    }
   public void SendLoadLevelMessage(NetworkIdentity identity,RoomPlayertype playertype, Vector3 position,int LevelNumber)
    {
        //发送关卡跳转消息
    }
    public void SendBackRoomMessage(NetworkIdentity whoBackRoom, RoomPlayertype playertype)
    {
        //todo
    }
}
