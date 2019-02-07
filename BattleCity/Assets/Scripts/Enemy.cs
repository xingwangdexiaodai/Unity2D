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
    [SerializeField]
    private float moveSpeed = 10f;
    private int nxtDirection = UP;
    [SerializeField]
    private float fireRate = 5f;
    private float timeToFire = 0f;
    private bool initDone = false;

    private void Awake()
    {
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
        Turn();
        Fire();
    }

    private void Move()
    {
        var xSpeed = Input.GetAxis("Horizontal");

        if (Mathf.Abs(xSpeed) > Mathf.Epsilon)
        {
            rigidbody2D.velocity = new Vector2(xSpeed, 0) * moveSpeed;
            nxtDirection = xSpeed > 0 ? RIGHT : LEFT;
            return;
        }

        var ySpeed = Input.GetAxis("Vertical");
        if (Mathf.Abs(ySpeed) > Mathf.Epsilon)
        {
            rigidbody2D.velocity = new Vector2(0, ySpeed) * moveSpeed;
            nxtDirection = ySpeed > 0 ? UP : DOWN;
            return;
        }

        rigidbody2D.velocity *= 0;
    }

    private void Turn()
    {
        float curDirection = transform.eulerAngles.z;
        transform.Rotate(0, 0, nxtDirection - curDirection);
    }

    private void Fire()
    {
        if (Input.GetButton("Fire1") && timeToFire < Time.time)
        {
            projectileMG.Shoot();
            timeToFire = Time.time + 1 / fireRate;
        }
    }

    public void InitDone() {
        initDone = true;
    }
}
