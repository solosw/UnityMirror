using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class MoneyManager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag=="Player")
        {
            Destroy(gameObject);
        }
    }
}
