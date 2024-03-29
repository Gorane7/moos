using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TorchGrower : MonoBehaviour
{
    public float Size;
    public float Duration;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOScale(1.0f* Size, 1.0f * Duration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
