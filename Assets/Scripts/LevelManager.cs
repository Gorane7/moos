using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager main;

    public GameObject castle;
    public GameObject[] towers;
    public GameObject[] caves;
    public List<GameObject> monsters;

    private void Awake() {
        main = this;
    }

    void Start()
    {
    }
}
