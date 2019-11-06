using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GamePlayController : MonoBehaviour
{
    public static GamePlayController instance;

    [SerializeField]
    private Button instructionButton;

    [SerializeField]
    private Text scoreText, endScoreText, bestScoreText;

    [SerializeField]
    private GameObject gameOverPanel, pausePanel;

    private void Awake()
    {
        Time.timeScale = 0; // đóng băng  tiến trình khi = 0
        _MakeInstance();
    }

    void _MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void _InstructionButton()
    {
        Time.timeScale = 1; // tiếp tục tiến trình
        instructionButton.gameObject.SetActive(false); // setActive dùng ẩn đi instructionButton
    }

    public void _SetScore(int score)
    {
        scoreText.text = "" + score;
    }

    // hiển thị panel over gồm end_Score và best_Score
    public void _BirdDiedShowPanel(int score)
    {
        gameOverPanel.SetActive(true);

        endScoreText.text = "" + score;
        if( score > GameManage.instance._GetHighScore())
        {
            GameManage.instance._SetHighScore(score);
        }

        bestScoreText.text = "" + GameManage.instance._GetHighScore();
    }

    public void _MenuButton()
    {
        Application.LoadLevel("MainMenu"); // quay về màn hình MainMenu
        
    }

    public void _RestartGameButton()
    {
        Application.LoadLevel(Application.loadedLevel);// quay về màn hình PlayScreen
    }

    public void _PauseButton()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void _ResumeButton()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}

