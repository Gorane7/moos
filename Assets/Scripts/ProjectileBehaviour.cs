using System.Collections;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 5f;
    private float lifeLength = 5.0f;
    private float lifeStartTime;

    // Target object towards which the projectile should move
    private GameObject targetObject;

    // Set the target object for the projectile

    public void SetTargetObject(GameObject target)
    {
        targetObject = target;
    }

    private void Start()
    {
        lifeStartTime = Time.time;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += moveSpeed * Time.deltaTime * -    transform.up;

        if(Time.time - lifeStartTime > lifeLength)
        {
            DestroyObject();
        }

        /*
        // If target object is not null, move towards it
        if (targetObject != null)
        {
            // Calculate the direction towards the target
            Vector3 direction = (targetObject.transform.position - transform.position).normalized;

            // Move towards the target
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // Check for collision with the target
            if (Vector3.Distance(transform.position, targetObject.transform.position) < 0.5f) // Adjust the threshold as needed
            {
                // Remove the object if it collides with the target
                Destroy(gameObject);
                MonsterBehavior monsterBehavior = targetObject.GetComponent<MonsterBehavior>();
                if (monsterBehavior != null)
                {
                    // Call the ProjectileHit method on the MonsterBehavior component
                    monsterBehavior.ProjectileHit();
                }
            }
        } else {
            Destroy(gameObject);
        }
        */
    }
    private void DestroyObject()
    {
        Debug.Log("I DIED");
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Monster")
        {
            Debug.Log("HIT MONSTER!");
            collision.GetComponent<MonsterBehavior>().ProjectileHit();
            DestroyObject();
        }
    }
}
