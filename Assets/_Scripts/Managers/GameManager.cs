using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Eventos para notificar a UI sem acoplamento direto
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnLivesChanged;
    public static event Action OnGameOver;

    [Header("Configurações Iniciais")]
    [SerializeField] private int startingLives = 3;

    public int CurrentScore { get; private set; }
    public int CurrentLives { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        CurrentScore = 0;
        CurrentLives = startingLives;

        OnScoreChanged?.Invoke(CurrentScore);
        OnLivesChanged?.Invoke(CurrentLives);
    }

    public void AddScore(int points)
    {
        CurrentScore += points;
        OnScoreChanged?.Invoke(CurrentScore);
    }

    public void TakeDamage(int amount = 1)
    {
        CurrentLives -= amount;
        if (CurrentLives < 0) CurrentLives = 0;

        OnLivesChanged?.Invoke(CurrentLives);

        if (CurrentLives <= 0)
        {
            TriggerGameOver();
        }
    }

    public void AddLife(int amount = 1)
    {
        CurrentLives += amount;
        OnLivesChanged?.Invoke(CurrentLives);
    }

    private void TriggerGameOver()
    {
        OnGameOver?.Invoke();
        Debug.Log("Game Over acionado!");
    }
}