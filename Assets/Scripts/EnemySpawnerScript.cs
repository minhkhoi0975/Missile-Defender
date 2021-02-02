using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject player;                   // The target who the enemy moves to.
    [SerializeField] private GameObject enemyToSpawn;             // The prefab of the enemy to be spawned.
    [SerializeField] private byte maximumNumberOfEnemies = 3;     // The maximum number of enemies that appear at the same time.
    [SerializeField] private float distanceFromPlayer = 100.0f;   // The 2D distance (xz plane) from the enemy to the player when the enemy is spawned.
    [SerializeField] private float minHeight = 5.0f;              // The minimum height of the enemy when it is spawned.
    [SerializeField] private float maxHeight = 20.0f;             // The maximum height of the enemy when it is spawned.

    private GameObject[] enemies;                                 // Used to manage the enemies.

    // Start is called before the first frame update
    void Start()
    {
        enemies = new GameObject[maximumNumberOfEnemies];     
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < maximumNumberOfEnemies; i++)
        {
            if(enemies[i] == null)
            {
                // Calculate the position where the enemy is spawned.
                Vector2 enemyPosition2D = Random.insideUnitCircle.normalized * distanceFromPlayer;
                Vector3 enemyPosition = new Vector3(enemyPosition2D.x, Random.Range(minHeight, maxHeight), enemyPosition2D.y);

                // Spawn the enemy.
                enemies[i] = Instantiate(enemyToSpawn, enemyPosition, player.transform.rotation);

                // Set the properties of the enemy.
                EnemyScript enemyScript = enemies[i].GetComponent<EnemyScript>();
                enemyScript.target = player;
            }
        }
    }
}
