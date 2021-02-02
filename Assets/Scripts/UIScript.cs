using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Update the number of lives.
        Text txtLives = GameObject.Find("NumberOfLives").GetComponent<Text>();
        txtLives.text = "Lives: " + PlayerScript.numberOfLives;

        // Update the score.
        Text txtScore = GameObject.Find("Score").GetComponent<Text>();
        txtScore.text = "Score: " + PlayerScript.score;

        // Show the "Game Over!" text if the number of lives is 0.
        if(PlayerScript.numberOfLives == 0)
        {
            Text txtGameOver = GameObject.Find("GameOver").GetComponent<Text>();
            txtGameOver.text = "Game Over!";
        }
    }
}
