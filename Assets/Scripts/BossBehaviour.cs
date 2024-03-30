using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private GameObject healthBarRedPrefab;
    [SerializeField] private GameObject healthBarGreenPrefab;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private static int baseHealth = 15;

    private int currentHealth = baseHealth;
    private GameObject healthBarRed;
    private GameObject healthBarGreen;
    private GameObject castle;
    public float towerAttackDistance= 20.0f;
    public float attackspeed = 1.0f;
    private float lastattacktime;
    private Animator animator;

    private bool activated;


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
        }
        float distance = Vector3.Distance(transform.position, new Vector3(0, 0, 0));
        MoveTowardsTarget(new Vector3(0, 0, 0));
        if (distance < 0.5f && Time.time - lastattacktime > attackspeed)
        {
            castle.GetComponent<CastleBehavior>().MonsterHit();
            lastattacktime=Time.time;
        }

    }

    public void ProjectileHit() {
        currentHealth -= 1;
        if (currentHealth <= 0) {
            animator.ResetTrigger("Surm");
            animator.SetTrigger("Surm");
            GameObject.Destroy(gameObject, 1);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Torch") return;
        activated = true;
    }
}
