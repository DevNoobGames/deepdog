using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdScript : MonoBehaviour
{
    void Start()
    {
        Vector2 force = (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(force * 250);

        Destroy(gameObject, 5);
    }
}
