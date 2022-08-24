using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoomPlayerManager : MonoBehaviour
{
    public Image image;
    public Text text;
    private string playername;
    private bool isOwner;
    public bool isChosen;
    public bool isReady { get; set; }
    public string Playername { get => playername;  }
    public bool IsOwner { get => isOwner;  }
    Color choosenColor = new Color(0, 0, 1, 0.5f);
    Color readycolor =new Color(0, 1, 0, 0.5f);
    Color initColor =new Color(0,0,0,0.5f);
    Color ownerColor = new Color(1, 0.92f, 0.016f, 0.5f);
    public Color ChoosenColor { get=>choosenColor;  }
    public Color Readycolor { get=>readycolor;  }
    public Color InitColor { get=>initColor;  }
    public Color OwnerColor { get=>ownerColor;  }
    private void SetColor(bool isOnwer)
    {
       
        if (isOnwer)
        {
            
            image.color = OwnerColor;
            
        }else
        {
            image.color = InitColor;
           
        }
        this.isOwner = isOnwer;
    }
    private void SetName(string name)
    {
        text.text = name;
        playername = name;
    }
    public void SetSelf(bool isOnwer,string name)
    {
        SetColor(isOnwer);
        SetName(name);
    }
    public void ChangeColor(Color color)
    { 
        image.color =color;
    }
    public void becomeOwner()
    {
        image.color = OwnerColor;

        isOwner = true;
    }
    public void ChoosePlayer()
    {   if (isOwner) return;
        Init();
        isChosen = !isChosen;
        if (isChosen)
        {
            image.color = ChoosenColor; 
        }
        else
        {
            if(isReady)
            {
                image.color = Readycolor;
            }else
            {
                image.color = InitColor;
            }
        } 
    }
    private void Init()
    {
        RoomContentmanager roomContentmanager = transform.parent.GetComponent<RoomContentmanager>();
        roomContentmanager.InitByChoose(this);
    }
}
