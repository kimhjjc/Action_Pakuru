using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MySlider : MonoBehaviour
{
    public Slider PlayerSlider;
    public Slider EnemySlider;

    public Transform FinishLine;

    void Start()
    {
        
    }

    void Update()
    {
        PlayerSlider.maxValue = FinishLine.position.z;
        PlayerSlider.value = Player.P_instance.transform.position.z;

        EnemySlider.maxValue = FinishLine.position.z;
        EnemySlider.value = Enemy.P_instance.transform.position.z;
    }
}
