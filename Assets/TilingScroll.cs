using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilingScroll : MonoBehaviour
{

    MeshRenderer mr;

    void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mr.materials[2].mainTextureOffset += new Vector2(Time.deltaTime * 0.1f, 0);  //mainTextureOffset = new Vector2(Time.deltaTime * 10, 0);
    }
}
