using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TowerBehaviour : MonoBehaviour
{

    [Header("Attributes")]
    [SerializeField] private float generationInterval = 0.5f;
    [SerializeField] private float shootRadius = 10f;
    [SerializeField] private float lineWidth = 0.1f;

    public GameObject objectToGenerate;
    
    public Vector3 size;
    private LevelManager levelManager;
    private Vector3 centerPosition;
    private float sleepInterval = 0.1f;
    private int vertexCount = 40;
    private LineRenderer lineRenderer;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
    }

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
            //Debug.Log("Got monster " + randomMonster);

            // If there are monsters available
            if (randomMonster != null)
            {
                GameObject projectile = Instantiate(objectToGenerate, spawnPosition, Quaternion.identity);
                ProjectileBehaviour projectileBehaviour = projectile.GetComponent<ProjectileBehaviour>();
                if (projectileBehaviour != null)
                {
                    projectileBehaviour.SetTargetObject(randomMonster);
                }
                yield return new WaitForSeconds(generationInterval);
            }
            yield return new WaitForSeconds(sleepInterval);
        }
    }

    GameObject GetRandomMonster()
    {
        List<GameObject> monstersInRange = new List<GameObject>();

        // Iterate through all monsters
        foreach (GameObject monster in levelManager.monsters)
        {
            // Calculate the distance between the tower and the monster
            float distance = Vector3.Distance(transform.position, monster.transform.position);

            // If the monster is within range, add it to the list
            if (distance <= shootRadius)
            {
                monstersInRange.Add(monster);
            }
        }

        // If there are monsters within range
        if (monstersInRange.Count > 0)
        {
            // Choose a random index from the monsters within range
            int randomIndex = Random.Range(0, monstersInRange.Count);
            // Return the monster at the random index
            return monstersInRange[randomIndex];
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        DrawCircle();
    }

    void DrawCircle()
    {
        lineRenderer.positionCount = vertexCount + 1;
        lineRenderer.useWorldSpace = true;

        float deltaTheta = (2f * Mathf.PI) / vertexCount;
        float theta = 0f;

        for (int i = 0; i < vertexCount + 1; i++)
        {
            float x = shootRadius * Mathf.Cos(theta);
            float y = shootRadius * Mathf.Sin(theta);

            Vector3 pos = new Vector3(x, y, 0f) + centerPosition;
            lineRenderer.SetPosition(i, pos);

            theta += deltaTheta;
        }
    }
}
