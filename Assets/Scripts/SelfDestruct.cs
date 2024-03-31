using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{

    public float lifeLength;
    void Start()
    {
        Destroy(gameObject, lifeLength);
    }


}
