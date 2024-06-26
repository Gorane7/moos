using System.Collections;
using UnityEngine;
using DG.Tweening;
using System.Drawing;

public class TorchBehavior : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject healthBarRedPrefab;

    [Header("Attributes")]

    [SerializeField] private static int baseHealth = 2;

    private int currentHealth = baseHealth;
    private GameObject healthBarRed;
    private RectTransform light;


    void Start()
    {
        //healthBarRed = Instantiate(healthBarRedPrefab, transform.position, Quaternion.identity, transform);

        //Vector3 redScale = healthBarRed.transform.localScale;
        //redScale.y = 0.1f;
        //healthBarRed.transform.localScale = redScale;

        //Vector3 redPosition = healthBarRed.transform.localPosition;
        //redPosition.y = 1.1f;
        //healthBarRed.transform.localPosition = redPosition;
        light = GetComponentInChildren<RectTransform>();
        AdjustHealthBars();
    }

    public void MonsterHit() {
        currentHealth -= 1;
        if (currentHealth <= 0) {
            light.transform.DOScale(0, 0.2f).OnComplete(() => LevelManager.main.RemoveTorch(gameObject));

        }
    }

    private void OnMouseDown()
    {
        MonsterHit();
    }

    private void AdjustHealthBars()
    {
        if (healthBarRed != null)
        {
            // Calculate the health percentage
        float healthPercentage = (float)currentHealth / baseHealth;

        Vector3 redScale = healthBarRed.transform.localScale;
        redScale.x = healthPercentage;
        healthBarRed.transform.localScale = redScale;

        Vector3 redPosition = healthBarRed.transform.localPosition;
        redPosition.x = healthPercentage - 1f;
        healthBarRed.transform.localPosition = redPosition;
        }
    }
}
