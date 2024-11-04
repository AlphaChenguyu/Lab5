using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject asteroidPrefab;  // Reference to the asteroid prefab
    public float spawnZ;         // Z position to spawn the asteroid, outside camera view
    public float playableAreaWidth; // Width of the playable area on the X-axis
    public int asteroidsPerWave;    // Number of asteroids per wave
    public float spawnWait;      // Delay between each asteroid spawn
    public float waveWait;         // Delay between each wave
    public float startWait;        // Initial delay before the first wave
    public List<float> waveWaitTimes;        // 每波之间的不同延迟
    public float defaultWaveWait;  // 数组走完后的默认延迟
    private int waveIndex = 0;               // 当前波次索引
    void Start()
    {
        SpawnAsteroid();
        StartCoroutine(SpawnWaves());
    }
    IEnumerator SpawnWaves()
    {
        // Initial delay before starting the first wave
        yield return new WaitForSeconds(startWait);

        // Main loop to continuously spawn waves
        while (true)
        {
            for (int i = 0; i < asteroidsPerWave; i++)
            {
                SpawnAsteroid();

                // Delay between spawning each asteroid in a wave
                yield return new WaitForSeconds(spawnWait);
            }
            // 获取当前波次的延迟时间
            float waveWait = waveIndex < waveWaitTimes.Count ? waveWaitTimes[waveIndex] : defaultWaveWait;
            waveIndex++;
            // 每波之间的延迟
            yield return new WaitForSeconds(waveWait);
        }
    }
    void SpawnAsteroid()
    {
        float randomX = Random.Range(-playableAreaWidth / 2, playableAreaWidth / 2);

        Vector3 spawnPosition = new Vector3(randomX, 0, spawnZ);

        Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
    }

}
