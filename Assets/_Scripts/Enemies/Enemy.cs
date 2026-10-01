using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Configurações do Inimigo")]
    public float speed = 3f;
    public int scoreValue = 100;
    
    [Header("Movimento em Zigue-Zague (Opcional)")]
    public bool isZigZag = false;
    public float frequency = 2f;
    public float magnitude = 1f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Movimento para baixo
        transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);

        // Movimento lateral se for zigue-zague
        if (isZigZag)
        {
            transform.position = new Vector3(
                startPos.x + Mathf.Sin(Time.time * frequency) * magnitude,
                transform.position.y,
                transform.position.z
            );
        }

        // Destrói se sair da tela por baixo
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Se colidir com o Tiro do Player
        if (collision.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject); // Destrói o tiro
            Die();
        }
        // Se colidir direto com o Player
        else if (collision.CompareTag("Player"))
        {
            // O próprio Player gerencia a perda de vida dele
            Die();
        }
    }

    void Die()
    {
        // Notifica o GameManager ou UI para somar pontos (se o script do Aluno 3 estiver configurado)
        // GameManager.instance.AddScore(scoreValue); 
        
        Destroy(gameObject);
    }
}