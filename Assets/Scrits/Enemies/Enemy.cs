using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
	private EnemyData enemyData;
	private int level;

	public void Initialize(EnemyData data, int level)
	{
		this.enemyData = data;
		this.level = level;

		// Apply stats from EnemyData
		this.health = enemyData.baseHealth + (enemyData.healthIncreasePerLevel * level);
		int damage = enemyData.baseDamage + (enemyData.damageIncreasePerLevel * level);
		float speed = enemyData.baseSpeed + (enemyData.speedIncreasePerLevel * level);

		// Apply stats to the enemy
		GetComponent<HealthSystem>().maxHealth = health;
		//GetComponent<HealthSystem>().Heal(health);
		// Apply damage and speed to the enemy's behavior
	}
	
		public int health { get; set; }
		public int maxHealth { get; private set; }

		public void TakeDamage(int damageAmount)
		{
			health -= damageAmount;
			if (health <= 0) Die();
		}

		public void Heal(int healAmount)
		{
			health = Mathf.Min(health + healAmount, maxHealth);
		}

		public void Die()
		{
			// Handle death (spawn effects, award points, etc.)
			Destroy(gameObject);
		}
}
