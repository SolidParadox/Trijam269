
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Fire On Awake
public class BulletFOA : MonoBehaviour
{
    public float velocity;
    private void Awake()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.tag == "Wall" )
        {
            Destroy(gameObject);
        }
    }
}
