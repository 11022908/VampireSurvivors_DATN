using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Image fillBar;
   

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        fillBar.fillAmount = (float)currentValue / (float)maxValue;
    }
    public void OnPlayerDeath()
    {
        Destroy(gameObject);
    }
}
