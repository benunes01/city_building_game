using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGrid : MonoBehaviour
{
    public GameObject ground;
    
    void Start()
    {
        ground.GetComponent<Renderer>().material.mainTextureScale = new Vector2(0, 0);
    }
}
