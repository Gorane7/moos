using System.Collections;
using UnityEngine;

public class CastleBehavior : MonoBehaviour
{

    [Header("Attributes")]

    [SerializeField] private static int baseHealth = 10;

    private int currentHealth = baseHealth;

    void Start()
    {
    }

    public void MonsterHit() {
        currentHealth -= 1;
    }
}
