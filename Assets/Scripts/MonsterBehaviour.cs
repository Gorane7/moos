using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject healthBarRedPrefab;
    [SerializeField] private GameObject healthBarGreenPrefab;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private static int baseHealth = 2;

    private int currentHealth = baseHealth;
    private GameObject healthBarRed;
    private GameObject healthBarGreen;
    public List<GameObject> torches = new List<GameObject>();
    public float towerAttackDistance= 20.0f;


    void Start()
    {
        healthBarRed = Instantiate(healthBarRedPrefab, transform.position, Quaternion.identity, transform);

        Vector3 redScale = healthBarRed.transform.localScale;
        redScale.y = 0.1f;
        healthBarRed.transform.localScale = redScale;

        Vector3 redPosition = healthBarRed.transform.localPosition;
        redPosition.y = 1.1f;
        healthBarRed.transform.localPosition = redPosition;

        //healthBarGreen = Instantiate(healthBarGreenPrefab, transform.position, Quaternion.identity, transform);
        AdjustHealthBars();
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, new Vector3(0, 0, 0));
        if (distance < towerAttackDistance || torches.Count == 0)
        {
            MoveTowardsTarget(new Vector3(0, 0, 0));
            if (distance < 0.5f)
            {

            }
        }
        else
        {
            GameObject TargetTorch = torches[0];
            distance = Vector3.Distance(transform.position, TargetTorch.transform.position);
            foreach (var t in torches)
            {
                float newdistance = Vector3.Distance(transform.position, t.transform.position);
                if (newdistance < distance)
                {
                    distance = newdistance;
                    TargetTorch = t;
                } 
            }
            MoveTowardsTarget(TargetTorch.transform.position);
            if (distance < 0.5f)
            {

            }

        }

    }

    public void ProjectileHit() {
        currentHealth -= 1;
        if (currentHealth <= 0) {
            LevelManager.main.RemoveMonster(gameObject);
        }
    }
    /*
    IEnumerator MoveTowardsCastle()
    {
        while (true)
        {
            
            // Calculate the direction towards the castle
            Vector3 direction = (LevelManager.main.castle.transform.position - transform.position).normalized;

            // Move towards the castle
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, LevelManager.main.castle.transform.position) < 0.5f) // Adjust the threshold as needed
            {
                // Remove the object if it collides with the target
                LevelManager.main.RemoveMonster(gameObject);
                CastleBehavior castleBehavior = LevelManager.main.castle.GetComponent<CastleBehavior>();
                if (castleBehavior != null)
                {
                    // Call the ProjectileHit method on the MonsterBehavior component
                    castleBehavior.MonsterHit();
                }
            }
            AdjustHealthBars();

            // Wait for the next frame
            yield return null;
        }
    }
    */
    private void MoveTowardsTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.Translate(moveSpeed * Time.deltaTime * direction);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Torch")) return;
        torches.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Torch")) return;
        torches.Remove(collision.gameObject);
        if (torches.Count == 0)
        {
        }

    }
}
