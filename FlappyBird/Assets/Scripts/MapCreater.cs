using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreater : MonoBehaviour
{
    private float timeToSpawn = 0f;
    public GameObject OB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToSpawn < Time.time) {
            timeToSpawn = Time.time + Random.Range(1f, 2f);
            Instantiate(OB, new Vector3(14, Random.Range(-1, 1)), Quaternion.identity); ;
        }
    }
}
