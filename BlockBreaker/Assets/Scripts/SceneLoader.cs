using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader loader;

    private void Awake()
    {
        loader = this;
    }

    // Use this for initialization
    public void LoadNextScene()
    {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curSceneIndex + 1);
    }

    // Use this for initialization
    public void LoadCurScene()
    {
        int curSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(curSceneIndex);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
        GameControl.control.ResetGame();
    }

    public void QuitGame() {
        Application.Quit();
    }
}
