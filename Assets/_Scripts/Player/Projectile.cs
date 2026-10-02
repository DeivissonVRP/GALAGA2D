using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Configurações do Tiro")]
    [SerializeField] private float velocidade = 12f;
    [SerializeField] private float tempoDeVida = 3f;

    void Start()
    {
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        transform.Translate(Vector2.up * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("LimiteSuperior"))
        {
            Destroy(gameObject);
        }
    }
}