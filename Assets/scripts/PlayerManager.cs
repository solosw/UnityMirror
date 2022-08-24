using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
public class PlayerManager : NetworkBehaviour
{
    private bool isInit;
    private MatchRoomPlayer matchroomPlayer;
    private CreateCustomRoomPlayer CustomRoomPlayer;
    private Rigidbody2D rig;
    public override void OnStartLocalPlayer()
    {
        // controller = GetComponent<CharacterController>();
        CustomRoomPlayer = new CreateCustomRoomPlayer(GetComponent<NetworkIdentity>());
        matchroomPlayer = new MatchRoomPlayer(RoomUiManager.Instance.sureMatchButton,RoomUiManager.Instance.CancelButton,RoomUiManager.Instance.MatchText,RoomUiManager.Instance.MatchPanel.GetComponent<Image>(),GetComponent<NetworkIdentity>());
        matchroomPlayer.RigisterMethondInit();
        CustomRoomPlayer.RigisterMethondInit();
        
        
    }
    public override void OnStartClient()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public override void OnStopLocalPlayer()
    {
       
    }
         // Update is called once per frame
    void Update()
    {
        if(ClientManager.Instance.RoomplayerStatus.IsGamIng&& isLocalPlayer)
        {   
            if(!isInit)
            {
                Init();
                isInit = true;
                rig.freezeRotation = true;
            }
            float h = Input.GetAxis("Horizontal");
            rig.velocity = new Vector2(h * 10, 0);
        }
    }
    private void Init()
    {
      rig= this.gameObject.AddComponent<Rigidbody2D>();
    }
}
