using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBall : MonoBehaviour
{
    private Vector3 positionDisplacement;
    private Vector3 positionOrigin;
    private float timePassed;

    private void Start()
    {
        positionOrigin = transform.localPosition;
        if (transform.position.x < 0)
        {
            positionDisplacement = new Vector3(3, 0, 0);
        }
        else
        {
            positionDisplacement = new Vector3(-3, 0, 0);
        }
    }

    private void Update()
    {
        timePassed += Time.deltaTime;
        transform.localPosition = Vector3.Lerp(positionOrigin, positionOrigin + positionDisplacement,
            Mathf.PingPong(timePassed, 1));
    }
}
