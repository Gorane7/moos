using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ResourcePoint : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] private float lineWidth = 0.1f;
    [SerializeField] private float growSpeed = 0.1f;
    [SerializeField] private float frequency = 0.1f;
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float dieSpeed = 0.001f;

    private SpriteRenderer spriteRenderer;
    private bool isVisible;
    private string connectionState = "None";
    private int connectionLength = 0;
    private LineRenderer lineRenderer;
    private float opacity = 1f;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.sortingOrder = 1;
        lineRenderer.startColor = Color.red; // Example: Set to red
        lineRenderer.endColor = Color.red;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isVisible = false;
    }

    void Update()
    {
        if (connectionState == "Advancing") {
            connectionLength += 1;
            if (connectionLength * growSpeed >= (transform.position - LevelManager.main.castle.transform.position).magnitude) {
                connectionState = "Done";
            }
        }
        if (connectionState == "Dying") {
            opacity -= dieSpeed;
            if (opacity < 0) {
                connectionState = "None";
                opacity = 1f;
                connectionLength = 0;
            }
        }
        DrawConnection();
    }

    void SetTransparency(float alpha)
    {
        // Get the LineRenderer's material
        Material material = lineRenderer.material;
        Debug.Log("Material: " + material);

        // Get the current color
        Color currentColor = material.color;

        // Create a new color with the same RGB components and the specified alpha
        Color newColor = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);

        // Set the material's color to the new color
        material.color = newColor;
    }

    void DrawConnection()
    {
        SetTransparency(opacity);
        lineRenderer.positionCount = connectionLength + 1;
        lineRenderer.useWorldSpace = true;

        Vector3 direction = (transform.position - LevelManager.main.castle.transform.position).normalized;
        Vector3 crossDirection = Quaternion.Euler(0, 0, 90) * direction;
        //Debug.Log(connectionLength);

        Vector3 start = LevelManager.main.castle.transform.position;
        for (int i = 0; i < connectionLength + 1; i++) {
            lineRenderer.SetPosition(i, start + i * direction * growSpeed + Mathf.Sin(i * growSpeed * frequency) * crossDirection * amplitude);
        }

        //float deltaTheta = (2f * Mathf.PI) / vertexCount;
        //float theta = 0f;

        /*for (int i = 0; i < vertexCount + 1; i++)
        {
            float x = shootRadius * Mathf.Cos(theta);
            float y = shootRadius * Mathf.Sin(theta);

            Vector3 pos = new Vector3(x, y, 0f) + centerPosition;
            lineRenderer.SetPosition(i, pos);

            theta += deltaTheta;
        }*/
    }

    private void StartDying() {
        connectionState = "Dying";
    }

    private void OnMouseDown()
    {
        if (!isVisible) return;
        Debug.Log("Got CLICK");
        if (connectionState == "None") {
            connectionState = "Advancing";
        } else if (connectionState == "Advancing" || connectionState == "Done") {
            StartDying();
        }
        

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
