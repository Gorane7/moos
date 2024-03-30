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

    public List<Tower> towers;
    public List<GameObject> towerButtons = new List<GameObject>();
    private List<GameObject> caves;
    private List<GameObject> menhirs;
    public List<GameObject> monsters;
    public List<GameObject> torches;
    private int currentHealth = baseHealth;

    private GameObject boss;

    public int currency = 10;

    private void Awake() {
        main = this;
    }

    void Start()
    {
        caves = new List<GameObject>();
        menhirs = new List<GameObject>();
        StartCoroutine(IncreaseCurrencyOverTime());
        GenerateBoss();
        GenerateCaves(150);
        GenerateMenhirs(150);
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
        float randomDistance = 50f;

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
            while (true) {
                randomDirection = Random.Range(0f, 2f * Mathf.PI);
                if (Random.Range(0f, 1f) < 0.5f) {
                    // Generate near boss
                    randomDistance = Random.Range(0f, 1f);
                    randomDistance = Mathf.Pow(randomDistance, 1);
                    randomDistance *= 50f;
                    x = randomDistance * Mathf.Cos(randomDirection);
                    y = randomDistance * Mathf.Sin(randomDirection);
                    x += boss.transform.position.x;
                    y += boss.transform.position.y;
                } else {
                    while (randomDistance < 10f) {
                        randomDistance = Random.Range(0f, 1f);
                        randomDistance = 1 - Mathf.Pow(randomDistance, 3);
                        randomDistance *= 50f;
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
                if (Random.Range(0f, 1f) < 0.5f) {
                    // Generate near boss
                    randomDistance = Random.Range(0f, 1f);
                    randomDistance = Mathf.Pow(randomDistance, 1);
                    randomDistance *= 50f;
                    x = randomDistance * Mathf.Cos(randomDirection);
                    y = randomDistance * Mathf.Sin(randomDirection);
                    x += boss.transform.position.x;
                    y += boss.transform.position.y;
                } else {
                    while (randomDistance < 5f) {
                        randomDistance = Random.Range(0f, 1f);
                        randomDistance = 1 - Mathf.Pow(randomDistance, 3);
                        randomDistance *= 50f;
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
}
