using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingGround : MonoBehaviour
{
    BoxCollider2D theBC;
    private float wid;

    private void Awake()
    {
        theBC = GetComponent<BoxCollider2D>();
        wid = theBC.size.x;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -wid) {
            transform.position = new Vector2(wid, transform.position.y);
        }
    }
}
