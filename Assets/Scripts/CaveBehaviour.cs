using System.Collections;
using UnityEngine;

public class CaveBehavior : MonoBehaviour
{
    public GameObject monsterPrefab; // The prefab of the monster to generate
    public float moveSpeed = 5f;
    public float generationInterval = 5f; // Time interval between each monster generation

    void Start()
    {
        // Start generating monsters periodically
        StartCoroutine(GenerateMonsters());
    }

    IEnumerator GenerateMonsters()
    {
        while (true)
        {
            // Instantiate the monster at the cave's position
            GameObject monster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);

            LevelManager.main.monsters.Add(monster);

            StartCoroutine(MoveTowardsCastle(monster));

            // Wait for the specified interval before generating the next monster
            yield return new WaitForSeconds(generationInterval);
        }
    }

    IEnumerator MoveTowardsCastle(GameObject monster)
    {
        while (true)
        {
            // Check if the castle object exists
            
            // Calculate the direction towards the castle
            Vector3 direction = (LevelManager.main.castle.transform.position - monster.transform.position).normalized;

            // Move towards the castle
            monster.transform.Translate(direction * moveSpeed * Time.deltaTime);

            // Wait for the next frame
            yield return null;
        }
    }
}
