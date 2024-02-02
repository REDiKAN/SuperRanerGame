using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlPanel : MonoBehaviour
{
    [Header("Info Player Statistic")]
    public float topTime;

    [Header("Button")]
    public GameObject startButton;
    public GameObject destroyButton;

    public void SaveStatistic(float time)
    {
        topTime = time;
        Debug.Log("new Time");
    }

    public void DestoyStatic()
    {
        topTime = 0f;
        Debug.Log("Destoy Time");
    }
}
