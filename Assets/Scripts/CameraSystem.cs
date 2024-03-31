using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{

    public float scrollSpeed = 45;
    public int scrollDistance = 30;

    private Bounds _cameraBounds;
    private Vector3 _targetPosition;
    private Camera _mainCamera;


    private void Awake()
    {
        _mainCamera = Camera.main;
    }
    private void Start()
    {
        var height = _mainCamera.orthographicSize;
        var width = height * _mainCamera.aspect;

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.extents.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height;

        _cameraBounds = new Bounds();
        _cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0.0f),
            new Vector3(maxX, maxY, 0.0f));
    }

    void Update()
    {

        float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0.0f;

        if (mousePosX < scrollDistance && (mouseWorldPos.x<_cameraBounds.max.x && mouseWorldPos.x > _cameraBounds.min.x))
        {
            transform.Translate(-scrollSpeed * Time.deltaTime * Vector3.right);
        }

        if (mousePosX >= Screen.width - scrollDistance && (mouseWorldPos.x < _cameraBounds.max.x && mouseWorldPos.x > _cameraBounds.min.x))
        {
            transform.Translate(scrollSpeed * Time.deltaTime * Vector3.right);
        }

        if (mousePosY < scrollDistance && (mouseWorldPos.y < _cameraBounds.max.y && mouseWorldPos.y > _cameraBounds.min.y))
        {
            transform.Translate(-scrollSpeed * Time.deltaTime * Vector3.up);
        }

        if (mousePosY >= Screen.height - scrollDistance && (mouseWorldPos.y < _cameraBounds.max.y && mouseWorldPos.y > _cameraBounds.min.y))
        {
            transform.Translate(scrollSpeed * Time.deltaTime * Vector3.up);
        }
    }
    private void LateUpdate()
    {

        _targetPosition = GetCameraBounds();

    }
    private Vector3 GetCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(_targetPosition.x, _cameraBounds.min.x, _cameraBounds.max.x),
            Mathf.Clamp(_targetPosition.y, _cameraBounds.min.y, _cameraBounds.max.y),
            transform.position.z
            );
    }
}
