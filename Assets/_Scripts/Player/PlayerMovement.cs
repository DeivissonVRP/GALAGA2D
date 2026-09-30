using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float moveSpeed = 8f;

    [Header("Limites de tela (eixo X)")]
    [SerializeField] private float limiteEsquerdo = -8f;
    [SerializeField] private float limiteDireito = 8f;

    private void Update()
    {
        MoverHorizontal();
    }

    private void MoverHorizontal()
    {
        // Input.GetAxis("Horizontal") já reconhece A/D e as Setas por padrão no Unity
        float input = Input.GetAxis("Horizontal");

        Vector3 novaPosicao = transform.position + Vector3.right * input * moveSpeed * Time.deltaTime;

        // Trava a nave dentro dos limites da tela
        novaPosicao.x = Mathf.Clamp(novaPosicao.x, limiteEsquerdo, limiteDireito);

        transform.position = novaPosicao;
    }
}