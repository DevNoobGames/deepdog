using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chunkMoving : MonoBehaviour
{
    public float speed = 0.063f;

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * speed);
    }
}
