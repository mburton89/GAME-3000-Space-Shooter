using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highestWaveText;
    public Button startButton;
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        int highestWave = PlayerPrefs.GetInt("HighestWave");
        highestWaveText.SetText("Highest Wave: " + highestWave);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
