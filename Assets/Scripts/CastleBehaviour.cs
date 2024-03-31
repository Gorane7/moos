using System.Collections;
using UnityEngine;

public class CastleBehavior : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject healthBarRedPrefab;

    [Header("Attributes")]

    [SerializeField] private static int baseHealth = 10;

    private int currentHealth = baseHealth;

    private GameObject healthBarRed;
    private StartGameButton startGameButton = new StartGameButton();

    void Start()
    {
        healthBarRed = Instantiate(healthBarRedPrefab, transform.position, Quaternion.identity, transform);

        Vector3 redScale = healthBarRed.transform.localScale;
        redScale.y = 2f;
        healthBarRed.transform.localScale = redScale;

        Vector3 redPosition = healthBarRed.transform.localPosition;
        redPosition.y = -25f;
        healthBarRed.transform.localPosition = redPosition;
        AdjustHealthBars();
    }

    public void MonsterHit() {
        currentHealth -= 1;
        AdjustHealthBars();
    }

    private void AdjustHealthBars()
    {
        if (currentHealth > 0) {
            // Calculate the health percentage
        float healthPercentage = (float)currentHealth / baseHealth;

        Vector3 redScale = healthBarRed.transform.localScale;
        redScale.x = 10f * healthPercentage;
        healthBarRed.transform.localScale = redScale;

        Vector3 redPosition = healthBarRed.transform.localPosition;
        redPosition.x = 10f * (healthPercentage - 1f);
        healthBarRed.transform.localPosition = redPosition;
        } else {
            startGameButton.SetScene(3);
            startGameButton.StartGame();
        }
    }
}
