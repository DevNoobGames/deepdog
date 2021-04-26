using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdManagerShooter : MonoBehaviour
{
    public float timer = 10;

    void Start()
    {
        StartCoroutine(spawn());
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GameObject.FindGameObjectWithTag("chunkManager").GetComponent<chunkManager>().ActiveTimer = 0.1f;
            Destroy(gameObject);
        }
    }

    IEnumerator spawn()
    {
        float randval = Random.Range(0.6f, 1);
        yield return new WaitForSeconds(randval);
        GameObject bird = Instantiate(Resources.Load("RandomChunks/Bird"), transform.position, Quaternion.identity) as GameObject;
        StartCoroutine(spawn());
    }
}
