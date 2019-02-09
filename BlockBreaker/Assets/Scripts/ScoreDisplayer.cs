using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplayer : MonoBehaviour
{
    public Text higestScore;
    public Text currentScore;

    // Update is called once per frame
    void Update()
    {
        higestScore.text = PlayerPrefs.GetInt("Higest_Score").ToString();
        currentScore.text = GameControl.control.curScore.ToString();
    }
}
