using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody2D theRB;
    AudioSource theAS;
    // 小球一开始的初速度。
    [SerializeField] float xSpeed = 3f;
    [SerializeField] float ySpeed = 5f;

    private bool transparent = false;
    public float minTimeBeforeNextTransparent = 1f;
    public float maxTimeBeforeNextTransparent = 5f;
    private float transparentInterval = 1f; 

    private void Awake()
    {
        theRB = GetComponent<Rigidbody2D>();
        theAS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(gameObject.tag == "TransparentBall") {
            HandleTransparentBall();
        }
    }

    // 发射小球。
    public void LaunchBall() {
        theRB.velocity = new Vector2(xSpeed, ySpeed);
        // 参考系从板变到相机。
        transform.parent = null;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // 每次强行给一个速度，防止小球纯垂直运动或者纯水平运动。
        theRB.velocity -= new Vector2(0.05f, 0.05f);
        theAS.Play();
    }

    private void HandleTransparentBall() {
        transparentInterval -= Time.deltaTime;
        if (transparentInterval < 0f)
        {
            if (!transparent)
            {
                transparentInterval = 1f;
                GetComponent<SpriteRenderer>().enabled = false;
                transparent = true;
            }
            else
            {
                transparentInterval = Random.Range(minTimeBeforeNextTransparent, maxTimeBeforeNextTransparent);
                GetComponent<SpriteRenderer>().enabled = true;
                transparent = false;
            }
        }
    }
}
