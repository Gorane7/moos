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
    public float shootdelay = 1.0f;
    private float lastshoottime = 0;
    private LevelManager levelManager;
    private Vector3 centerPosition;
    private float sleepInterval = 0.1f;
    private int vertexCount = 40;
    private LineRenderer lineRenderer;
    public List<GameObject> monstersInRange;
    public List<GameObject> torches = new List<GameObject>();
    public float shootangle = 180.0f;
    private bool isVisible= false;

    void Awake()
    {
        monstersInRange = new List<GameObject>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;
        lineRenderer.sortingOrder = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.main;
        centerPosition = transform.position;
        //StartCoroutine(GenerateObjects());
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
        //DrawCircle();
        int i = 0;
        while (monstersInRange.Count > i && Time.time - lastshoottime > shootdelay && isVisible)
        {
            if (monstersInRange[i] == null) {
                i += 1;
                continue;
            }
            Vector3 objectpos = monstersInRange[0].transform.position;
            Vector3 vectorToTarget = objectpos - transform.position;
            Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * vectorToTarget;  
            Instantiate(objectToGenerate, transform.position, Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget));
            lastshoottime = Time.time;
            break;
        }
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Torch"))
        {
            isVisible = true;
            torches.Add(collision.gameObject);
        }
        if (!collision.CompareTag("Monster") && !collision.CompareTag("Boss")) { return; }
        monstersInRange.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Torch"))
        {
            torches.Remove(collision.gameObject);
            if (torches.Count == 0)
            {
                isVisible = false;
            }
        }
        if (!collision.CompareTag("Monster") && !collision.CompareTag("Boss")) { return; }
        monstersInRange.Remove(collision.gameObject);
    }
}
