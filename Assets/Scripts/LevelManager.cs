using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager main;
    public static int baseHealth;

    public GameObject castle;
    public GameObject[] towers;
    public GameObject[] caves;
    public List<GameObject> monsters;
    private int currentHealth = baseHealth;

    private void Awake() {
        main = this;
    }

    void Start()
    {
    }

    public void RemoveMonster(GameObject monster) {
        if (monsters.Contains(monster)) {
            monsters.Remove(monster);
            Destroy(monster);
        }
    }
}
