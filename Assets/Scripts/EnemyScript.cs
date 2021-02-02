using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject target;            // The target the enemy moves to.
    public float moveSpeed = 5.0f;       // The move speed of the enemy.
    public float rotationSpeed = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Translate the enemy.
        transform.position += (target.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;

        // Rotate the enemy.
        transform.RotateAround(transform.position, target.transform.position - transform.position, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            // Increase player's score.
            GameManagerScript.Instance.score++;

            // Destroy the enemy and the bullet.
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
