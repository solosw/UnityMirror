using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
public class ClientManager :MonoBehaviour
{
    [Scene]   
    public List<string> LevelScene = new List<string>();
    private static ClientManager instance;
    public static ClientManager Instance { get => instance;  }
    private ButtonsManager buttonsManager;
    private LevelSceneManager sceneManager;
    private RoomPlayerStatus roomplayerStatus;
    public ButtonsManager ButtonsManager { get => buttonsManager; }
    public LevelSceneManager LevelsceneManager { get => sceneManager;  }
    public RoomPlayerStatus RoomplayerStatus { get => roomplayerStatus;  }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        instance = this;
        buttonsManager = new ButtonsManager();
        if(LevelScene.Count==0||LevelScene==null)
        {
            Debug.LogError("LevelScene is Null");
        }else
        {
            sceneManager = new LevelSceneManager(LevelScene);
            
        }
        roomplayerStatus = new RoomPlayerStatus();  
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
