using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private bool launchBall = true;
    // 移动速度得倒数。
    [SerializeField]
    private float moveSpeedInverse = 10f;
    public bool GUA = false;

    // Update is called once per frame
    void Update()
    {
        HandleBall();
        Move();
    }

    private void HandleBall() {
        if (launchBall && Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var ball in FindObjectsOfType<Ball>())
            {
                ball.LaunchBall();
            }
            launchBall = false;
        }
    }

    private void Move() {
        if (!launchBall && GUA)
        {
            Cheating();
            return;
        }

        var dx = Input.GetAxisRaw("Horizontal");
        var newX = Mathf.Clamp(transform.position.x + dx / moveSpeedInverse, -3.5f, 3.5f);
        transform.position = new Vector2(newX, transform.position.y);
    }

    private void Cheating()
    {
        Ball lowestball = null;
        float lowestY = 1 << 10;
        foreach (var ball in FindObjectsOfType<Ball>())
        {
            if (lowestY > ball.transform.position.y)
            {
                lowestY = ball.transform.position.y;
                lowestball = ball;
            }
        }
        var newX = Mathf.Clamp(lowestball.transform.position.x, -3.5f, 3.5f);
        this.transform.position = new Vector2(newX, this.transform.position.y);
    }
}
