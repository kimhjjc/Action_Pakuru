using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLinePos : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        transform.position = Vector3.forward * MyGameManager.P_instance.FinishTime * (10 + MyGameManager.P_instance.stage* 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
