using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour {
    public static BuildManager main;

    [Header("References")]
    //[SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private Tower[] towers;

    private int selectedTower = 0;

    private void Awake() {
        main = this;
    }

    public Tower GetSelectedTower() {
        Tower current = towers[selectedTower];
        return new Tower(current.GetName(), current.GetCost(), current.GetPrefabs());
    }

    public void SetSelectedTower(int _selectedTower) {
        selectedTower = _selectedTower;
    }
}
