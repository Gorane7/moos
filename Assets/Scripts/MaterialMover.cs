using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMover : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private SpriteRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        renderer.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
