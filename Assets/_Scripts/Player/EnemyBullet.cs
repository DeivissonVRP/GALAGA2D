using UnityEngine;

/// <summary>
/// Coloque no prefab do tiro inimigo (BossBullet e outros).
/// Tag do objeto: EnemyBullet
/// O dano ao jogador é tratado no PlayerHealth.
/// </summary>
public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float velocidade = 6f;
    [SerializeField] private float tempoDeVida = 5f;

    private void Start()
    {
        Destroy(gameObject, tempoDeVida);
    }

    private void Update()
    {
        // Vector2.down = anda para baixo na tela
        transform.Translate(Vector2.down * velocidade * Time.deltaTime);
    }
}
