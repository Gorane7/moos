    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickScript : MonoBehaviour
{
    public GameObject torch;
    public GameObject emptytorch;
    public GameObject towerPart;
    private GameObject projectile;
    public float projectileSpeed = 1.0f;
    private bool clicked=false;
    private void Update()
    {
        if (!clicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mousePos = Input.mousePosition;
                Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
                objectPos.z = 0f;
                float distance = Vector3.Distance(new Vector3(0, 0, 0), objectPos);

                GameObject projectile = Instantiate(emptytorch, new Vector3(0, 0, 0), Quaternion.LookRotation(forward: Vector3.forward, upwards: Quaternion.Euler(0, 0, 180) * objectPos));
                projectile.transform.DOMove(objectPos, distance / projectileSpeed).SetEase(Ease.Linear).OnComplete(() => ExplodeProjectile(projectile, objectPos));
            }
            if (Input.GetMouseButtonDown(1)) {
                Debug.Log("Clicked right mouse button");
                Vector3 mousePos = Input.mousePosition;
                Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
                objectPos.z = 0f;
                float distance = Vector3.Distance(new Vector3(0, 0, 0), objectPos);

                GameObject projectile = Instantiate(towerPart, new Vector3(0, 0, 0), Quaternion.LookRotation(forward: Vector3.forward, upwards: Quaternion.Euler(0, 0, 180) * objectPos));
                projectile.transform.DOMove(objectPos, distance / projectileSpeed).SetEase(Ease.Linear).OnComplete(() => ExplodeProjectileTower(projectile, objectPos));
            }
        }
        else { clicked = false; }
    }
    private void ExplodeProjectile(GameObject thingtodestroy, Vector3 positiontocreate)
    {
        GameObject.Destroy(thingtodestroy);
        GameObject t = Instantiate(torch, positiontocreate, Quaternion.identity);
        LevelManager.main.torches.Add(t);
    }

    private void ExplodeProjectileTower(GameObject thingtodestroy, Vector3 positiontocreate)
    {
        GameObject.Destroy(thingtodestroy);

        foreach (Tower towerInList in LevelManager.main.towers)
        {
            // Check if the tower's currentObject is close to positionToCreate
            if (Vector3.Distance(towerInList.GetCurrentObject().transform.position, positiontocreate) < 3f)
            {
                towerInList.Increment();
                GameObject currentObject = towerInList.GetCurrentObject();
                GameObject newPrefab = Instantiate(towerInList.GetCurrentPrefab(), currentObject.transform.position, Quaternion.identity);
                Destroy(currentObject);
                towerInList.SetCurrentObject(newPrefab);
                return;
            }
        }

        Tower tower = BuildManager.main.GetSelectedTower();
        GameObject currentPrefab = Instantiate(tower.prefabs[0], positiontocreate, Quaternion.identity);
        tower.SetCurrentObject(currentPrefab);
        LevelManager.main.towers.Add(tower);
        
        //GameObject t = Instantiate(torch, positiontocreate, Quaternion.identity);
        //LevelManager.main.torches.Add(t);
    }

    public void SetClickedTrue()
    {
        clicked= true;
    }
}
