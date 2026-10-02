using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Vida e Pontos")]
    public int maxHealth = 20;
    private int currentHealth;
    public int scoreValue = 2000;

    [Header("Movimento")]
    public float speed = 2f;
    public float leftBound = -5f;
    public float rightBound = 5f;
    private bool movingRight = true;

    [Header("Ataque")]
    public GameObject bossBulletPrefab;
    public Transform firePoint;
    public float fireRate = 1.2f;

    void Start()
    {
        currentHealth = maxHealth;
        InvokeRepeating(nameof(Shoot), 1f, fireRate);
    }

    void Update()
    {
        // Movimento para esquerda e direita no topo da tela
        if (movingRight)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (transform.position.x >= rightBound) movingRight = false;
        }
        else
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            if (transform.position.x <= leftBound) movingRight = true;
        }
    }

    void Shoot()
    {
        if (bossBulletPrefab != null && firePoint != null)
        {
            Instantiate(bossBulletPrefab, firePoint.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            TakeDamage(1);
        }
    }

    void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        CancelInvoke(nameof(Shoot));
        // Se houver script de Vitória na UI do Aluno 3, chame aqui:
        // GameManager.instance.ShowVictory();
        
        Destroy(gameObject);
    }
}