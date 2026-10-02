using UnityEngine;

public class EnemyElite : MonoBehaviour
{
    [Header("Configurações do Elite")]
    public float speed = 4.5f;
    public int health = 3; // Resistência mecânica (precisa de 3 tiros)
    public int scoreValue = 300;

    void Update()
    {
        // Movimento para baixo
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);

        // Destrói se sair da tela por baixo
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject); // Destrói o tiro do Player
            TakeDamage(1);
        }
        else if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // GameManager.instance.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}