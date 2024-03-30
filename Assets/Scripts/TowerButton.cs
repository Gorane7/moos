using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour{
    [Header("Attributes")]
    [SerializeField] private int towerCost = 0;

    public void enableButton() {
        Button button = gameObject.GetComponent<Button>();
        if ((towerCost > LevelManager.main.currency)) {
            button.interactable = false;
        } if ((towerCost <= LevelManager.main.currency)) {
            button.interactable = true;
        }
    }
}
