using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class UiManager : UiManagerBase
{   
    void Start()
    {
        manager = GameObject.FindObjectOfType<NetworkManager>();
        SearchButton();
    }
}
