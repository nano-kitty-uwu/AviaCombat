using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
	[Header("Base Stats")]
	public int baseHealth = 100; // Base health of the enemy
	public int baseDamage = 10; // Base damage of the enemy
	public float baseSpeed = 5f; // Base movement speed of the enemy

	[Header("Scaling")]
	public int healthIncreasePerLevel = 20; // Health increase per level
	public int damageIncreasePerLevel = 5; // Damage increase per level
	public float speedIncreasePerLevel = 1f; // Speed increase per level

	[Header("Behavior")]
	public float detectionRange = 10f; // Range at which the enemy detects the player
	public float attackRange = 5f; // Range at which the enemy attacks
	public float attackCooldown = 2f; // Cooldown between attacks
}
