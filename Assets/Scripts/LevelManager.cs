using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager main;
    public static int baseHealth;

    public GameObject castle;
    private List<GameObject> towerButtons = new List<GameObject>();
    public GameObject[] caves;
    public List<GameObject> monsters;
    public List<GameObject> torches;
    private int currentHealth = baseHealth;

    public int currency = 100;

    private void Awake() {
        main = this;
    }

    void Start()
    {
    }
    

    public void RemoveMonster(GameObject monster) {
        if (monsters.Contains(monster)) {
            monsters.Remove(monster);
            Destroy(monster);
        }
    }

    public void RemoveTorch(GameObject torch) {
        if (torches.Contains(torch)) {
            torches.Remove(torch);
            Destroy(torch);
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

    public void UpdateButtons() {
        foreach (GameObject button in towerButtons) {
            button.GetComponent<TowerButton>().enableButton();
        }
    }

    /*
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            OnMouseDown();
        }
    }
    
    private void OnMouseDown() {
        // Gets the mouse position relative to camera and sets the z position to 0.
        var mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;

        // Goes through towers and checks if there is a tower near the new one.
        foreach (GameObject tower in towers) {
            if (Vector3.Distance(mouseWorldPos, tower.transform.position) < 0.5f) {
                // TODO: add build error return
                return;
            }
        }

        // Gets the tower to build.
        Tower towerToBuild = BuildManager.main.GetSelectedTower();

        // Cost check. If too expensive return if not then remove money.
        if (towerToBuild.cost > currency) {
            return;
        } else {
            SpendCurrency(towerToBuild.cost);
        }

        // Instantiates it and adds it to the list aswell.
        towers.Add(Instantiate(towerToBuild.prefab, mouseWorldPos, Quaternion.identity));
    }*/
}
