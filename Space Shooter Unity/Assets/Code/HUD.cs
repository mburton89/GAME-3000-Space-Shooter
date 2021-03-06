﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD Instance;

    public Image healthBar;
    public TextMeshProUGUI waveText;

    void Awake()
    {
        Instance = this;
    }

    public void UpdateHealthBar(int currentArmor, int maxArmor)
    {
        float healthAmount = (float)currentArmor / (float)maxArmor;
        healthBar.fillAmount = healthAmount;
    }

    public void UpdateWaveText(int currentWave)
    {
        waveText.SetText("Wave: " + currentWave.ToString());
    }
}
