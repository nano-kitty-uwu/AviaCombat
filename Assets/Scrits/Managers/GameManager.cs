using UnityEngine;

public class GameManager : MonoBehaviour
{
	// Singleton instance
	public static GameManager Instance { get; private set; }

	[Header("Game Settings")]
	public int startingLevel = 1; // Starting level of the game
	public int startingScore = 0; // Starting score of the player
	public int CurrentLevel;
	[Header("Wave Settings")]
	public WaveData[] waveData; // Array of WaveData ScriptableObjects
	private int currentWaveIndex = 0; // Index of the current wave

	[Header("Player Stats")]
	public int playerLevel = 1; // Current player level
	public int playerScore = 0; // Current player score
	public int playerHealth = 100; // Current player health

	private void Awake()
	{
		// Singleton pattern
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject); // Persist across scenes
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		// Initialize game state
		playerLevel = startingLevel;
		playerScore = startingScore;
		StartWave(); // Start the first wave
	}

	// Start the current wave
	public void StartWave()
	{
		if (currentWaveIndex < waveData.Length)
		{
			Debug.Log($"Starting Wave: {waveData[currentWaveIndex].waveName}");
			// Trigger wave spawning logic (e.g., call WaveManager)
		}
		else
		{
			Debug.Log("All waves completed!");
			// Handle game completion (e.g., show victory screen)
		}
	}

	// Proceed to the next wave
	public void NextWave()
	{
		currentWaveIndex++;
		if (currentWaveIndex < waveData.Length)
		{
			StartWave();
		}
		else
		{
			Debug.Log("No more waves!");
		}
	}

	// Increase the player's score
	public void IncreaseScore(int points)
	{
		playerScore += points;
		Debug.Log($"Score increased by {points}. Current score: {playerScore}");
	}

	// Increase the player's level
	public void IncreaseLevel()
	{
		playerLevel++;
		Debug.Log($"Player leveled up to level {playerLevel}");
	}

	// Handle player death
	public void PlayerDied()
	{
		Debug.Log("Player has died.");
		// Handle game over logic (e.g., show game over screen)
	}

	// Reset the game state
	public void ResetGame()
	{
		playerLevel = startingLevel;
		playerScore = startingScore;
		currentWaveIndex = 0;
		StartWave();
	}
}