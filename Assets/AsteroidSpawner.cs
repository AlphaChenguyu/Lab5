using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab; // 分配您的陨石预制体
    public float spawnInterval = 2f; // 生成间隔（秒）
    public float spawnRangeX = 5f; // X 轴生成范围

    // 将生成位置设为可调节
    public float spawnZ = 10f; // Z 轴生成位置（正方向）

    private void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 0f, spawnInterval);
    }

    private void SpawnAsteroid()
    {
        // 在 X 轴范围内随机生成
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, 0f, spawnZ); // 使用可调节的 Z 轴位置

        // 在生成位置实例化陨石
        Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
    }
}
