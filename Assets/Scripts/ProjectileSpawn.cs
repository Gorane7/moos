using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawn : MonoBehaviour
{

    public float generationInterval = 2f;
    public float moveSpeed = 5f;
    public GameObject objectToGenerate;
    public GameObject centerObject;
    public GameObject targetObject;
    public Vector3 size;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GenerateObjects());
    }

    IEnumerator GenerateObjects()
    {
        while (true)
        {
            // Calculate a random position within the defined area
            Vector3 spawnPosition = centerObject.transform.position + new Vector3(Random.Range(-size.x / 2, size.x / 2),
                                                         Random.Range(-size.y / 2, size.y / 2),
                                                         Random.Range(-size.z / 2, size.z / 2));

            // Instantiate the object at the random position
            GameObject projectile = Instantiate(objectToGenerate, spawnPosition, Quaternion.identity);
            StartCoroutine(MoveTowardsTarget(projectile));

            // Wait for the specified interval before generating the next object
            yield return new WaitForSeconds(generationInterval);
        }
    }

    IEnumerator MoveTowardsTarget(GameObject obj)
    {
        while (true)
        {
            // Calculate the direction towards the target
            Vector3 direction = (targetObject.transform.position - obj.transform.position).normalized;

            // Move towards the target
            obj.transform.Translate(direction * moveSpeed * Time.deltaTime);

             if (Vector3.Distance(obj.transform.position, targetObject.transform.position) < 0.5f) // Adjust the threshold as needed
            {
                // Remove the object if it collides with the target
                Destroy(obj);
                break; // Exit the coroutine
            }

            // Wait for the next frame
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
