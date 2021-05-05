using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highestWaveText;
    public Button startButton;
    public Button startMazeButton;

    private void Start()
    {
        startButton.onClick.AddListener(StartArena);
        startMazeButton.onClick.AddListener(StartMaze);
        int highestWave = PlayerPrefs.GetInt("HighestWave");
        highestWaveText.SetText("Highest Wave: " + highestWave);
    }

    void StartArena()
    {
        SceneManager.LoadScene(1);
    }

    void StartMaze()
    {
        SceneManager.LoadScene(2);
    }
}