using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int baseEnemyCount = 3;
    [SerializeField] private float minSpawnDistance = 5f;
    [SerializeField] private float maxSpawnDistance = 8f;

    private Transform playerTransform;
    private int currentWave;
    private readonly List<GameObject> aliveEnemies = new List<GameObject>();

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
    }

    void Start()
    {
        currentWave = 1;
        StartWave();
    }

    void Update()
    {
        if (aliveEnemies.Count == 0)
        {
            return;
        }

        aliveEnemies.RemoveAll(enemy => enemy == null);

        if (aliveEnemies.Count == 0)
        {
            currentWave++;
            StartWave();
        }
    }

    void StartWave()
    {
        if (playerTransform == null || enemyPrefab == null)
        {
            return;
        }

        int enemyCount = baseEnemyCount + (currentWave - 1);

        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();
            GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            aliveEnemies.Add(enemy);
        }
    }

    Vector2 GetRandomSpawnPosition()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float distance = Random.Range(minSpawnDistance, maxSpawnDistance);
        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
        return (Vector2)playerTransform.position + offset;
    }
}
