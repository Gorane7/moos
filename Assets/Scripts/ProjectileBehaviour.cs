using System.Collections;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    public float moveSpeed = 5f;

    // Target object towards which the projectile should move
    private GameObject targetObject;

    // Set the target object for the projectile
    public void SetTargetObject(GameObject target)
    {
        targetObject = target;
    }

    // Update is called once per frame
    void Update()
    {
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
        }
    }
}
