using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksCreater : MonoBehaviour
{
    public static BricksCreater creater;

    [SerializeField]
    private GameObject[] bricks;
    private float len = 1f;
    private float wid = 0.5f;
    private int brickNum = 0;

    private void Awake()
    {
        creater = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        int rowNum = Random.Range(1, 5);
        int colNum = Random.Range(1, 9);
        for (int i = 0; i < rowNum; ++i)
        {
            for (int j = 0; j < colNum; ++j)
            {
                var brick = bricks[Random.Range(0, bricks.Length - 1)];
                Instantiate(brick, new Vector3(len * (j - colNum/2f), wid * i), Quaternion.identity);
                ++brickNum;
            }
        }
    }

    public void HandleBrickDestoryed() {
        --brickNum;
        GameControl.control.UpdateScore();
        if (brickNum == 0) {
            SceneLoader.loader.LoadNextScene();
        }
    }
}
