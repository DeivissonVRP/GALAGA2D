using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

/// <summary>
/// Toca o video de abertura e carrega a proxima cena quando ele termina.
/// Tambem permite pular o video apertando qualquer tecla ou clicando na tela.
/// Anexe este script no mesmo GameObject que tem o componente Video Player.
/// </summary>
[RequireComponent(typeof(VideoPlayer))]
public class IntroVideoController : MonoBehaviour
{
    [Tooltip("Nome exato da cena que sera carregada apos o video (ou ao pular)")]
    [SerializeField] private string nextSceneName = "MainMenuScene";

    [Tooltip("Permitir que o jogador pule o video apertando uma tecla ou clicando")]
    [SerializeField] private bool allowSkip = true;

    private VideoPlayer videoPlayer;
    private bool isLoadingScene = false;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    private void OnEnable()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnDisable()
    {
        videoPlayer.loopPointReached -= OnVideoFinished;
    }

    private void Update()
    {
        if (!allowSkip || isLoadingScene) return;

        bool skipPressed = Input.anyKeyDown || Input.GetMouseButtonDown(0);

        if (skipPressed)
        {
            LoadNextScene();
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        LoadNextScene();
    }

    private void LoadNextScene()
    {
        if (isLoadingScene) return;
        isLoadingScene = true;

        videoPlayer.Stop();
        SceneManager.LoadScene(nextSceneName);
    }
}
