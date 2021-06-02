using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Player.P_instance._realSpeed *= 1.1f;
            Debug.Log("Speed UP");
        }
    }
}
