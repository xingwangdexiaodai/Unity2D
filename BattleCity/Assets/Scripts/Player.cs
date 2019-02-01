using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private const int DOWN = 0, RIGHT = 90, UP = 180, LEFT = 270;

    // Tank Components.
    private Rigidbody2D rigidbody2D;
    [SerializeField]
    private GameObject projectilePF;

    // Tnak Config.
    [SerializeField]
    private float moveSpeed = 10f;
    private int nxtDirection;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(projectilePF, transform.position, transform.rotation);
        }
    }
}
