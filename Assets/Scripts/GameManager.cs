using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public Button changeDifficultyButton;
    public Button startButton;
    public Image menuBackground;
    public GameObject playerPrefab;
    public GameObject background;
    public GameObject difficultyScreen;
    public GameObject timerScore;
    public GameObject countScore;
    public SpawnManager spawnManagerScript;

    public bool isGameActive;
    public float gameSpeed = 5.0f;

    private int score;

    [SerializeField] private int gameDifficulty;

    //set game difficulty trugh buttons // change for get/set method if have time
    public void SetDifficulty(int difficulty)
    {
        gameDifficulty = difficulty;
    }


    public void StartGame()
    {
        if (gameDifficulty > 1)
        {
            gameSpeed *= gameDifficulty;
        }

        score = GameData.Instance.countNumber;
        scoreText.text = "Попытка N: " + score;

        startButton.gameObject.SetActive(false);
        difficultyScreen.gameObject.SetActive(false);
        Instantiate(playerPrefab, playerPrefab.transform.position, Quaternion.identity);
        background.gameObject.SetActive(true);
        menuBackground.gameObject.SetActive(false);
        isGameActive = true;
        spawnManagerScript.StartSpawning();

        Timer.Instance.BeginTimer();
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        changeDifficultyButton.gameObject.SetActive(true);
        menuBackground.gameObject.SetActive(true);
        Timer.Instance.EndTimer();
        timerScore.gameObject.SetActive(true);
        countScore.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
