using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dinosaurScript : MonoBehaviour
{
    private Vector3 positionDisplacement;
    private Vector3 positionOrigin;
    private float timePassed;

    public Transform fireSpot;

    public bool isGoingUp;
    public bool isGoingDown;

    public bool loaded;


    void Start()
    {
        positionDisplacement = new Vector3(3.6f, 0, 0);
        isGoingUp = true;
    }

    private void FixedUpdate()
    {
        if (isGoingUp)
        {
            transform.Translate(Vector2.up * 0.021f);
        }
        else if (!isGoingDown)
        {
            timePassed += Time.deltaTime;
            transform.localPosition = Vector3.Lerp(positionOrigin, positionOrigin + positionDisplacement,
            Mathf.PingPong(timePassed, 1));

            if (timePassed > 10)
            {
                isGoingDown = true;
            }

            if (loaded)
            {
                loaded = false;
                GameObject bally = Instantiate(Resources.Load("RandomChunks/DinoBall"), fireSpot.position, Quaternion.identity) as GameObject;
                Destroy(bally, 6);
                StartCoroutine(reload());
            }
        }
        if (isGoingDown)
        {
            transform.Translate(Vector2.down * 0.021f);
        }

        if (transform.localPosition.y >= -3 && isGoingUp)
        {
            positionOrigin = new Vector3(-1.8f, transform.position.y, transform.position.z);
            isGoingUp = false;
        }
    }

    IEnumerator reload()
    {
        float randval = Random.Range(0.4f, 0.7f);
        yield return new WaitForSeconds(randval);
        loaded = true;
    }
}
