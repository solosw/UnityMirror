using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class buttonLabelBase : MonoBehaviour
{
    public ButtonType type;
    public bool isMirrorButton;
    public virtual void ButtonEvent()
    {
        //其他自定义按钮
    }
}
