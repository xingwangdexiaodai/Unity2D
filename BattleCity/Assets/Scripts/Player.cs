using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const int DOWN = 0, RIGHT = 90, UP = 180, LEFT = 270;

    // Tank Components.
    private Rigidbody2D theRB;
    private ProjectileManager projectileMG;
    private Animator theAM;

    // Tnak Config.
    private bool isAlive = false;
    [SerializeField]
    private int health = 1;
    // Move.
    [SerializeField]
    private float moveSpeed = 10f;
    // Turn.
    private int nxtDirection = UP;
    // Fire.
    [SerializeField]
    private float fireRate = 5f;
    private float timeToFire = 0f;

    private void Awake()
    {
        projectileMG = GetComponent<ProjectileManager>();
        theRB = GetComponent<Rigidbody2D>();
        theAM = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return;
        Move();
        Turn();
        Fire();
    }

    private void Move()
    {
        var xSpeed = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(xSpeed) > Mathf.Epsilon)
        {
            theRB.velocity = new Vector2(xSpeed, 0) * moveSpeed;
            nxtDirection = xSpeed > 0 ? RIGHT : LEFT;
            return;
        }

        var ySpeed = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(ySpeed) > Mathf.Epsilon)
        {
            theRB.velocity = new Vector2(0, ySpeed) * moveSpeed;
            nxtDirection = ySpeed > 0 ? UP : DOWN;
            return;
        }

        theRB.velocity = Vector2.zero;
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
        isAlive = true;
    }

    public void DealDamage()
    {
        --health;
        if (health == 0)
        {
            isAlive = false;
            theAM.SetTrigger("Dead");
            theRB.simulated = false;
            GetComponent<SpriteRenderer>().sortingOrder = 9;
        }
    }

    public void DestroyTank()
    {
        Destroy(gameObject);
    }
}
