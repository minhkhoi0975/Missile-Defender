using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Update the timer.
        Text txtTimer = GameObject.Find("Timer").GetComponent<Text>();
        switch(EnemySpawnerScript.enemySpawnerState)
        {
            case EnemySpawnerState.SPAWN:
                txtTimer.color = Color.white;
                break;
            case EnemySpawnerState.NOSPAWN:
                txtTimer.color = Color.yellow;
                break;
            case EnemySpawnerState.TRANSITION:
                txtTimer.color = Color.green;
                break;
        }
        txtTimer.text = string.Format("Wave {0}: {1:0.0}s", EnemySpawnerScript.currentWave, EnemySpawnerScript.timer);

        // Update the number of lives.
        Text txtLives = GameObject.Find("NumberOfLives").GetComponent<Text>();
        txtLives.text = "Lives: " + GameManagerScript.Instance.numberOfLives;

        // Update the score.
        Text txtScore = GameObject.Find("Score").GetComponent<Text>();
        txtScore.text = "Score: " + GameManagerScript.Instance.score;

        // Show the "Game Over!" text if the number of lives is 0.
        if(GameManagerScript.Instance.numberOfLives == 0)
        {
            Text txtGameOver = GameObject.Find("GameOver").GetComponent<Text>();
            txtGameOver.text = "Game Over!";
        }
    }
}
