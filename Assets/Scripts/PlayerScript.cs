using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static int score = 0;
    public static int numberOfLives = 3;

    [SerializeField] private float rotationSpeed = 40;  // Camera rotation speed.
    public GameObject bulletToSpawn;                    // The prefab of the bullet to be spawed.

    private float rotationAngle = 0.0f;

    // Update is called once per frame
    void Update()
    {
        // Rotate the camera horizontally using A/D keys.
        rotationAngle += Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);

        // Press the left mouse button button to shoot a bullet.
        if(Input.GetButtonDown("Fire1") && numberOfLives > 0)
        {
            // Spawn a bullet.
            GameObject newBullet = Instantiate(bulletToSpawn, transform.position, transform.rotation);
            newBullet.transform.position = new Vector3(0, 0, 0);

            // Get access to the Bullet script.
            BulletScript newBulletScript = newBullet.GetComponent<BulletScript>();

            // Set the owner of the bullet.
            newBulletScript.player = gameObject;

            // Set where the bullet goes to.
            Camera camera = GetComponentInChildren<Camera>();
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            newBulletScript.target = ray.GetPoint(newBulletScript.maxDistanceFromOwner);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Destroy the enemy if it hits the player.
        if(other.CompareTag("Enemy"))
        {
            numberOfLives = (numberOfLives == 0 ? 0 : numberOfLives - 1);
            Destroy(other.gameObject);
        }
    }
}
