using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab; // ����������ʯԤ����
    public float spawnInterval = 2f; // ���ɼ�����룩
    public float spawnRangeX = 5f; // X �����ɷ�Χ

    // ������λ����Ϊ�ɵ���
    public float spawnZ = 10f; // Z ������λ�ã�������

    private void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), 0f, spawnInterval);
    }

    private void SpawnAsteroid()
    {
        // �� X �᷶Χ���������
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPosition = new Vector3(randomX, 0f, spawnZ); // ʹ�ÿɵ��ڵ� Z ��λ��

        // ������λ��ʵ������ʯ
        Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
    }
}
