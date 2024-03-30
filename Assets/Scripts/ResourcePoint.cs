using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePoint : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private bool isVisible;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isVisible = false;
    }

    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (!isVisible) return;

    }
    private void OnMouseEnter()
    {
        print(isVisible);
        if (!isVisible) return;
        spriteRenderer.color = Color.red;
    }

    private void OnMouseExit()
    {
        print(isVisible);
        if (!isVisible) return;
        Debug.Log("EXIT");
        spriteRenderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Light!");
        if (collision.tag != "Torch") return;
        isVisible = true;
    }


}
