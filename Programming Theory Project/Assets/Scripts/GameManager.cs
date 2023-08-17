using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject youWinUI;
    public bool gameOver = false;
    private int enemyCount;

    private void Start()
    {
        enemyCount = GameObject.FindObjectsOfType<BasicEnemy>().Length;
    }

    public void updateScore(int scoreChange)
    {
        score += scoreChange;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        GameObject.Find("EnemyContainer").GetComponent<EnemyContainerBehavior>().StopMoving();
        gameOver = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void Win()
    {
        GameObject.Find("EnemyContainer").GetComponent<EnemyContainerBehavior>().StopMoving();
        GameObject[] enemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach(GameObject bullet in enemyBullets)
        {
            Destroy(bullet);
        }
        youWinUI.SetActive(true);
        gameOver = true;
    }

    public void DeductEnemyCount()
    {
        enemyCount--;
        if(enemyCount == 0)
        {
            Win();
        }
    }
}
