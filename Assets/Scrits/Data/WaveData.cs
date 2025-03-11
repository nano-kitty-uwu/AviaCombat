using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{
	[Header("Wave Info")]
	public string waveName = "Wave 1"; // Name of the wave
	public int waveNumber = 1; // Wave number

	[Header("Enemy Spawning")]
	public GameObject[] enemyPrefabs; // Array of enemy prefabs to spawn
	public int[] enemyCounts; // Number of each enemy type to spawn
	public float spawnInterval = 2f; // Time between enemy spawns
	public float waveDuration = 30f; // Duration of the wave

	[Header("Scaling")]
	public int healthMultiplier = 1; // Health multiplier for enemies in this wave
	public int damageMultiplier = 1; // Damage multiplier for enemies in this wave
	public float speedMultiplier = 1f; // Speed multiplier for enemies in this wave
}
