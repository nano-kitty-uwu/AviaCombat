using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
	public EnemyFactory enemyFactory;
	public ObjectPool enemyPool;
	public WaveData[] waveData;

	private int currentWaveIndex = 0;

	private void Start()
	{
		StartCoroutine(SpawnWaves());
	}

	private IEnumerator SpawnWaves()
	{
		while (currentWaveIndex < waveData.Length)
		{
			WaveData currentWave = waveData[currentWaveIndex];
			Debug.Log($"Spawning Wave: {currentWave.waveName}");

			for (int i = 0; i < currentWave.enemyPrefabs.Length; i++)
			{
				for (int j = 0; j < currentWave.enemyCounts[i]; j++)
				{
					Vector3 spawnPosition = GetRandomSpawnPosition();
					GameObject enemy = enemyPool.GetObject();
					enemy.transform.position = spawnPosition;
					enemy.transform.rotation = Quaternion.identity;
					enemy.GetComponent<Enemy>().Initialize(enemyFactory.enemyData[i], GameManager.Instance.playerLevel);
					yield return new WaitForSeconds(currentWave.spawnInterval);
				}
			}

			currentWaveIndex++;
			yield return new WaitForSeconds(currentWave.waveDuration); // Delay between waves
		}
	}

	private Vector3 GetRandomSpawnPosition()
	{
		// Define your spawn area logic here
		return new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0);
	}
}