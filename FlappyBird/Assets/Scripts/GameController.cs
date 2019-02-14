using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public float score = 0f;
    public bool isAlive = true;
    public Text scoreT;

    private void Awake()
    {
        if(instance == null) {
            instance = this;
        }
        else if(instance != this){
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreT.text = score.ToString();
    }

    public void ScoreUp() {
        score++;
    }
}
