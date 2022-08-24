using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class DropProcessBase 
{
   public virtual void GamingDropProcess<T>(Action<T> GameDropAction,T obj) 
    {
        GameDropAction(obj);
    }
    
}
