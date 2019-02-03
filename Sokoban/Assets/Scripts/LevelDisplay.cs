using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    private SceneLoader loader;
    public int digitCount = 4;

    private void Awake()
    {
        loader = FindObjectOfType<SceneLoader>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // 得到本关关数。
        string in_level = loader.getLevelIndex().ToString();
        // 调整字符串让最终显示总共有4位，前面是一连串的0，
        // 比如 关数 1， 最后显示 0001.
        string out_level = in_level.PadLeft(digitCount, '0');
        GetComponent<Text>().text = "LEVEL " + out_level;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
