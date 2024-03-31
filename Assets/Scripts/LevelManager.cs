using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager main;
    public static int baseHealth;

    public GameObject castle;
    public GameObject cavePrefab;
    public GameObject bossPrefab;
    public GameObject menhirPrefab;
    public GameObject bushPrefab;
    public GameObject treePrefab1;
    public GameObject treePrefab2;

    public List<Tower> towers;
    public List<GameObject> towerButtons = new List<GameObject>();
    private List<GameObject> caves;
    private List<GameObject> menhirs;
    private List<GameObject> scenery;
    public List<GameObject> monsters;
    public List<GameObject> torches;
    private int currentHealth = baseHealth;

    private GameObject boss;

    public int currency = 10;

    private float mapSize = 35f;
    private int menhirAmount = 0;

    private void Awake() {
        main = this;
    }

    void Start()
    {
        caves = new List<GameObject>();
        menhirs = new List<GameObject>();
        scenery = new List<GameObject>();
        StartCoroutine(IncreaseCurrencyOverTime());
        GenerateBoss();
        GenerateCaves(25);
        GenerateMenhirs(25);
        GenerateScenery(35);
    }

    IEnumerator IncreaseCurrencyOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            AddCurrency(1);
        }
    }

    void GenerateBoss() {
        float randomDirection = Random.Range(0f, 2f * Mathf.PI);
        float randomDistance = mapSize;

        Debug.Log("Boss generation angle: " + randomDirection);

        float x = randomDistance * Mathf.Cos(randomDirection);
        float y = randomDistance * Mathf.Sin(randomDirection);

        Vector3 randomPosition = new Vector3(x, y, 0f);
        boss = Instantiate(bossPrefab, randomPosition, Quaternion.identity);
        Debug.Log("Boss generated at: " + boss.transform.position);
    }

    void GenerateCaves(int numberOfCaves)
    {
        for (int i = 0; i < numberOfCaves; i++)
        {
            float randomDirection;
            float randomDistance = 0f;
            float x, y;
            x = 0f;
            y = 0f;
            while (true) {
                randomDirection = Random.Range(0f, 2f * Mathf.PI);
                if (Random.Range(0f, 1f) < 0.7f) {  // Significantly more caves near boss
                    // Generate near boss
                    while (Mathf.Pow(x, 2) + Mathf.Pow(y, 2) < 100f) {
                        randomDistance = Random.Range(0f, 1f);
                        randomDistance = Mathf.Pow(randomDistance, 1);
                        randomDistance *= mapSize;
                        x = randomDistance * Mathf.Cos(randomDirection);
                        y = randomDistance * Mathf.Sin(randomDirection);
                        x += boss.transform.position.x;
                        y += boss.transform.position.y;
                    }
                } else {
                    while (randomDistance < 10f) {
                        randomDistance = Random.Range(0f, 1f);
                        randomDistance = 1 - Mathf.Pow(randomDistance, 3);
                        randomDistance *= mapSize;
                    }
                    x = randomDistance * Mathf.Cos(randomDirection);
                    y = randomDistance * Mathf.Sin(randomDirection);
                }
                
                bool works = true;
                if (Mathf.Pow(x - boss.transform.position.x, 2)+ Mathf.Pow(y - boss.transform.position.y, 2) < 5f) {
                    works = false;
                }
                foreach(GameObject cave in caves) {
                    if (Mathf.Pow(x - cave.transform.position.x, 2)+ Mathf.Pow(y - cave.transform.position.y, 2) < 5f) {
                        works = false;
                    }
                }
                if (works) {
                    break;
                }
            }
            

            // Generate random position within some range
            Vector3 randomPosition = new Vector3(x, y, 0f);

            // Instantiate a cave at the random position
            GameObject newCave = Instantiate(cavePrefab, randomPosition, Quaternion.identity);

            // Add the instantiated cave to the caves list
            caves.Add(newCave);
        }
    }

    void GenerateMenhirs(int numberOfCaves)
    {
        for (int i = 0; i < numberOfCaves; i++)
        {

            float randomDirection;
            float randomDistance = 0f;
            float x, y;
            while (true) {
                randomDirection = Random.Range(0f, 2f * Mathf.PI);
                if (Random.Range(0f, 1f) < 0.3f) {  // Only slighly more menhirs near boss
                    // Generate near boss
                    randomDistance = Random.Range(0f, 1f);
                    randomDistance = Mathf.Pow(randomDistance, 1);
                    randomDistance *= mapSize;
                    x = randomDistance * Mathf.Cos(randomDirection);
                    y = randomDistance * Mathf.Sin(randomDirection);
                    x += boss.transform.position.x;
                    y += boss.transform.position.y;
                } else {
                    while (randomDistance < 3f) {
                        randomDistance = Random.Range(0f, 1f);
                        randomDistance = 1 - Mathf.Pow(randomDistance, 3);
                        randomDistance *= mapSize;
                    }
                    x = randomDistance * Mathf.Cos(randomDirection);
                    y = randomDistance * Mathf.Sin(randomDirection);
                }

                bool works = true;
                if (Mathf.Pow(x - boss.transform.position.x, 2)+ Mathf.Pow(y - boss.transform.position.y, 2) < 25f) {
                    works = false;
                }
                foreach(GameObject cave in caves) {
                    if (Mathf.Pow(x - cave.transform.position.x, 2)+ Mathf.Pow(y - cave.transform.position.y, 2) < 5f) {
                        works = false;
                    }
                }
                foreach(GameObject menhir in menhirs) {
                    if (Mathf.Pow(x - menhir.transform.position.x, 2)+ Mathf.Pow(y - menhir.transform.position.y, 2) < 5f) {
                        works = false;
                    }
                }
                if (works) {
                    break;
                }
            }

            // Generate random position within some range
            Vector3 randomPosition = new Vector3(x, y, 0f);

            // Instantiate a cave at the random position
            GameObject newMenhir = Instantiate(menhirPrefab, randomPosition, Quaternion.identity);

            // Add the instantiated cave to the caves list
            menhirs.Add(newMenhir);
        }
    }

    void GenerateScenery(int numberOfBushes)
    {
        //Debug.Log("Starting bush generation");
        for (int i = 0; i < numberOfBushes; i++)
        {
            //Debug.Log("Starting generation of bush " + i);
            float randomDirection;
            float randomDistance = 0f;
            float x, y;
            while (true) {
                randomDirection = Random.Range(0f, 2f * Mathf.PI);
                if (Random.Range(0f, 1f) < 0f) {  // No bushes near boss
                    // Generate near boss
                    randomDistance = Random.Range(0f, 1f);
                    randomDistance = Mathf.Pow(randomDistance, 2);
                    randomDistance *= mapSize;
                    x = randomDistance * Mathf.Cos(randomDirection);
                    y = randomDistance * Mathf.Sin(randomDirection);
                    x += boss.transform.position.x;
                    y += boss.transform.position.y;
                } else {
                    while (randomDistance < 5f) {
                        randomDistance = Random.Range(0f, 1f);
                        randomDistance = 1 - Mathf.Pow(randomDistance, 2);
                        randomDistance *= mapSize;
                    }
                    x = randomDistance * Mathf.Cos(randomDirection);
                    y = randomDistance * Mathf.Sin(randomDirection);
                }

                bool works = true;
                if (Mathf.Pow(x - boss.transform.position.x, 2)+ Mathf.Pow(y - boss.transform.position.y, 2) < 25f) {
                    works = false;
                }
                foreach(GameObject cave in caves) {
                    if (Mathf.Pow(x - cave.transform.position.x, 2)+ Mathf.Pow(y - cave.transform.position.y, 2) < 5f) {
                        works = false;
                    }
                }
                foreach(GameObject menhir in menhirs) {
                    if (Mathf.Pow(x - menhir.transform.position.x, 2)+ Mathf.Pow(y - menhir.transform.position.y, 2) < 5f) {
                        works = false;
                    }
                }
                foreach(GameObject bush in scenery) {
                    if (Mathf.Pow(x - bush.transform.position.x, 2)+ Mathf.Pow(y - bush.transform.position.y, 2) < 5f) {
                        works = false;
                    }
                }
                if (works) {
                    break;
                }
            }

            //Debug.Log("Finished generation of bush " + i);

            // Generate random position within some range
            Vector3 randomPosition = new Vector3(x, y, 0f);

            // Instantiate a cave at the random position
            float roll = Random.Range(0f, 1f);
            GameObject newBush;
            if (roll < 0.33) {
                newBush = Instantiate(bushPrefab, randomPosition, Quaternion.identity);
            } else if (roll < 0.66) {
                newBush = Instantiate(treePrefab1, randomPosition, Quaternion.identity);
            } else {
                newBush = Instantiate(treePrefab1, randomPosition, Quaternion.identity);
            }

            // Add the instantiated cave to the caves list
            scenery.Add(newBush);
        }
    }
    

    public void RemoveMonster(GameObject monster) {
        if (monsters.Contains(monster)) {
            monsters.Remove(monster);
            GameObject.Destroy(monster);
        }
    }

    public void RemoveTorch(GameObject torch) {
        if (torches.Contains(torch)) {
            torches.Remove(torch);
            GameObject.Destroy(torch);
        }
    }

    public void AddCurrency(int amount) {
        currency += amount;
        UpdateButtons();
    }

    public void SpendCurrency(int costAmount) {
        currency -= costAmount;
        UpdateButtons();
    }

    public int GetCurrency() {
        return currency;
    }

    public void UpdateButtons() {
        foreach (GameObject button in towerButtons) {
            button.GetComponent<TowerButton>().enableButton();
        }
    }

    public void AddMenhir() {
        menhirAmount += 1;
    }

    public void RemoveMenhir() {
        menhirAmount -= 1;
    }

    public int GetMenhirAmount() {
        return menhirAmount;
    }
}
