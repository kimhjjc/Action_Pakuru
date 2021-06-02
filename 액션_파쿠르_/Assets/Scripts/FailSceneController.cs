using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailSceneController : MonoBehaviour
{
    public Text Title;

    void Start()
    {
        Title.text = "Stage " + MyGameManager.P_instance.stage.ToString() + " Fail!";
    }

    // Update is called once per frame
    void Update()
    {

    }
}
