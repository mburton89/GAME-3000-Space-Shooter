using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScreenShaker : MonoBehaviour
{
    public static ScreenShaker Instance;
    public float shakeDuration;
    public float shakeStrength;
    public int shakeVibrato;
    public float shakeRandomness;

    public Transform mainCamera;

    void Awake()
    {
        Instance = this;
    }

    public void ShakeScreen()
    {
        mainCamera.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness, false, true);
    }
}
