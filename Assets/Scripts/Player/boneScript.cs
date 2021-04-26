using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boneScript : MonoBehaviour
{
    public string[] tags = {
        "fireball",
        "dinosaur",
        "dinoball",
        "bird",
        };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit");

        foreach (string tag in tags)
        {
            if (collision.CompareTag(tag))
            {
                deleteItAll(collision.gameObject, tag);
            }
        }
    }

    public void deleteItAll(GameObject deleteThis, string tag)
    {
        Destroy(deleteThis);
        if (tag == "dinosaur")
        {
            GameObject.FindGameObjectWithTag("chunkManager").GetComponent<chunkManager>().ActiveTimer = 0.1f;
        }

        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
