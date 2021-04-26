using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public Material BGMaterial;
    Vector2 offset;

    public float SpeedX, SpeedY;
    private void Start()
    {
        BGMaterial = GetComponent<Renderer>().material;
    }
    void Update()
    {
        offset = new Vector2(SpeedX, SpeedY);
        BGMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
