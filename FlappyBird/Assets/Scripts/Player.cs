using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Animator theAM;
    Rigidbody2D theRB;
    AudioSource theAS;

    private void Awake()
    {
        theAM = GetComponent<Animator>();
        theRB = GetComponent<Rigidbody2D>();
        theAS = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.instance.isAlive) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            theRB.velocity = Vector2.zero;
            theRB.AddForce(new Vector2(0, 500));
            theAM.SetTrigger("Flap");
            transform.eulerAngles = new Vector3(0, 0, 25);
            theAS.Play();
        }
        else {
            transform.Rotate(0, 0, -1.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameController.instance.isAlive = false;
        theAM.SetTrigger("Die");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameController.instance.ScoreUp();
    }
}
