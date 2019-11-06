using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    public static GameManage instance;

    private const string HIGH_SCORE = "High score";


    void Awake()
    {
        _MakeSingleInstance();
        _IsGameStartTheFirstTime();
    }

    void _IsGameStartTheFirstTime()
    {
        if (!PlayerPrefs.HasKey("_IsGameStartTheFirstTime")) // nếu tải về lần đầu, set key = true, reset high score = 0
        {
            PlayerPrefs.SetInt(HIGH_SCORE, 0); 
            PlayerPrefs.SetInt("_IsGameStartTheFirstTime", 0); // set key = false

        }
    }

    void _MakeSingleInstance()
    {
        if(instance != null) // nếu đã tồn tại, xoá bản sau
        {
            Destroy(gameObject);
        } else
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // giữ game object này khi điều hướng sang screen khác
        }
    }

    public void _SetHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE, score);
    }

    public int _GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }


}
