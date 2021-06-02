using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadNextStageGameScene()
    {
        MyGameManager.P_instance.stage += 1;
        SceneManager.LoadScene("MainScene");
    }

    public void LoadTitleGameScene()
    {
        MyGameManager.P_instance.stage = 1;
        SceneManager.LoadScene("StartScene");
    }

}
