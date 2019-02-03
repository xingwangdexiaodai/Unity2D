using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    // 蛇的身体。
    [SerializeField]
    private GameObject node;
    private List<GameObject> body;

    // 蛇的状态。
    private Vector2 direction = Vector2.down;
    private bool ateFood = false;
    private bool dead = false;
    private int foodCount = 0;
    [SerializeField]
    private float startWaitTime = 0.2f;
    private float waitToMove;
    private int level;

    // 食物生成器。
    private FoodCreater foodCreater;

    private AudioSource audioSource;

    [SerializeField]
    private int levelUpRate = 2;
    private int score = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        waitToMove = startWaitTime;
        body = new List<GameObject>();
        foodCreater = FindObjectOfType<FoodCreater>();
    }

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (true) {
            Move();
            yield return new WaitForSeconds(waitToMove);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Vector2.down;
        }

        if (waitToMove > 0.02f)
        {
            level = 1 + foodCount / levelUpRate;
            waitToMove = startWaitTime - level / 100f;
        }
    }

    private void Move() {
        if (dead) return;

        // 得到蛇当前蛇头的位置。
        var curHeadPosition = transform.position;
        // 把蛇头移到下一个位置。
        transform.Translate(direction);

        if (ateFood) {
            // 在蛇头原来的地方生成一个身体，并在链表头部插入一段新的身体。
            // 把蛇吃过食物的状态改否，同时让食物生成器重新生成食物。
            var newNode = Instantiate(node, curHeadPosition, Quaternion.identity);
            body.Insert(0,newNode);
            ateFood = false;
            while (!foodCreater.CreateFoodPosition(body)) { };
        }
        else {
            // 把尾巴移到蛇头原来的位置，对链表做类似操作。
            if (body.Count == 0) return;
            var tail = body[body.Count - 1];
            tail.transform.position = curHeadPosition;
            body.RemoveAt(body.Count - 1);
            body.Insert(0, tail);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 判断吃到的是不是食物。
        if(collision.tag == "Food") {
            // 把蛇的状态改成刚吃过食物，并让食物消失
            ateFood = true;
            audioSource.Play();
            Destroy(collision.gameObject);
            foodCount++;
            score += level;
        }
        else {
            // 当蛇撞到其他物体，则蛇死亡
            dead = true;
        }
    }   

    public int GetLevel() {
        return level;
    }

    public int GetScore() {
        return score;
    }
}
