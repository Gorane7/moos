using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMover : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        spriteRenderer.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
