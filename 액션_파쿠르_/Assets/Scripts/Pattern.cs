using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pattern : MonoBehaviour
{
    [SerializeField]
    private float destroyTime = 60.0f;

    void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }


    void Update()
    {
        
    }
}
