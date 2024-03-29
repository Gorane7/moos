using System.Collections;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;

    public static int baseHealth = 2;

    private int currentHealth = baseHealth;

    void Start()
    {
        // Start generating monsters periodically
        StartCoroutine(MoveTowardsCastle());
    }

    public void ProjectileHit() {
        currentHealth -= 1;
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    IEnumerator MoveTowardsCastle()
    {
        while (true)
        {
            
            // Calculate the direction towards the castle
            Vector3 direction = (LevelManager.main.castle.transform.position - transform.position).normalized;

            // Move towards the castle
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // Wait for the next frame
            yield return null;
        }
    }
}
