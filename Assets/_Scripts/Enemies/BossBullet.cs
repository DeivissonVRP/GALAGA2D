using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Projétil desce em direção ao Player
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        // Se sair da tela por baixo, destrói para não pesar o jogo
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Colide com o Player
        if (collision.CompareTag("Player"))
        {
            // O próprio Player processa a perda de vida dele
            Destroy(gameObject);
        }
    }
}