using UnityEngine;

/// <summary>
/// Coloque em TODOS os inimigos (EnemyA, EnemyB, EnemyElite2, Boss).
/// Cada prefab pode ter vida e pontos diferentes, ajustados no Inspector.
/// Tag do objeto: Enemy
/// </summary>
public class Enemy : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] private int vida = 1;
    [SerializeField] private int pontos = 100;

    [Header("Colisão com o jogador")]
    [Tooltip("Desmarque no Boss para ele não morrer ao encostar na nave")]
    [SerializeField] private bool morreAoBaterNoJogador = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Tiro do jogador (o prefab do projétil precisa ter a tag PlayerBullet)
        if (other.CompareTag("PlayerBullet"))
        {
            LevarDano(1);
        }
    }

    public void LevarDano(int dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            Morrer();
        }
    }

    private void Morrer()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.AddScore(pontos);

        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(AudioManager.instance.sfxExplosionEnemy);

        Destroy(gameObject);
    }

    // Chamado pelo PlayerHealth quando o inimigo bate na nave (sem dar pontos)
    public void ColidirComJogador()
    {
        if (morreAoBaterNoJogador)
            Destroy(gameObject);
    }
}
