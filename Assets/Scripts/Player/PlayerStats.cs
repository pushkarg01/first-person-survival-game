using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Image healthStats, staminaStats;

    public void DisplayHealthStats(float healthVal)
    {
        healthVal /= 100f;
        healthStats.fillAmount=healthVal;
    }

    public void DisplayStaminaStats(float staminaVal)
    {
        staminaVal /= 100f;
        staminaStats.fillAmount = staminaVal;
    }
}
