using UnityEngine;

/// <summary>
/// Cria um efeito de scroll infinito no fundo do jogo (estilo Galaga).
/// Funciona com DUAS copias do mesmo sprite de fundo, uma logo acima da outra.
///
/// Como usar:
/// 1. Duplique o objeto do fundo (Ctrl+D) para ter duas copias identicas.
/// 2. Posicione a copia B logo ACIMA da copia A, encostadas (sem espaco/sobreposicao entre elas).
///    Ex: se o fundo tem 20 unidades de altura, e A esta em Y=0, coloque B em Y=20.
/// 3. Crie um objeto vazio chamado "ScrollingBackground" e arraste as duas copias
///    do fundo para dentro dele como filhos (nao e obrigatorio, mas ajuda a organizar).
/// 4. Adicione este script em qualquer objeto da cena (pode ser no proprio "ScrollingBackground").
/// 5. Arraste as duas copias nos campos Background A e Background B.
/// 6. Preencha "Altura Do Fundo" com a altura real do sprite em unidades do mundo
///    (Selecione o fundo e olhe o tamanho em Bounds no componente Sprite Renderer,
///    ou calcule: Scale.y * (pixels de altura do sprite / Pixels Per Unit)).
/// </summary>
public class ScrollingBackground : MonoBehaviour
{
    [Header("Referências (arraste as 2 cópias do fundo)")]
    [SerializeField] private Transform backgroundA;
    [SerializeField] private Transform backgroundB;

    [Header("Configuração do movimento")]
    [Tooltip("Velocidade do scroll (unidades por segundo)")]
    [SerializeField] private float velocidade = 2f;

    [Tooltip("Altura de UM fundo, em unidades do mundo (ex: 20)")]
    [SerializeField] private float alturaDoFundo = 20f;

    private void Update()
    {
        if (backgroundA == null || backgroundB == null) return;

        // Move os dois fundos para baixo
        backgroundA.position += Vector3.down * velocidade * Time.deltaTime;
        backgroundB.position += Vector3.down * velocidade * Time.deltaTime;

        // Quando um fundo sai completamente por baixo da tela,
        // reposiciona ele logo acima do outro, criando o loop infinito.
        if (backgroundA.position.y <= -alturaDoFundo)
        {
            backgroundA.position = new Vector3(
                backgroundA.position.x,
                backgroundB.position.y + alturaDoFundo,
                backgroundA.position.z
            );
        }

        if (backgroundB.position.y <= -alturaDoFundo)
        {
            backgroundB.position = new Vector3(
                backgroundB.position.x,
                backgroundA.position.y + alturaDoFundo,
                backgroundB.position.z
            );
        }
    }
}
