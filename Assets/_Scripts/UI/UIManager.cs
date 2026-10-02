using UnityEngine;
using TMPro; // Usando TextMeshPro

public class UIManager : MonoBehaviour
{
    [Header("Elementos de UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private GameObject gameOverPanel;

    private void OnEnable()
    {
        GameManager.OnScoreChanged += UpdateScoreUI;
        GameManager.OnLivesChanged += UpdateLivesUI;
        GameManager.OnGameOver += ShowGameOverScreen;
    }

    private void OnDisable()
    {
        GameManager.OnScoreChanged -= UpdateScoreUI;
        GameManager.OnLivesChanged -= UpdateLivesUI;
        GameManager.OnGameOver -= ShowGameOverScreen;
    }

    private void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    private void UpdateScoreUI(int newScore)
    {
        if (scoreText != null)
            scoreText.text = $"Pontos: {newScore}";
    }

    private void UpdateLivesUI(int newLives)
    {
        if (livesText != null)
            livesText.text = $"Vidas: {newLives}";
    }

    private void ShowGameOverScreen()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }
}