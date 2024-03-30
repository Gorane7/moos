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

    private void Start() {
        // Just in case setting the colour of selected tower button to be highlighted.
        LevelManager.main.towerButtons[selectedTower].GetComponent<TowerButton>().setColour(Color.green);
    }

    public Tower GetSelectedTower() {
        Tower current = towers[selectedTower];
        return new Tower(current.GetName(), current.GetCost(), current.GetPrefabs());
    }

    public void SetSelectedTower(int _selectedTower) {
        // Set previous selected tower colour to not be highlighted.
        LevelManager.main.towerButtons[selectedTower].GetComponent<TowerButton>().setColour(Color.white);
        selectedTower = _selectedTower;
        // Set new selected tower colour to be highlighted.
        LevelManager.main.towerButtons[selectedTower].GetComponent<TowerButton>().setColour(Color.green);
    }
}
