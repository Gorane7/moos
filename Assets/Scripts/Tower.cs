using System;
using UnityEngine;

[Serializable]
public class Tower {
    public string name;
    public int cost;
    public GameObject[] prefabs;

    private GameObject currentObject;
    private int stageAmount;

    public Tower (string _name, int _cost, GameObject[] _prefabs) {
        name = _name;
        cost = _cost;
        prefabs = _prefabs;
        stageAmount = 0;
    }

    public void SetCurrentObject(GameObject thisObject) {
        currentObject = thisObject;
    }

    public GameObject GetCurrentObject() {
        return currentObject;
    }

    public GameObject GetCurrentPrefab() {
        return prefabs[stageAmount < prefabs.Length ? stageAmount : prefabs.Length - 1];
    }

    public void Increment() {
        stageAmount += 1;
    }

    public bool AtFinalStage() {
        return stageAmount == (prefabs.Length - 1);
    }

    public string GetName() {
        return name;
    }

    public int GetCost() {
        return cost;
    }

    public GameObject[] GetPrefabs() {
        return prefabs;
    }
}
