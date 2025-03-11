using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
	[Header("Enemy Prefabs")]
	public GameObject[] enemyPrefabs; // Array of enemy prefabs (e.g., Fighter, Bomber)

	[Header("Enemy Data")]
	public EnemyData[] enemyData; // Array of EnemyData ScriptableObjects

	// Spawn an enemy of a specific type at a given position
	public GameObject SpawnEnemy(int enemyTypeIndex, Vector3 position, Quaternion rotation, int level)
	{
		if (enemyTypeIndex < 0 || enemyTypeIndex >= enemyPrefabs.Length)
		{
			Debug.LogError("Invalid enemy type index!");
			return null;
		}

		// Instantiate the enemy prefab
		GameObject enemy = Instantiate(enemyPrefabs[enemyTypeIndex], position, rotation);

		// Configure the enemy using EnemyData
		Enemy enemyScript = enemy.GetComponent<Enemy>();
		if (enemyScript != null)
		{
			enemyScript.Initialize(enemyData[enemyTypeIndex], level);
		}
		else
		{
			Debug.LogError("Enemy prefab does not have an Enemy script!");
		}

		return enemy;
	}

	// Spawn a random enemy at a given position
	public GameObject SpawnRandomEnemy(Vector3 position, Quaternion rotation, int level)
	{
		int randomIndex = Random.Range(0, enemyPrefabs.Length);
		return SpawnEnemy(randomIndex, position, rotation, level);
	}
}