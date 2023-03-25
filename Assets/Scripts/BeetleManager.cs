using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BeetleManager : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public int beetleGauge;
    public Text goalDistanceText;
    public GameObject goal;

    public bool powerUp1;
    public bool powerUp2;
    public bool powerUp3;

    CharacterController _charC;

    public void MeasureDistance()
    {
        float distance = Vector3.Distance(goal.transform.position, transform.position);
        goalDistanceText.text = (int)distance + " m";
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enter");
            currentHealth -= 1;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Start()
    {
        _charC = GetComponent<CharacterController>();
        currentHealth = maxHealth;
    }

    public void Update()
    {
        MeasureDistance();
    }

}
