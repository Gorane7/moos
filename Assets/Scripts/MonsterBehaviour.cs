using System.Collections;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private static int baseHealth = 2;

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

            if (Vector3.Distance(transform.position, LevelManager.main.castle.transform.position) < 0.5f) // Adjust the threshold as needed
            {
                // Remove the object if it collides with the target
                Destroy(gameObject);
                LevelManager.main.MonsterHit();
            }

            // Wait for the next frame
            yield return null;
        }
    }
}
