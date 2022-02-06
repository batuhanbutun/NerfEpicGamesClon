using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameStats _gameStats;
    [SerializeField] private Button _restartButton;
    [SerializeField] private int enemyCount;
    void Start()
    {
        _gameStats.isGameOver = false;
        _gameStats.isGameSuccess = false;
    }

    void Update()
    {
        if (_gameStats.isGameOver)
        {
            _restartButton.gameObject.SetActive(true);
        }

        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if (enemyCount == 0 && SceneManager.GetActiveScene().name != "BossScene")
        {
            SceneManager.LoadScene("BossScene");
        }

    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level1");
    }
}
