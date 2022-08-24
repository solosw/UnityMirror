using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateRoomDrop:DropProcessBase
{
    public override void GamingDropProcess<T>(Action<T> GameDropAction, T obj)
    {
        base.GamingDropProcess(GameDropAction, obj);
    }
   
}
