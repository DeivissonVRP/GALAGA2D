using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Controla uma cena de video (intro ou explicacao do jogo).
/// - Toca o video automaticamente.
/// - Ao apertar qualquer tecla, o video PAUSA e aparece uma confirmacao com 2 botoes:
///     - Pular video: vai para a proxima cena (gameplay)
///     - Continuar o video: esconde os botoes e o video volta a tocar de onde parou
/// - Se o video terminar sozinho (sem o jogador pular), vai direto para a proxima cena.
/// Anexe este script no mesmo GameObject que tem o componente Video Player.
/// </summary>
[RequireComponent(typeof(VideoPlayer))]
public class VideoSceneController : MonoBehaviour
{
    [Header("Navegacao")]
    [Tooltip("Cena carregada quando o video termina ou o jogador confirma pular")]
    [SerializeField] private string nextSceneName = "Cena_LevelDesign_Teste";

    [Header("Opcoes de pular")]
    [Tooltip("Permite abrir a confirmacao de pular apertando qualquer tecla do teclado")]
    [SerializeField] private bool allowKeyboardSkip = true;

    [Header("Referencias de UI (obrigatorio)")]
    [Tooltip("Botao 'Pular video' - confirma e vai para a proxima cena")]
    [SerializeField] private Button skipButton;

    [Tooltip("Botao 'Continuar o video' - cancela e o video volta a tocar")]
    [SerializeField] private Button continueButton;

    private VideoPlayer videoPlayer;
    private bool isLoadingScene = false;
    private bool isConfirmationVisible = false;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Start()
    {
        SetConfirmationVisible(false);
    }

    private void OnEnable()
    {
        videoPlayer.loopPointReached += OnVideoFinished;

        if (skipButton != null)
            skipButton.onClick.AddListener(ConfirmSkip);

        if (continueButton != null)
            continueButton.onClick.AddListener(CancelSkip);
    }

    private void OnDisable()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;

        if (skipButton != null)
            skipButton.onClick.RemoveListener(ConfirmSkip);

        if (continueButton != null)
            continueButton.onClick.RemoveListener(CancelSkip);
    }

    private void Update()
    {
        if (!allowKeyboardSkip || isLoadingScene || isConfirmationVisible) return;

        // Ignora cliques que forem em cima de botoes de UI
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        bool triggerPressed = Input.anyKeyDown;

        if (triggerPressed)
        {
            ShowConfirmation();
        }
    }

    private void ShowConfirmation()
    {
        isConfirmationVisible = true;
        videoPlayer.Pause();
        SetConfirmationVisible(true);
    }

    private void CancelSkip()
    {
        isConfirmationVisible = false;
        SetConfirmationVisible(false);
        videoPlayer.Play();
    }

    private void ConfirmSkip()
    {
        LoadScene(nextSceneName);
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // So avanca sozinho se o jogador nao estiver com a confirmacao aberta
        if (!isConfirmationVisible)
            LoadScene(nextSceneName);
    }

    private void SetConfirmationVisible(bool visible)
    {
        if (skipButton != null)
            skipButton.gameObject.SetActive(visible);

        if (continueButton != null)
            continueButton.gameObject.SetActive(visible);
    }

    private void LoadScene(string sceneName)
    {
        if (isLoadingScene || string.IsNullOrEmpty(sceneName)) return;
        isLoadingScene = true;

        videoPlayer.Stop();
        SceneManager.LoadScene(sceneName);
    }
}
