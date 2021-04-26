using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chunkManager : MonoBehaviour
{
    public float ActiveTimer = 3;
    public chunkClass[] ChunkInfo;

    [System.Serializable]
    public class chunkClass
    {
        public GameObject chunk;
        public float timeNeeded;
    }


    void Update()
    {
        ActiveTimer -= Time.deltaTime;
        if (ActiveTimer <= 0)
        {
            ActiveTimer = 9999; //Just to make sure it doesnt run twice - not tested anyway (sorry for the mess, I changed it)
            int ranval = Random.Range(0, ChunkInfo.Length);
            GameObject chunkSpawn = Instantiate(ChunkInfo[ranval].chunk, transform.position, Quaternion.identity) as GameObject;
            StartCoroutine(destroyChunk(chunkSpawn, ChunkInfo[ranval].timeNeeded));
        }
    }

    IEnumerator destroyChunk(GameObject chunk, float time)
    {
        yield return new WaitForSeconds(time);
        if (chunk != null)
        {
            ActiveTimer = 0;
        }
        yield return new WaitForSeconds(5);
        if (chunk != null)
        {
            Destroy(chunk);
        }
    }

}
