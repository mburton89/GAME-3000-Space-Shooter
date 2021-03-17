using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void HandlePlayerDeath()
    {
        if (EnemyShipSpawner.Instance.currentWave > PlayerPrefs.GetInt("HighestWave"))
        {
            PlayerPrefs.SetInt("HighestWave", EnemyShipSpawner.Instance.currentWave);
        }

        StartCoroutine(EndSession());
    }

    private IEnumerator EndSession()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}
