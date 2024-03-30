using System;
using UnityEngine;

[Serializable]
public class Tower {
    public string name;
    public int cost;
    public GameObject prefab;

    private GameObject currentObject;

    public Tower (string _name, int _cost, GameObject _prefab) {
        name = _name;
        cost = _cost;
        prefab = _prefab;
    }

    public void SetCurrentObject(GameObject thisObject) {
        currentObject = thisObject;
    }
}
