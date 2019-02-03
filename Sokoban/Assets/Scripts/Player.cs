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

    // 用来记录上次摁键后玩家的方向。
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
        animator.SetFloat("moveX", x_dir);
        animator.SetFloat("moveY", y_dir);

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

        // 更新玩家的动画。
        // Update player last movement.
        if (dx != 0 || dy != 0)
        {
            // Player movement animation.
            x_dir = dx;
            y_dir = dy;
        }

        // 玩家下个位置。
        // Next position.
        int nx = dx + (int)transform.position.x;
        int ny = dy + (int)transform.position.y;

        // 判断下个位置是不是墙。
        if (isWall(nx, ny)) return;

        // 判断下个位置是不是盒子。
        if (isBox(nx, ny))
        {
            // 得到玩家的下下个位置。
            // Next next position.
            int nnx = nx + dx;
            int nny = ny + dy;

            // 判断下下个位置是不是墙或者盒子。
            if (isBox(nnx, nny) || isWall(nnx, nny)) return;

            // 把盒子移到下个位置。
            GameObject box = getBox(nx, ny);
            box.transform.position = new Vector3(nnx, nny);

            // 更新盒子在Map里面的结构。
            myMap.getPosBoxMap().Remove(myMap.TwoDToOneD(nx, ny));
            myMap.getPosBoxMap().Add(myMap.TwoDToOneD(nnx, nny), box);
        }

        // 把玩家移动到下个位置。
        // Move player to next position.
        transform.position = new Vector3(nx, ny);

        // 如果玩家移动，播放音乐。
        if(dx != 0 || dy != 0) {
            // Play foot step sound.
            audioSource.Play();
        }

        // 判断是不是赢了。
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
