using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [Header("HUD")]
    public TextMeshProUGUI scoreText;           // text that displays our score
    public Image healthBarFill;                 // the image fill representing the health bar

    //[Header("Pause Menu")]
    //public GameObject pauseMenu;                // pause menu object

    [Header("End Game Screen")]
    public GameObject endGameScreen;            // end game screen object
    //public TextMeshProUGUI endGameHeaderText;   // end game screen header text
    public TextMeshProUGUI endGameScoreText;    // end game screen displaying our final score

    public static GameUI instance;

    void Awake ()
    {
        instance = this;
    }

    // updates the health bar fill
    public void UpdateHealthBar (int curHp, int maxHp)
    {
        healthBarFill.fillAmount = (float)curHp / (float)maxHp;
    }

    // updates the score text to show the current score
    public void UpdateScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }

    //// enables or disables the pause menu
    //public void TogglePauseMenu (bool paused)
    //{
    //    pauseMenu.SetActive(paused);
    //}

    // activates and sets the end game screen
    public void SetEndGameScreen(int score)
    {
        endGameScreen.SetActive(true);
        //endGameHeaderText.text = won == true ? "You Win" : "You Lose";
        //endGameHeaderText.color = won == true ? Color.green : Color.red;
        endGameScoreText.text = "<b>Score</b>\n" + score;
    }

    //// called when we press the "Resume" button
    //public void OnResumeButton ()
    //{
    //    // GameManager.instance.TogglePauseGame();
    //}

    // called when we press the "Restart" button
    public void OnRestartButton()
    {
        //SceneManager.LoadScene("Game");
        GameManager.instance.restart();
    }

    //// called when we press the "Menu" button
    //public void OnMenuButton ()
    //{
    //    SceneManager.LoadScene("Menu");
    //}
}