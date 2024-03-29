    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickScript : MonoBehaviour
{
    public GameObject torch;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("am here");
            Vector3 mousePos = Input.mousePosition;
            Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            objectPos.z = 0f;
            Instantiate(torch, objectPos, Quaternion.identity);
        }
    }
}
