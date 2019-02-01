using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    SceneLoader loader;

    private void Awake()
    {
        loader = FindObjectOfType<SceneLoader>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = "LEVEL 00" + loader.getLevelIndex();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
