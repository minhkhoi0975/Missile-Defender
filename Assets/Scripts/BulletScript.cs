using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float speed = 20.0f;  // The speed of the bullet.
    public Vector3 destination;                    // Where does the bullet go to?
    public GameObject player;                      // The player who shoots the bullet.

    [SerializeField] private float maxDistanceFromOwner = 50.0f; // If the distance from the bullet to the owner is greater than the maximum distance, the bullet is destroyed.
    public float MaxDistanceFromOwner { get { return maxDistanceFromOwner; } }


    [SerializeField] private GameObject bulletExplosion;     // The particle effect that happens when the bullet is destroyed.

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Move the bullet
            transform.position += (destination - player.transform.position).normalized * speed * Time.deltaTime;

            // Destroy the bullet if it is far away from the owner.
            if (Vector3.Distance(transform.position, player.transform.position) >= maxDistanceFromOwner)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        // Create particles when the bullet is destroyed.
        GameObject explosion = Instantiate(bulletExplosion, transform.position, transform.rotation);
        Destroy(explosion.gameObject, explosion.GetComponent<ParticleSystem>().main.startLifetime.Evaluate(0.0f));
    }
}
