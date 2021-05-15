using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD Instance;

    public Image healthBar;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI limiterText;
    public TextMeshProUGUI specialText;

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

    public void UpdateAmmoCountText(int currentAmmo, int maxAmmo)
    {
        if (limiterText != null)
        {
            limiterText.SetText("Projectiles Fired: " + currentAmmo + "/" + maxAmmo);
        }
    }
}
