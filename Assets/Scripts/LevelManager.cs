using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager main;
    public static int baseHealth;

    public GameObject castle;
    public GameObject cavePrefab;
    public GameObject menhirPrefab;

    public List<Tower> towers;
    private List<GameObject> towerButtons = new List<GameObject>();
    private List<GameObject> caves;
    private List<GameObject> menhirs;
    public List<GameObject> monsters;
    public List<GameObject> torches;
    private int currentHealth = baseHealth;

    public int currency = 10;

    private void Awake() {
        main = this;
    }

    void Start()
    {
        caves = new List<GameObject>();
        menhirs = new List<GameObject>();
        StartCoroutine(IncreaseCurrencyOverTime());
        GenerateCaves(100);
        GenerateMenhirs(100);
    }

    IEnumerator IncreaseCurrencyOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second
            AddCurrency(1);
        }
    }

    void GenerateCaves(int numberOfCaves)
    {
        for (int i = 0; i < numberOfCaves; i++)
        {
            float randomDirection = Random.Range(0f, 2f * Mathf.PI);

            // Generate random distance within some range
            float randomDistance = 0f;
            while (randomDistance < 10f) {
                randomDistance = Random.Range(0f, 1f);
                randomDistance = 1 - Mathf.Pow(randomDistance, 4);
                randomDistance *= 100f;
            }

            float x = randomDistance * Mathf.Cos(randomDirection);
            float y = randomDistance * Mathf.Sin(randomDirection);

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
            float randomDirection = Random.Range(0f, 2f * Mathf.PI);

            // Generate random distance within some range
            float randomDistance = 0f;
            while (randomDistance < 5f) {
                randomDistance = Random.Range(0f, 1f);
                randomDistance = 1 - Mathf.Pow(randomDistance, 4);
                randomDistance *= 50f;
            }

            float x = randomDistance * Mathf.Cos(randomDirection);
            float y = randomDistance * Mathf.Sin(randomDirection);

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
