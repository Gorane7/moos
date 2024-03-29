using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] private float generationInterval = 2f;

    public GameObject objectToGenerate;
    
    public Vector3 size;
    private LevelManager levelManager;
    private Vector3 centerPosition;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.main;
        centerPosition = transform.position;
        StartCoroutine(GenerateObjects());
    }

    IEnumerator GenerateObjects()
    {
        while (true)
        {
            // Calculate a random position within the defined area
            Vector3 spawnPosition = centerPosition + new Vector3(Random.Range(-size.x / 2, size.x / 2),
                                                         Random.Range(-size.y / 2, size.y / 2),
                                                         Random.Range(-size.z / 2, size.z / 2));

            // Instantiate the object at the random position
            
            GameObject randomMonster = GetRandomMonster();

            // If there are monsters available
            if (randomMonster != null)
            {
                GameObject projectile = Instantiate(objectToGenerate, spawnPosition, Quaternion.identity);
                ProjectileBehaviour projectileBehaviour = projectile.GetComponent<ProjectileBehaviour>();
                if (projectileBehaviour != null)
                {
                    projectileBehaviour.SetTargetObject(randomMonster);
                }
            }

            // Wait for the specified interval before generating the next object
            yield return new WaitForSeconds(generationInterval);
        }
    }

    GameObject GetRandomMonster()
    {
        // If there are monsters available in the levelManager
        if (levelManager.monsters.Count > 0)
        {
            // Choose a random index
            int randomIndex = Random.Range(0, levelManager.monsters.Count);
            // Return the monster at the random index
            return levelManager.monsters[randomIndex];
        }
        else
        {
            return null; // Return null if there are no monsters available
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
