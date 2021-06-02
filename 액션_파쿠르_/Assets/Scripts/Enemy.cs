using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    private static Enemy instance;
    public static Enemy P_instance
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

    public Transform FinishLine;

    [SerializeField]
    private float goalTime = 90.0f;

    void Start()
    {
        instance = this;

        goalTime = MyGameManager.P_instance.FinishTime;
    }

    private void FixedUpdate()
    {
        MovePosition();
    }
    void Update()
    {
    }

    private void MovePosition()
    {
        Vector3 myPosition = this.transform.position;

        myPosition.z = FinishLine.position.z * Timer.P_instance.TimeCost / goalTime;
        this.transform.position = myPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLine")
        {
            SceneManager.LoadScene("StageFail");
        }
    }
}
