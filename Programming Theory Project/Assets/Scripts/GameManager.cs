using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void updateScore(int scoreChange)
    {
        score += scoreChange;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        GameObject.Find("EnemyContainer").GetComponent<EnemyContainerBehavior>().StopMoving();
    }
}
