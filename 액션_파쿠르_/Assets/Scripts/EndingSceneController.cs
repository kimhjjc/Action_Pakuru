using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingSceneController : MonoBehaviour
{
    public Text Title;
    public Text TimeCount;

    public GameObject NextStage;
    public GameObject GoTitle;


    void Start()
    {
        if(MyGameManager.P_instance.stage != 3)
        {
            NextStage.SetActive(true);
            GoTitle.SetActive(false);
        }
        else
        {
            NextStage.SetActive(false);
            GoTitle.SetActive(true);
        }

        Title.text = "Stage " + MyGameManager.P_instance.stage.ToString() + " Clear!";

        float TimeCost = MyGameManager.P_instance.endingTime;
        TimeCount.text = " : " + TimeCost.ToString("N2");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
