using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class WaveData
    {
        public string waveName = "Fase 1";
        public GameObject[] enemyPrefabs; // Inimigos permitidos nesta fase
        public int totalEnemiesToSpawn = 10; // Quantos inimigos nascem antes de mudar de fase
        public float spawnInterval = 1.5f;   // Tempo entre o nascimento dos inimigos
    }

    [Header("Configuração das Fases (1 a 3)")]
    public WaveData[] waves;
    private int currentWaveIndex = 0;

    [Header("Fase 4 - Boss Final")]
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;

    [Header("Limites da Tela")]
    public float xMin = -7f;
    public float xMax = 7f;
    public float spawnY = 6f;

    private int enemiesSpawnedInCurrentWave = 0;

    void Start()
    {
        if (waves != null && waves.Length > 0)
        {
            StartCoroutine(StartWaveSequence());
        }
    }

    IEnumerator StartWaveSequence()
    {
        while (currentWaveIndex < waves.Length)
        {
            WaveData currentWave = waves[currentWaveIndex];
            enemiesSpawnedInCurrentWave = 0;

            // Loop de spawn da fase atual
            while (enemiesSpawnedInCurrentWave < currentWave.totalEnemiesToSpawn)
            {
                SpawnEnemyFromWave(currentWave);
                enemiesSpawnedInCurrentWave++;
                yield return new WaitForSeconds(currentWave.spawnInterval);
            }

            // Espera 3 segundos entre uma fase e outra para dar tempo ao jogador
            yield return new WaitForSeconds(3f);

            currentWaveIndex++;
        }

        // Quando terminar a Fase 3, inicia a Fase 4 (Boss)
        SpawnBoss();
    }

    void SpawnEnemyFromWave(WaveData wave)
    {
        if (wave.enemyPrefabs == null || wave.enemyPrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, wave.enemyPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(xMin, xMax), spawnY, 0f);

        Instantiate(wave.enemyPrefabs[randomIndex], spawnPos, Quaternion.identity);
    }

    void SpawnBoss()
    {
        if (bossPrefab != null)
        {
            Vector3 spawnPos = bossSpawnPoint != null ? bossSpawnPoint.position : new Vector3(0f, 3.5f, 0f);
            Instantiate(bossPrefab, spawnPos, Quaternion.identity);
        }
    }
}