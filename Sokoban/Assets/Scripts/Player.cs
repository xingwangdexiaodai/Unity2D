using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Map.
    private MapCreater myMap;

    // Audo.
    private AudioSource audioSource;

    // Animator.
    private Animator animator;

    private int x_dir = 0;
    private int y_dir = 0;

    private void Awake()
    {
        myMap = FindObjectOfType<MapCreater>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        // delta x and delta y.
        int dx = 0;
        int dy = 0;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            dx--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            dx++;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            dy++;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            dy--;
        }

        // Next position.
        int nx = dx + (int)transform.position.x;
        int ny = dy + (int)transform.position.y;

        if (isWall(nx, ny)) return;

        if (isBox(nx, ny))
        {
            // Next next position.
            int nnx = nx + dx;
            int nny = ny + dy;

            if (isBox(nnx, nny) || isWall(nnx, nny)) return;

            GameObject box = getBox(nx, ny);
            box.transform.position = new Vector3(nnx, nny);

            myMap.getPosBoxMap().Remove(myMap.TwoDToOneD(nx, ny));
            myMap.getPosBoxMap().Add(myMap.TwoDToOneD(nnx, nny), box);
        }

        // Move player to next position.
        transform.position = new Vector3(nx, ny);

        if(dx != 0 || dy != 0) {
            // Play foot step sound.
            audioSource.Play();

            // Player movement animation.
            x_dir = dx;
            y_dir = dy;
        }

        animator.SetFloat("moveX", x_dir);
        animator.SetFloat("moveY", y_dir);

        checkIfWin();
    }

    bool isWall(int x, int y)
    {
        return myMap.getWallPosSet().Contains(myMap.TwoDToOneD(x, y));
    }

    bool isBox(int x, int y)
    {
        return myMap.getPosBoxMap().ContainsKey(myMap.TwoDToOneD(x, y));
    }

    GameObject getBox(int x, int y)
    {
        return myMap.getPosBoxMap()[myMap.TwoDToOneD(x, y)];
    }

    void checkIfWin()
    {
        int num = 0;
        foreach (var tar_pos in myMap.getTargetPosList())
        {
            if (myMap.getPosBoxMap().ContainsKey(tar_pos)) ++num;
        }
        if (num == myMap.getTargetPosList().Count)
        {
            SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
            sceneLoader.LoadNextScene();
        }
    }
}
