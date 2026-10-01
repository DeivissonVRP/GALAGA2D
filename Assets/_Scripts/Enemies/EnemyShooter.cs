using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float minFireDelay = 2f;
    public float maxFireDelay = 4f;

    void Start()
    {
        // Agenda o primeiro tiro em um tempo aleatório
        float randomDelay = Random.Range(minFireDelay, maxFireDelay);
        Invoke(nameof(Shoot), randomDelay);
    }

    void Shoot()
    {
        if (enemyBulletPrefab != null && firePoint != null)
        {
            Instantiate(enemyBulletPrefab, firePoint.position, Quaternion.identity);
        }

        // Agenda o próximo tiro aleatório
        float randomDelay = Random.Range(minFireDelay, maxFireDelay);
        Invoke(nameof(Shoot), randomDelay);
    }
}