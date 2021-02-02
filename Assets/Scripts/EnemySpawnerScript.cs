using System.Collections.Generic;
using UnityEngine;

// The states of the enemy spawner:
// + SPAWN:      When the timer is greater than 0, the spawner spawns enemies to replace dead ones.
// + NOSPAWN:    When the timer reaches 0, the spawner stops spawning, but the wave still continues until the player destroys all the enemies.
// + TRANSITION: After all the enemies are destroyed, the timer is reset before another wave starts.
public enum EnemySpawnerState
{
    SPAWN,
    NOSPAWN,
    TRANSITION
}

public class EnemySpawnerScript : MonoBehaviour
{
    public float waveTime = 10.0f;                                                  // The amount of time (in seconds) per wave.
    public float transitionTime = 3.0f;                                             // The amount of time (in seconds) before the next wave starts.
    public static float timer;                                                      // The remaining time of the current wave.
    public static int   currentWave = 1;

    [SerializeField] private GameObject player;                                     // The target who the enemy moves to.
    [SerializeField] private GameObject enemyToSpawn;                               // The prefab of the enemy to be spawned.
    [SerializeField] private float distanceFromPlayer = 100.0f;                     // The 2D distance (xz plane) from the enemy to the player when the enemy is spawned.
    [SerializeField] private float minHeight = 5.0f;                                // The minimum height of the enemy when it is spawned.
    [SerializeField] private float maxHeight = 20.0f;                               // The maximum height of the enemy when it is spawned.

    private List<GameObject> enemies = new List<GameObject>();                      // Used to manage the enemies.
    public static EnemySpawnerState enemySpawnerState = EnemySpawnerState.SPAWN;

    // Start is called before the first frame update
    void Start()
    {
        // The number of enemy in the first wave is 1. In the next waves, the number of enemies is incremented.
        enemies.Add(null);
        timer = waveTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch(enemySpawnerState)
        {
            case EnemySpawnerState.SPAWN:
                if(timer > 0)
                {
                    SpawnEnemies();
                    timer -= Time.deltaTime;
                }
                else
                {
                    enemySpawnerState = EnemySpawnerState.NOSPAWN;
                }
                break;
            case EnemySpawnerState.NOSPAWN:
                if(allEnemiesAreDead() && GameManagerScript.Instance.numberOfLives > 0)
                {
                    DestroyEnemiesInPreviousWave();
                    enemySpawnerState = EnemySpawnerState.TRANSITION;
                }
                break;
            case EnemySpawnerState.TRANSITION:
                timer += Time.deltaTime* (waveTime / transitionTime);
                if(timer > waveTime)
                {
                    timer = waveTime;
                    currentWave++;
                    enemies.Add(null);
                    enemySpawnerState = EnemySpawnerState.SPAWN;
                }
                break;
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                // Calculate the position where the enemy is spawned.
                Vector2 enemyPosition2D = Random.insideUnitCircle.normalized * distanceFromPlayer;
                Vector3 enemyPosition = new Vector3(enemyPosition2D.x, Random.Range(minHeight, maxHeight), enemyPosition2D.y);

                // Spawn the enemy.
                enemies[i] = Instantiate(enemyToSpawn, enemyPosition, player.transform.rotation);

                // Make the enemy face the player.
                enemies[i].transform.LookAt(player.transform.position);

                // Set the properties of the enemy.
                EnemyScript enemyScript = enemies[i].GetComponent<EnemyScript>();
                enemyScript.target = player;
            }
        }
    }

    private void DestroyEnemiesInPreviousWave()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] != null)
            {
                Destroy(enemies[i]);
            }
        }
    }

    private bool allEnemiesAreDead()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if(enemies[i] != null)
            {
                return false;
            }
        }
        return true;
    }
}
