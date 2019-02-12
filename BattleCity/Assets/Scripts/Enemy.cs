using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private const int DOWN = 0, RIGHT = 90, UP = 180, LEFT = 270;

    // Tank Components.
    private Rigidbody2D rigidbody2D;
    private ProjectileManager projectileMG;

    // Tnak Config.
    private bool initDone = false;
    // Move.
    [SerializeField]
    private float moveSpeed = 10f;
    // Turn.
    private int nxtDirection = DOWN;
    [SerializeField]
    private float minTimeBeforeNextTurn = 0.5f;
    [SerializeField]
    private float maxTimeBeforeNextTurn = 3f;
    private float turnInterval = 1f;
    // Fire.
    [SerializeField]
    private float minTimeBeforeNextFire = 0.5f;
    [SerializeField]
    private float maxTimeBeforeNextFire = 1f;
    private float fireInterval = 1f;

    private void Awake()
    {
        fireInterval = Random.Range(minTimeBeforeNextFire, maxTimeBeforeNextFire);
        turnInterval = Random.Range(minTimeBeforeNextTurn, maxTimeBeforeNextTurn);
        projectileMG = GetComponent<ProjectileManager>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!initDone) return;
        Move();
        CountDownAndTurn();
        CountDownAndFire();
    }

    private void Move()
    {
        float xSpeed = 0f, ySpeed = 0f;
        switch (nxtDirection) {
            case UP:
                ySpeed = 1;
                break;
            case DOWN:
                ySpeed = -1;
                break;
            case LEFT:
                xSpeed = -1;
                break;
            case RIGHT:
                xSpeed = 1;
                break;
        }
        rigidbody2D.velocity = new Vector2(xSpeed, ySpeed) * moveSpeed;
    }

    private void Turn()
    {
        int nxtDirVal = Random.Range(0, 5);
        switch (nxtDirVal) {
            case 0:
                nxtDirection = UP;
                break;
            case 1:
                nxtDirection = LEFT;
                break;
            case 2:
                nxtDirection = RIGHT;
                break;
            default:
                nxtDirection = DOWN;
                break;
        }
        float curDirection = transform.eulerAngles.z;
        transform.Rotate(0, 0, nxtDirection - curDirection);
    }

    private void Fire()
    {
        projectileMG.Shoot();
    }

    public void InitDone() {
        initDone = true;
    }

    public void CountDownAndTurn() {
        turnInterval -= Time.deltaTime;
        if(turnInterval < 0) {
            Turn();
            turnInterval = Random.Range(minTimeBeforeNextTurn, maxTimeBeforeNextTurn);
        }
    }

    public void CountDownAndFire()
    {
        fireInterval -= Time.deltaTime;
        if (fireInterval < 0)
        {
            Fire();
            fireInterval = Random.Range(minTimeBeforeNextFire, maxTimeBeforeNextFire);
        }
    }
}
