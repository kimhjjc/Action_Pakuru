using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private static Timer instance;
    public static Timer P_instance
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

    public Text TimeCount;
    public float TimeCost = 0.0f;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        CountTime();
    }

    private void CountTime()
    {
        TimeCost += Time.deltaTime;
        TimeCount.text = " : " + TimeCost.ToString("N2");
    }
}
