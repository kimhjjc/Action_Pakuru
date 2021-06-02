using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGameManager : MonoBehaviour
{
    private static MyGameManager instance = null;
    public float endingTime = 0.0f;

    int _stage = 1;
    public int stage
    {
        get { return _stage; }
        set { _stage = value; }
    }

    public int FinishTime
    {
        get
        {
            if (_stage == 1)
            {
                return 70;
            }
            else if (_stage == 2)
            {
                return 70;
            }
            else if (_stage == 3)
            {
                return 80;
            }
            return 70;
        }
    }

    void Awake()
    {
        stage = 1;
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static MyGameManager P_instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Update()
    {

    }
}
