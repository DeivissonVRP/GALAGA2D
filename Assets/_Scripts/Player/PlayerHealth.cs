using System.Collections;
using UnityEngine;

/// <summary>
/// Coloque na nave (Player). Tag do objeto: Player
/// Detecta colisão com inimigos e tiros inimigos, tira vida no GameManager,
/// dá um tempo de invulnerabilidade (piscando) e desliga a nave no Game Over.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    [Header("Invulnerabilidade após levar dano")]
    [SerializeField] private float tempoInvulneravel = 1.5f;
    [SerializeField] private float intervaloPiscar = 0.1f;

    private bool invulneravel = false;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (invulneravel) return;

        if (other.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
            LevarDano();
        }
        else if (other.CompareTag("Enemy"))
        {
            Enemy inimigo = other.GetComponent<Enemy>();
            if (inimigo != null) inimigo.ColidirComJogador();
            LevarDano();
        }
    }

    private void LevarDano()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.TakeDamage(1);

        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(AudioManager.instance.sfxExplosionPlayer);

        if (GameManager.Instance.CurrentLives <= 0)
        {
            MorrerDeVez();
        }
        else
        {
            StartCoroutine(Invulnerabilidade());
        }
    }

    private IEnumerator Invulnerabilidade()
    {
        invulneravel = true;
        float fim = Time.time + tempoInvulneravel;

        while (Time.time < fim)
        {
            if (sprite != null) sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(intervaloPiscar);
        }

        if (sprite != null) sprite.enabled = true;
        invulneravel = false;
    }

    private void MorrerDeVez()
    {
        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(AudioManager.instance.sfxGameOver);

        // Desativa a nave (some da tela e para de atirar/mover)
        gameObject.SetActive(false);
    }
}
