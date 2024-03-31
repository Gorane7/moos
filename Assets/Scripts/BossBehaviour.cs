using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehavior : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject healthBarRedPrefab;
    [SerializeField] private GameObject healthBarGreenPrefab;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private static int baseHealth = 55;
    [SerializeField] public GameObject discoverMessagePrefab;
    [SerializeField] public GameObject victoryMessagePrefab;

    private int currentHealth = baseHealth;
    private GameObject healthBarRed;
    private GameObject healthBarGreen;
    private GameObject castle;
    public float towerAttackDistance = 20.0f;
    public float attackspeed = 1.0f;
    private float lastattacktime;
    private Animator animator;

    private bool activated;

    private float showMessage = 5f;

    private GameObject discoverMessage;

    void Start()
    {
        animator= GetComponent<Animator>();
        lastattacktime = Time.time; 
        castle = GameObject.FindGameObjectWithTag("Player");
        activated = false;
    }
    private void Update()
    {
        if (!activated) {
            return;
        } else {
            // After 5 seconds deenables the canvas component that says "You have angered the gods"
            if (showMessage <= 0) {
                discoverMessage.GetComponent<Canvas>().enabled = false;
            }
            showMessage -= Time.deltaTime;
        }
        float distance = Vector3.Distance(transform.position, new Vector3(0, 0, 0));
        MoveTowardsTarget(new Vector3(0, 0, 0));
        //ProjectileHit();
        if (distance < 0.5f && Time.time - lastattacktime > attackspeed)
        {
            castle.GetComponent<CastleBehavior>().MonsterHit();
            lastattacktime=Time.time;
        }

    }

    public void ProjectileHit() {
        Debug.Log("Called boss projectile hit");
        currentHealth -= 1;
        if (!activated) {
            activated = true;
            healthBarRed = Instantiate(healthBarRedPrefab, transform.position, Quaternion.identity, transform);

            Vector3 redScale = healthBarRed.transform.localScale;
            redScale.y = 0.1f;
            healthBarRed.transform.localScale = redScale;

            Vector3 redPosition = healthBarRed.transform.localPosition;
            redPosition.y = 1.1f;
            healthBarRed.transform.localPosition = redPosition;

            AdjustHealthBars();
            // Creates the Angered gods message
            discoverMessage = Instantiate(discoverMessagePrefab);
            discoverMessage.GetComponent<Canvas>().enabled = true;
        }
        AdjustHealthBars();
        if (currentHealth <= 0) {
            animator.ResetTrigger("Surm");
            animator.SetTrigger("Surm");
            StartGame();
            GameObject.Destroy(gameObject, 1);
            float elapsedTime = Time.time - LevelManager.main.GetStartTime();
            Debug.Log("Time elapsed: " + elapsedTime.ToString("F2") + " seconds");
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
    private void MoveTowardsTarget(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        transform.Translate(moveSpeed * Time.deltaTime * direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated) return;
        if (collision.tag != "Torch") return;
        activated = true;
        healthBarRed = Instantiate(healthBarRedPrefab, transform.position, Quaternion.identity, transform);

        Vector3 redScale = healthBarRed.transform.localScale;
        redScale.y = 0.1f;
        healthBarRed.transform.localScale = redScale;

        Vector3 redPosition = healthBarRed.transform.localPosition;
        redPosition.y = 1.1f;
        healthBarRed.transform.localPosition = redPosition;

        AdjustHealthBars();
        // Creates the Angered gods message
        discoverMessage = Instantiate(discoverMessagePrefab);
        discoverMessage.GetComponent<Canvas>().enabled = true;
    }

    public void StartGame() {
        SceneManager.LoadScene(4);
    }
}
