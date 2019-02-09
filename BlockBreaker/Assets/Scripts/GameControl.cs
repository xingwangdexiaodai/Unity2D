using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static GameControl control;

    // 要不要重置最高分。
    public bool resetHighestScore = false;
    // 当前得分
    public int curScore = 0;

    private void Awake()
    { 
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            if (resetHighestScore)
            {
                ResetHigestScore();
            }
            control = this;
        }
        else if(control != this)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void UpdateScore() {
        ++curScore;
        UpdateHighestScore();
    }

    public void UpdateHighestScore()
    {
        if (curScore > PlayerPrefs.GetInt("Higest_Score"))
        {
            PlayerPrefs.SetInt("Higest_Score", curScore);
        }
    }

    public void ResetHigestScore()
    {
        PlayerPrefs.DeleteKey("Higest_Score");
        PlayerPrefs.Save();
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }
}
