using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    private ProjectileManager projectileMG;

    // Start is called before the first frame update
    void Start()
    {
        projectileMG = transform.parent.GetComponent<ProjectileManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(projectileMG != null)
        {
            projectileMG.DecrementProjectileCount();
        }
        else 
        {
            Debug.LogError("No Projectile Manager");
        }
        Destroy(gameObject);
    }
}
