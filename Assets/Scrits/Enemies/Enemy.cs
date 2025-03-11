using UnityEngine;

public class Enemy : MonoBehaviour
{
	private EnemyData enemyData;
	private int level;

	public void Initialize(EnemyData data, int level)
	{
		this.enemyData = data;
		this.level = level;

		// Apply stats from EnemyData
		int health = enemyData.baseHealth + (enemyData.healthIncreasePerLevel * level);
		int damage = enemyData.baseDamage + (enemyData.damageIncreasePerLevel * level);
		float speed = enemyData.baseSpeed + (enemyData.speedIncreasePerLevel * level);

		// Apply stats to the enemy
		GetComponent<HealthSystem>().MaxHealth = health;
		GetComponent<HealthSystem>().Heal(health);
		// Apply damage and speed to the enemy's behavior
	}

	private void Die()
	{
		Debug.Log("Enemy has died.");
		Destroy(gameObject);
	}
}