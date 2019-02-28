using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float minX, maxX, minY, maxY;
    public float moveSpeed = 10f;

    public GameObject laserPF;
    public float fireRate = 10f;
    float nextFire;

    public GameObject explosionPF;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
        Move();
    }

    private void Fire()
    {
        if (Input.GetButton("Fire1") && nextFire < Time.time)
        {
            AudioManager.instance.Play("Shoot");
            nextFire = Time.time + 1 / fireRate;
            Instantiate(laserPF, transform.position, Quaternion.identity);
        }
    }

    private void Move()
    {
        var dx = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var dy = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var x = Mathf.Clamp(transform.position.x, minX, maxX);
        var y = Mathf.Clamp(transform.position.y, minY, maxY);

        var nx = x + dx;
        var ny = y + dy;

        transform.position = new Vector3(nx, ny);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.instance.Play("Bomb");
        GameManager.instance.GameOver();
        var explosion = Instantiate(explosionPF, transform.position, Quaternion.identity);
        Destroy(explosion, 0.4f);
        Destroy(gameObject);
    }
}
