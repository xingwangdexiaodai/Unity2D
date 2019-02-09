using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public AudioClip clip;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BricksCreater.creater.HandleBrickDestoryed();
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
