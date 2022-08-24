using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
public class UiManagerBase : MonoBehaviour
{
    protected NetworkManager manager;
    protected NetworkIdentity identity;
    public void SearchButton()
    {
        foreach (buttonLabel item in GameObject.FindObjectsOfType<buttonLabel>())
        {
            if (item.isMirrorButton)
            {
                MirrorButtonBase newbutton = buttonFactory.CreateButton(item.type, manager, item.gameObject.GetComponent<Button>());
                ClientManager.Instance.ButtonsManager.InitMainButton(newbutton);
            }else
            {
                
                ButtonBase button = buttonFactory.CreateButton(identity,item.type, item.gameObject.GetComponent<Button>());
                ClientManager.Instance.ButtonsManager.InitMainButton(button);
            }
        }
    }
}
