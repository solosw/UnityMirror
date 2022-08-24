using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class NetWorkManagerPluss :NetworkManager
{
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        NetworkServer.DestroyPlayerForConnection(conn);
        ServerManager.Instance.MatchDropProcess(conn);
        ServerManager.Instance.CreateDropprocess(conn);
    }
}
