using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeetleManager : MonoBehaviour
{
    public int beetleHealth;
    public int beetleGauge;
    public Text goalDistanceText;
    public GameObject goal;
    public bool powerUp1;
    public bool powerUp2;
    public bool powerUp3;

    public void MeasureDistance()
    {
        float distance = Vector3.Distance(goal.transform.position, transform.position);
        goalDistanceText.text = (int)distance + " m";
    }

    public void Update()
    {
        MeasureDistance();
    }
}
