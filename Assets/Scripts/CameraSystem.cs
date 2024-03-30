using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{

    public float scrollSpeed = 45;
    public int scrollDistance = 30;
    void Update()
    {

        float mousePosX = Input.mousePosition.x;
        float mousePosY = Input.mousePosition.y;

        if (mousePosX < scrollDistance)
        {
            transform.Translate(-scrollSpeed * Time.deltaTime * Vector3.right);
        }

        if (mousePosX >= Screen.width - scrollDistance)
        {
            transform.Translate(scrollSpeed * Time.deltaTime * Vector3.right);
        }

        if (mousePosY < scrollDistance)
        {
            transform.Translate(-scrollSpeed * Time.deltaTime * Vector3.up);
        }

        if (mousePosY >= Screen.height - scrollDistance)
        {
            transform.Translate(scrollSpeed * Time.deltaTime * Vector3.up);
        }
    }
}
