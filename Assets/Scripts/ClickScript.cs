    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClickScript : MonoBehaviour
{
    public GameObject torch;
    public GameObject emptytorch;
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
        }
        else { clicked = false; }
    }
    private void ExplodeProjectile(GameObject thingtodestroy, Vector3 positiontocreate)
    {
        GameObject.Destroy(thingtodestroy);
        GameObject t = Instantiate(torch, positiontocreate, Quaternion.identity);
        LevelManager.main.torches.Add(t);
    }

    public void SetClickedTrue()
    {
        clicked= true;
    }
}
